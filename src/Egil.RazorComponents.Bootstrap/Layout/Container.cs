using Egil.RazorComponents.Bootstrap.Helpers;
using Egil.RazorComponents.Bootstrap.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Layout
{
    public sealed class Container : BootstrapParentComponentBase
    {
        [Parameter]
        public ContainerTypeParameter IsFluid { get; set; } = ContainerTypeParameter.Default;

        [Parameter]
        public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        [Parameter]
        public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(Html.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }
}
