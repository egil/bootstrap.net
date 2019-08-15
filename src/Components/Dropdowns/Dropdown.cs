using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Accessibility;
using Egil.RazorComponents.Bootstrap.Components.Dropdowns.Events;
using Egil.RazorComponents.Bootstrap.Components.Dropdowns.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Components.Dropdowns
{
    public sealed class Dropdown : ParentComponentBase
    {
        private const string DropdownToggleCssClass = "dropdown-toggle";
        private const string SplitDropdownToggleCssClass = "dropdown-toggle dropdown-toggle-split";
        private const string BtnDropdownToggleCssClass = "btn " + DropdownToggleCssClass;
        private const string MenuCssClass = "dropdown-menu";
        private const string DropdownItemCssClass = "dropdown-item";
        private const string TrueValue = "true";

        private readonly string _toggleId;
        private ElementReference _menuDomElement;

        [Inject] private IJSRuntime? JSRT { get; set; }

        [Inject] private IEventBus? EventBus { get; set; }

        /// <summary>
        /// Gets or sets the screen reader only text used in the toggle button
        /// if no other text is provided via the <see cref="Text"/> parameter.
        /// </summary>
        [Parameter] public string SrOnlyToggleText { get; set; } = "Toggle Dropdown";

        /// <summary>
        /// Gets or sets the color of the dropdowns toggle button.
        /// </summary>
        [Parameter, CssClassExcluded] public ColorParameter<ButtonColor>? Color { get; set; }

        /// <summary>
        /// Gets or sets the size of the dropdown toggle buttons.
        /// </summary>
        [Parameter, CssClassExcluded] public SizeParamter<ButtonSize>? Size { get; set; }

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

        /// <summary>
        /// Gets or sets whether the dropdown is visible/open.
        /// </summary>
        [Parameter] public bool Visible { get; set; }
        [Parameter] public EventCallback<bool> VisibleChanged { get; set; }

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
            EventBus!.PublishAsync(new Event<DropdownOpenedEventType, Dropdown>(DropdownOpenedEventType.Instance, this));
        }

        [JSInvokable]
        public void Hide()
        {
            if (!Visible) return;

            Visible = false;
            StateHasChanged();
        }

        protected override void ApplyChildHooks(ComponentBase component)
        {
            switch (component)
            {
                case Button btn:
                    ApplyButtonHooks(btn);
                    break;

                case A a:
                    ApplyAHooks(a);
                    break;

                case Menu menu:
                    menu.DomElementCapture = (elm) => _menuDomElement = elm;
                    menu.CustomChildHooksInjector = CustomMenuChildHooksInjector;
                    ApplyMenuHooks(menu);
                    break;

                case Form form:
                    ApplyMenuHooks(form);
                    break;

                default: break;
            }
        }

        protected override void OnCompomnentInit()
        {
            EventBus!.Subscribe<DropdownOpenedEventType, Dropdown>(DropdownOpenedEventType.Instance, DropDownOpenedEventHandler);
        }

        protected override void OnCompomnentParametersSet()
        {
            DefaultCssClass = Split || Direction != DirectionParameter.Default
                ? "btn-group"
                : "dropdown";
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementTag);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);
            AddKeyboardNavigation(builder);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(OverriddenAttributes);
            builder.AddContent(BuiltInButtonsRenderFragment);
            builder.AddContent(ChildContent);
            builder.AddElementReferenceCapture(DomElementCapture);
            builder.CloseElement();
        }

        protected override Task OnCompomnentAfterRenderAsync()
        {
            if (Visible)
            {
                //  TOP       : 'top-start',
                //  TOPEND    : 'top-end',
                //  BOTTOM    : 'bottom-start',
                //  BOTTOMEND : 'bottom-end',
                //  RIGHT     : 'right-start',
                //  RIGHTEND  : 'right-end',
                //  LEFT      : 'left-start',
                //  LEFTEND   : 'left-end'
                var placement = Direction.DirectionValue switch
                {
                    "up" => "top-start",
                    "down" => "bottom-start",
                    "right" => "right-start",
                    "left" => "left-start",
                    string dir => dir
                };
                var positionTask = JSRT!.InvokeAsync<object>("bootstrapDotNet.components.dropdown.positionDropdown", _menuDomElement, "toggle", placement);
                var closeTask = JSRT!.InvokeAsync<object>("bootstrapDotNet.components.dropdown.addDocumentClickEventListener", CreateDotNetObjectRef(this), nameof(Hide));
                return Task.WhenAll(positionTask, closeTask);
            }
            else
                return Task.CompletedTask;
        }

        protected override void OnCompomnentDispose()
        {
            EventBus!.Unsubscribe<DropdownOpenedEventType, Dropdown>(DropdownOpenedEventType.Instance, DropDownOpenedEventHandler);
        }

        private void DropDownOpenedEventHandler(IEvent<DropdownOpenedEventType, Dropdown> evt)
        {
            if (evt.Source == this) return;
            InvokeAsync(Hide);
        }

        private void ApplyAHooks(A a)
        {
            a.CustomRenderFragment = builder =>
            {
                a.AsButton = true;
                a.Color = Color;

                if (!Split)
                {
                    a.DefaultCssClass = Size is null
                        ? DropdownToggleCssClass
                        : $"{DropdownToggleCssClass} {Size.Value}";

                    a.AddOverride(HtmlAttrs.ID, _toggleId);
                    a.AddOverride(HtmlAttrs.ARIA_HASPOPUP, TrueValue);
                    a.AddOverride(HtmlAttrs.ARIA_EXPANDED, Visible.ToLowerCaseString());
                    a.AddOverride(HtmlAttrs.ROLE, "button");
                    a.AddOverride(HtmlEvents.CLICK, JoinEventCallbacks<UIMouseEventArgs>(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle)));

                    builder.AddContent(a.DefaultRenderFragment);
                }
                else
                {
                    if (!(Size is null))
                        a.DefaultCssClass = Size.Value;

                    builder.AddContent(a.DefaultRenderFragment);

                    CaretToggleButtonRenderFragment(builder);
                }
            };
        }

        private void ApplyButtonHooks(Button btn)
        {
            btn.CustomRenderFragment = builder =>
            {
                btn.Color = Color;
                btn.Size = Size;

                if (!Split)
                {
                    btn.DefaultCssClass = BtnDropdownToggleCssClass;

                    btn.AddOverride(HtmlAttrs.ID, _toggleId);
                    btn.AddOverride(HtmlAttrs.ARIA_HASPOPUP, TrueValue);
                    btn.AddOverride(HtmlAttrs.ARIA_EXPANDED, Visible.ToLowerCaseString());
                    btn.AddOverride(HtmlEvents.CLICK, JoinEventCallbacks<UIMouseEventArgs>(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle)));

                    builder.AddContent(btn.DefaultRenderFragment);
                }
                else
                {
                    builder.AddContent(btn.DefaultRenderFragment);

                    CaretToggleButtonRenderFragment(builder);
                }
            };
        }

        private void ApplyMenuHooks(ComponentBase menu)
        {
            menu.DefaultCssClass = MenuCssClass;
            menu.AddOverride(HtmlAttrs.ARIA_LABELLEDBY, _toggleId);
            menu.OnParametersSetHook = _ => menu.DefaultCssClass = Visible ? $"{MenuCssClass} show" : MenuCssClass;
        }

        private void CustomMenuChildHooksInjector(ComponentBase component)
        {
            switch (component)
            {
                case A a:
                    a.DefaultCssClass = DropdownItemCssClass;
                    break;

                case Button button:
                    button.DefaultCssClass = DropdownItemCssClass;
                    break;

                case Span span:
                    span.DefaultCssClass = "dropdown-item-text";
                    break;

                case Hr hr:
                    hr.DefaultCssClass = "dropdown-divider";
                    hr.DefaultElementTag = HtmlTags.DIV;
                    break;

                case Heading heading:
                    heading.DefaultCssClass = "dropdown-header";

                    break;
                default: break;
            }
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
                    case "ArrowDown":
                        await JSRT!.InvokeAsync<object>("bootstrapDotNet.components.dropdown.changeFocusedDropdownItem", _menuDomElement, e.Code);
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
            builder.AddAttribute(HtmlAttrs.ARIA_EXPANDED, Visible.ToLowerCaseString());
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle));
            builder.AddDisableParentOverridesAttribute(true);
        }


        #region Hack to fix https://github.com/aspnet/AspNetCore/issues/11159
        public static object CreateDotNetObjectRefSyncObj = new object();

        private DotNetObjectRef<T> CreateDotNetObjectRef<T>(T value) where T : class
        {
            lock (CreateDotNetObjectRefSyncObj)
            {
                JSRuntime.SetCurrentJSRuntime(JSRT);
                return DotNetObjectRef.Create(value);
            }
        }
        #endregion
    }
}
