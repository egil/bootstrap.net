using System;
using System.Linq;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles.Events;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class Button : ParentComponentBase, IToggleForCollapse
    {
        #region IToggleForCollapse state

        string[] IToggleForCollapse.SubscribedToggleTargetIds { get; set; } = Array.Empty<string>();
        IEventBus IToggleForCollapse.EventBus => EventBus!;
        EventCallback<UIMouseEventArgs>? IToggleForCollapse.ToggleForClickHandler
        {
            get => ToggleForClickHandler; set => ToggleForClickHandler = value;
        }

        #endregion 

        private EventCallback<UIMouseEventArgs>? ToggleableClickHandler { get; set; }
        private EventCallback<UIMouseEventArgs>? ToggleForClickHandler { get; set; }

        [Inject] private IEventBus? EventBus { get; set; }

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
        [Parameter] public ColorParameter<ButtonColor>? Color { get; set; }

        /// <summary>
        /// Set to true to remove all background color from the button. Keeps the specified 
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
        [Parameter] public SizeParamter<ButtonSize>? Size { get; set; }

        /// <summary>
        /// Gets or sets whether the button should be disabled or not.
        /// </summary>
        [Parameter] public bool Disabled { get; set; } = false;

        /// <summary>
        /// Get or set the active state of the button. If set to true, the 'active' css class is added to the component.
        /// </summary>
        [Parameter, CssClassToggleParameter("active")] public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound active state.
        /// </summary>
        [Parameter] public EventCallback<bool> ActiveChanged { get; set; }

        /// <summary>
        /// Gets or sets automatic toggling of <see cref="Active"/>.
        /// </summary>
        [Parameter] public bool Toggleable { get; set; } = false;

        /// <summary>
        /// Gets or sets the IDs of the <see cref="Collapse"/> components that this 
        /// component should be used to toggle. One or more IDs can be specified 
        /// by separating them with a comma or space.
        /// </summary>
        [Parameter] public string? ToggleFor { get; set; }

        public Button()
        {
            DefaultCssClass = "btn";
        }

        /// <summary>
        /// Toggles <see cref="Active"/>.
        /// </summary>
        public void Toggle()
        {
            if (Toggleable)
            {
                Active = !Active;
                ActiveChanged.InvokeAsync(Active);
            }
        }

        protected override void OnCompomnentInit()
        {
            ((IToggleForCollapse)this).AddToggleHooks(this);
        }

        protected override void OnCompomnentParametersSet()
        {
            if (!(Color is null))
                Color.ColorPrefix.Outlined = Outlined;

            if (Toggleable && ToggleableClickHandler is null)
            {
                ToggleableClickHandler = EventCallback.Factory.Create<UIMouseEventArgs>(this, Toggle);
            }

            if (ToggleableClickHandler.HasValue || ToggleForClickHandler.HasValue)
            {
                AddOverride(HtmlEvents.CLICK, JoinEventCallbacks(HtmlEvents.CLICK, ToggleableClickHandler, ToggleForClickHandler));
            }
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.BUTTON);

            builder.AddIdAttribute(Id);
            builder.AddAttribute(HtmlAttrs.TYPE, Type);
            builder.AddClassAttribute(CssClassValue);

            if (Toggleable || Active)
            {
                AddOverride(HtmlAttrs.ARIA_PRESSED, Active.ToLowerCaseString());
            }

            if (Disabled)
            {
                builder.AddAttribute(HtmlAttrs.ARIA_DISABLED, "true");
                builder.AddAttribute(HtmlAttrs.DISABLED, Disabled);
            }

            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(OverriddenAttributes);

            builder.AddContent(ChildContent);

            builder.AddElementReferenceCapture(DomElementCapture);
            builder.CloseElement();
        }
    }
}
