using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Animations;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts
{
    public sealed class Alert : BootstrapParentComponentBase
    {
        private const string DefaultRole = "alert";
        private const string CssClass = "alert";
        private const string LinkCssClass = "alert-link";
        private const string HeadingCssClass = "alert-heading";
        private const string DismissableCssClass = "alert-dismissible";
        private const string DismissButtonType = "button";
        private const string CloseCssClass = "close";
        private const string DefaultDismissAriaLabel = "Close";
        private const string DefaultDismissText = "&times;";

        private ICssClassAnimation DismissAnimation { get; } = new FadeOutAnimation();

        [Parameter] public bool EnableRendering { get; set; } = true;

        /// <summary>
        /// Gets or sets the role HTML attribute used on the component.
        /// </summary>
        [Parameter] public string Role { get; set; } = DefaultRole;

        /// <summary>
        /// Gets or sets the padding of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        /// <summary>
        /// Gets or sets the margin of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        [Parameter] public ColorParameter<AlertColor> Color { get; set; } = ColorParameter<AlertColor>.None;

        [Parameter, CssClassToggleParameter(DismissableCssClass)]
        public bool Dismissable { get; set; } = false;

        [Parameter] public EventCallback<UIDismissingEventArgs> OnDismissing { get; set; }

        [Parameter] public EventCallback OnDismissed { get; set; }

        [Parameter] public string DismissAriaLabel { get; set; } = DefaultDismissAriaLabel;

        [Parameter] public string DismissText { get; set; } = DefaultDismissText;

        public AlertState State { get; private set; } = AlertState.Visible;

        public Alert()
        {
            DefaultCssClass = CssClass;
        }

        public async void Dismiss()
        {
            var evt = await NotifyParent();
            if (evt.Cancel) { return; }

            await DismissAlert();

            DisableAfter();

            async Task<UIDismissingEventArgs> NotifyParent()
            {
                var evt = new UIDismissingEventArgs();
                await OnDismissing.InvokeAsync(evt);
                return evt;
            }

            async Task DismissAlert()
            {
                State = AlertState.Dismissing;
                await DismissAnimation.Run();
                State = AlertState.Dismissed;
                await OnDismissed.InvokeAsync(EventCallback.Empty);
            }

            void DisableAfter()
            {
                if (!evt.EnabledAfter)
                {
                    EnableRendering = false;
                    StateHasChanged();
                }
            }
        }

        public void Show()
        {
            State = AlertState.Visible;
            EnableRendering = true;
            StateHasChanged();
            DismissAnimation.Reset();
            StateHasChanged();
        }

        protected override void OnRegisterChildRules()
        {
            Rules.RegisterOnInitRule<A>(x => x.DefaultCssClass = LinkCssClass);
            Rules.RegisterOnInitRule<Heading, H1, H2, H3, H4, H5, H6>(x => x.DefaultCssClass = HeadingCssClass);
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            if (!EnableRendering) return;

            builder.OpenElement(HtmlTags.DIV);
            builder.AddRoleAttribute(Role);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.AddContent(DismissButtonRenderFragment);
            builder.CloseElement();
        }

        private void DismissButtonRenderFragment(RenderTreeBuilder builder)
        {
            if (!Dismissable) return;

            builder.OpenElement(HtmlTags.BUTTON);
            builder.AddClassAttribute(CloseCssClass);
            builder.AddAttribute(HtmlAttrs.TYPE, DismissButtonType);
            builder.AddAriaLabelAttribute(DismissAriaLabel);
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Dismiss));
            builder.OpenElement(HtmlTags.SPAN);
            builder.AddAriaHiddenAttribute();
            builder.AddContent(DismissText);
            builder.CloseElement();
            builder.CloseElement();
        }
    }
}
