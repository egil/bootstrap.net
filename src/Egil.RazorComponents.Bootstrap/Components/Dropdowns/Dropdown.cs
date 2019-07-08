using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Accessibility;
using Egil.RazorComponents.Bootstrap.Components.Dropdowns.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Components.Dropdowns
{
    public sealed class Dropdown : BootstrapParentComponentBase
    {
        private const string DropdownToggleCssClass = "dropdown-toggle";
        private const string SplitDropdownToggleCssClass = "dropdown-toggle dropdown-toggle-split";
        private const string BtnDropdownToggleCssClass = "btn " + DropdownToggleCssClass;
        private const string MenuCssClass = "dropdown-menu";
        private const string DropdownItemCssClass = "dropdown-item";
        private const string TrueValue = "true";
        private const string FalseValue = "false";
        private readonly string _toggleId;

        private List<IExploseElementRef> _items = new List<IExploseElementRef>();
        private int _itemInFocus = -1;

        [Inject] private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// Gets or sets the screen reader only text used in the toggle button
        /// if no other text is provided via the <see cref="Text"/> parameter.
        /// </summary>
        [Parameter] public string SrOnlyToggleText { get; set; } = "Toggle Dropdown";

        /// <summary>
        /// Gets or sets the color of the dropdowns toggle button.
        /// </summary>
        [Parameter, CssClassExcluded] public ColorParameter<ButtonColor> Color { get; set; } = ColorParameter<ButtonColor>.None;

        /// <summary>
        /// Gets or sets the size of the dropdown toggle buttons.
        /// </summary>
        [Parameter, CssClassExcluded] public SizeParamter<ButtonSize> Size { get; set; } = SizeParamter<ButtonSize>.Medium;

        /// <summary>
        /// Gets or sets the text displayed in the toggle button or the 
        /// attached button, if <see cref="Split"/> is set to true.
        /// </summary>
        [Parameter] public string? Text { get; set; }

        /// <summary>
        /// Gets or sets whether to render toggle button with a caret and a
        /// second attached button to the right, that can be used to do 
        /// anything a normal button can be used for.
        /// </summary>
        [Parameter] public bool Split { get; set; } = false;

        /// <summary>
        /// Gets or sets the direction of the drop "down". Valid options are:
        /// down (default), up, left, right.
        /// </summary>
        [Parameter] public DirectionParameter Direction { get; set; } = DirectionParameter.Default;

        [Parameter] public bool Visible { get; set; }

        public Dropdown()
        {
            _toggleId = $"dropdown-toggle-{GetHashCode()}";
        }

        public void Toggle()
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void Show()
        {
            if (Visible) return;

            Visible = true;
            StateHasChanged();
        }

        public void Hide()
        {
            if (!Visible) return;

            Visible = false;
            StateHasChanged();
        }

        protected override void OnBootstrapParametersSet()
        {
            DefaultCssClass = Split || Direction != DirectionParameter.Default
                ? "btn-group"
                : "dropdown";
        }

        protected override void OnRegisterChildRules()
        {
            Rules.RegisterOnInitRule<Button>(btn =>
            {
                btn.CustomRenderFragment = builder =>
                {
                    btn.Color = Color;
                    btn.Size = Size;

                    if (!Split)
                    {
                        btn.DefaultCssClass = BtnDropdownToggleCssClass;

                        btn.AdditionalAttributes[HtmlAttrs.ID] = _toggleId;
                        btn.AdditionalAttributes[HtmlAttrs.ARIA_HASPOPUP] = TrueValue;
                        btn.AdditionalAttributes[HtmlAttrs.ARIA_EXPANDED] = FalseValue;
                        btn.AdditionalAttributes[HtmlEvents.CLICK] = EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle);

                        builder.AddContent(btn.DefaultRenderFragment);
                    }
                    else
                    {
                        builder.AddContent(btn.DefaultRenderFragment);

                        CaretToggleButtonRenderFragment(builder);
                    }
                };
            });

            Rules.RegisterOnInitRule<A>(a =>
            {
                a.CustomRenderFragment = builder =>
                {
                    a.AsButton = true;
                    a.Color = Color;

                    if (!Split)
                    {
                        a.DefaultCssClass = Size != SizeParamter<ButtonSize>.Medium
                            ? $"{DropdownToggleCssClass} {Size.Value}"
                            : DropdownToggleCssClass;

                        a.AdditionalAttributes[HtmlAttrs.ID] = _toggleId;
                        a.AdditionalAttributes[HtmlAttrs.ARIA_HASPOPUP] = TrueValue;
                        a.AdditionalAttributes[HtmlAttrs.ARIA_EXPANDED] = FalseValue;
                        a.AdditionalAttributes[HtmlAttrs.ROLE] = "button";


                        builder.AddContent(a.DefaultRenderFragment);
                    }
                    else
                    {
                        a.DefaultCssClass = Size.Value;

                        builder.AddContent(a.DefaultRenderFragment);

                        CaretToggleButtonRenderFragment(builder);
                    }
                };
            });

            Rules.RegisterOnInitRule<Menu>(menu =>
            {
                menu.DefaultCssClass = MenuCssClass;

                Rules.RegisterOnInitRule<A>(a =>
                {
                    a.DefaultCssClass = DropdownItemCssClass;
                    _items.Add(a);
                });
                Rules.RegisterOnInitRule<Button>(button =>
                {
                    button.DefaultCssClass = DropdownItemCssClass;
                    _items.Add(button);
                });
                Rules.RegisterOnInitRule<Span>(span => span.DefaultCssClass = "dropdown-item-text");
                Rules.RegisterOnInitRule<Hr>(hr =>
                {
                    hr.DefaultCssClass = "dropdown-divider";
                    hr.DefaultElementName = HtmlTags.DIV;
                });
                Rules.RegisterOnInitRule<Heading, H1, H2, H3, H4, H5, H6>(heading => heading.DefaultCssClass = "dropdown-header");
            });

            Rules.RegisterRule<Menu>(menu =>
            {
                menu.AdditionalAttributes[HtmlAttrs.ARIA_LABELLEDBY] = _toggleId;
                menu.DefaultCssClass = Visible ? $"{MenuCssClass} show" : MenuCssClass;
            });

            Rules.RegisterOnInitRule<Form>(form =>
            {
                form.DefaultCssClass = MenuCssClass;
            });

            Rules.RegisterRule<Form>(form =>
            {
                form.AdditionalAttributes[HtmlAttrs.ARIA_LABELLEDBY] = _toggleId;
                form.DefaultCssClass = Visible ? $"{MenuCssClass} show" : MenuCssClass;
            });

        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementName);
            builder.AddClassAttribute(CssClassValue);
            AddKeyboardNavigation(builder);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(BuiltInButtonsRenderFragment);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }

        private void AddKeyboardNavigation(RenderTreeBuilder builder)
        {
            if (!Visible) return;

            // To make the KEYDOWN event fire on a DIV element, it must be focusable. Setting
            // tabindex to -1 makes it focusable, without making it possible to tab to it.
            // See https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/tabindex for details.
            builder.AddTabIndex(-1);

            builder.AddEventListener(HtmlEvents.KEYDOWN, EventCallback.Factory.Create<UIKeyboardEventArgs>(this, async (UIKeyboardEventArgs e) =>
            {
                switch (e.Code)
                {
                    case "Escape": Hide(); break;
                    case "ArrowUp":
                        if (_itemInFocus <= 0) return;
                        _itemInFocus--;
                        await JSRuntime!.InvokeAsync<object>("bootstrapDotNet.utils.focus", _items[_itemInFocus].DomElement);
                        break;
                    case "ArrowDown":
                        if (_itemInFocus >= _items.Count - 1) return;
                        _itemInFocus++;
                        await JSRuntime!.InvokeAsync<object>("bootstrapDotNet.utils.focus", _items[_itemInFocus].DomElement);
                        break;
                    default: break;
                }
            }));
        }

        private void BuiltInButtonsRenderFragment(RenderTreeBuilder builder)
        {
            if (Text is null) return;

            if (!Split)
            {
                builder.OpenComponent<Button>();
                builder.AddClassAttribute(DropdownToggleCssClass);
                AddToggleButtonAttributes(builder);
                builder.AddChildMarkupContent(Text);
                builder.CloseComponent();
            }
            else
            {
                builder.OpenComponent<Button>();
                builder.AddAttribute(nameof(Color), Color);
                builder.AddAttribute(nameof(Size), Size);
                builder.AddChildMarkupContent(Text);
                builder.CloseComponent();

                CaretToggleButtonRenderFragment(builder);
            }
        }

        private void CaretToggleButtonRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenComponent<Button>();
            builder.AddClassAttribute(SplitDropdownToggleCssClass);
            AddToggleButtonAttributes(builder);
            builder.AddChildContentFragment((nested) =>
            {
                nested.OpenComponent<SrOnly>();
                nested.AddChildMarkupContent(SrOnlyToggleText);
                nested.CloseComponent();
            });
            builder.CloseComponent();
        }

        private void AddToggleButtonAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(nameof(Color), Color);
            builder.AddAttribute(nameof(Size), Size);
            builder.AddIdAttribute(_toggleId);
            builder.AddAttribute(HtmlAttrs.ARIA_HASPOPUP, TrueValue);
            builder.AddAttribute(HtmlAttrs.ARIA_EXPANDED, FalseValue);
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle));
            builder.AddIgnoreParentContextAttribute(true);
        }
    }
}
