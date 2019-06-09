using System;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Common;
using Egil.RazorComponents.Bootstrap.Helpers;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Utilities.Animations;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts
{

    public sealed class Alert : BootstrapParentComponentBase
    {
        private const string AlertCssClass = "alert";
        private const string AlertRole = "alert";
        private bool _buildRenderTree = true; // TODO replace with visibility utility

        public Alert()
        {
            DefaultCssClass = AlertCssClass;
            Role = AlertRole;
            Color = OptionsFactory.Primary;
            IsDismissable = DismissableParameter.Default;
            DismissAnimation = NoneAnimation.Instance;
        }

        protected override void OnBootstrapComponentInit()
        {
            if ((bool)IsDismissable) DismissAnimation = new FadeOutAnimation();
        }

        #region Common parameters

        [Parameter]
        public string Role { get; set; }

        [Parameter]
        public SpacingParameter<PaddingSpacing>? Padding { get; set; }

        [Parameter]
        public SpacingParameter<MarginSpacing>? Margin { get; set; }

        #endregion

        [Parameter]
        public ColorParameter<AlertColor> Color { get; set; }

        [Parameter]
        public DismissableParameter IsDismissable { get; set; }

        [Parameter]
        public string DismissAriaLabel { get; set; } = "Close";

        [Parameter]
        public EventCallback<UIHidingEventArgs> OnDismissing { get; set; }

        [Parameter]
        public EventCallback<UIEventArgs> OnDismissed { get; set; }

        private ICssClassAnimation DismissAnimation { get; set; }

        protected override void RegisterChildContextRules()
        {
            ChildContext.RegisterRule<A>(x => x.DefaultCssClass = "alert-link");
            ChildContext.RegisterRule<Heading>(x => x.DefaultCssClass = "alert-heading");
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (!_buildRenderTree)
            {
                Console.WriteLine($"SKIPPING RENDERING: {nameof(Alert)}");
                return;
            }
            Console.WriteLine($"RENDERING: {nameof(Alert)}");

            builder.OpenElement(Html.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddRoleAttribute(Role);
            builder.AddContent(ChildContent);
            BuildDismissableButtonRenderTree(builder);
            builder.CloseElement();
        }

        /**
         * <button type="button" class="close" data-dismiss="alert" aria-label="Close">
         *   <span aria-hidden="true">&times;</span>
         * </button>
         **/
        private void BuildDismissableButtonRenderTree(RenderTreeBuilder builder)
        {
            if (!ShouldRenderDismissButton()) return;

            builder.OpenElement(Html.BUTTON);
            builder.AddClassAttribute("close");
            builder.AddAriaLabelAttribute(DismissAriaLabel);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create<UIMouseEventArgs>(this, DismissButtonClicked));

            builder.OpenElement(Html.SPAN);
            builder.AddAriaHiddenAttribute();
            builder.AddMarkupContent("&times;");
            builder.CloseElement();

            builder.CloseElement();

            bool ShouldRenderDismissButton()
            {
                return (bool)IsDismissable && DismissAnimation.Ready;
            }
        }

        private async void DismissButtonClicked(UIMouseEventArgs e)
        {
            var hidingEvent = new UIHidingEventArgs();
            await OnDismissing.InvokeAsync(hidingEvent);
            if (hidingEvent.Cancel) return;

            _buildRenderTree = hidingEvent.KeepInRenderTree;
            await DismissAnimation.Run();
            await OnDismissed.InvokeAsync(UIEventArgs.Empty);
        }
    }
}
