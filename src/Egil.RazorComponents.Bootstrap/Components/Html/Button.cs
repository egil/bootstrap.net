using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class Button : BootstrapHtmlElementComponentBase, IToggleForCollapse, IDisposable
    {
        /// <summary>
        /// The type of the button. Possible values are:
        /// <list type="bullet">
        /// <item>
        /// <term>button</term>
        /// <description>Default value. The button has no default behavior.It can have client-side scripts associated with the element's events, which are triggered when the events occur.</description>
        /// </item>
        /// <item>
        /// <term>submit</term>
        /// <description>The button submits the form data to the server.This is the default if the attribute is not specified, or if the attribute is dynamically changed to an empty or invalid value.</description>
        /// </item>
        /// <item>
        /// <term>reset</term>
        /// <description>The button resets all the controls to their initial values..</description>
        /// </item>
        /// </list>
        /// </summary>
        [Parameter] public string Type { get; set; } = "button";

        /// <summary>
        /// Sets the color of the button.
        /// </summary>
        [Parameter] public ColorParameter<ButtonColor> Color { get; set; } = ColorParameter<ButtonColor>.None;

        /// <summary>
        /// Set to true to remove all backgroud color from the button. Keeps the specified 
        /// color in text and border.
        /// </summary>
        /// <see cref="Color"/>
        [Parameter] public bool Outlined { get; set; } = false;

        /// <summary>
        /// Set to true to make the button appear as a link.
        /// </summary>
        [Parameter, CssClassToggleParameter("btn-link")] public bool AsLink { get; set; } = false;

        /// <summary>
        /// Gets or sets the size of the button.
        /// </summary>
        [Parameter] public SizeParamter<ButtonSize> Size { get; set; } = SizeParamter<ButtonSize>.Medium;

        /// <summary>
        /// Get or set the active state of the button. If set to true, the 'active' css class is added to the component.
        /// </summary>
        [Parameter, CssClassToggleParameter("active")] public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound active state.
        /// </summary>
        [Parameter] public EventCallback<bool> IsActiveChanged { get; set; }

        /// <summary>
        /// Gets or sets automatic toggling of <see cref="IsActive"/>.
        /// </summary>
        [Parameter] public bool Toggleable { get; set; } = false;

        #region IToggleForCollapse

        private bool _isToggleTargetExpanded;
        private string? _ariaControls;
        public event EventHandler OnToggled;

        [Parameter] public string? ToggleFor { get; set; }

        void IToggleForCollapse.SetExpandedState(bool isExpanded)
        {
            _isToggleTargetExpanded = isExpanded;
            StateHasChanged();
        }

        #endregion

        public Button()
        {
            DefaultCssClass = "btn";
        }

        /// <summary>
        /// Toggles <see cref="IsActive"/>.
        /// </summary>
        public async void Toggle()
        {
            if (Toggleable)
            {
                IsActive = !IsActive;
                await IsActiveChanged.InvokeAsync(IsActive);
            }

            if (!(OnToggled is null))
            {
                OnToggled?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnBootstrapParametersSet()
        {
            Color.ColorPrefix.Outlined = Outlined;
        }

        protected override void OnBootstrapInit()
        {
            IToggleForCollapse.Connect(this);
            _ariaControls = string.Join(' ', ToggleFor?.SplitOnComma() ?? Array.Empty<string>());
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.BUTTON);

            if (Toggleable || !(OnToggled is null))
            {
                builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle));
            }

            if (!(ToggleFor is null))
            {
                builder.AddAttribute(HtmlAttrs.ARIA_EXPANDED, _isToggleTargetExpanded.ToLowerCaseString());
                builder.AddAttribute(HtmlAttrs.ARIA_CONTROLS, _ariaControls);
            }

            builder.AddAttribute(HtmlAttrs.TYPE, Type);
            builder.AddClassAttribute(CssClassValue);
            builder.AddAttribute("aria-pressed", IsActive.ToLowerCaseString());
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }

        public void Dispose()
        {
            IToggleForCollapse.Disconnect(this);
        }
    }
}
