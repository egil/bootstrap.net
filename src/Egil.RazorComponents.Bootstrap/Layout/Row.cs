using Egil.RazorComponents.Bootstrap.Helpers;
using Egil.RazorComponents.Bootstrap.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Layout
{
    public sealed class Row : BootstrapParentComponentBase
    {
        private const string RowCssClass = "row";

        public Row()
        {
            DefaultCssClass = RowCssClass;
        }

        [Parameter]
        public NoGuttersParameter NoGutters { get; set; } = NoGuttersParameter.Default;

        [Parameter]
        public AlignmentParameter<VerticalRowAlignment> VerticalAlign { get; set; } = AlignmentParameter<VerticalRowAlignment>.None;

        [Parameter]
        public HorizontalAlignmentParameter Align { get; set; } = HorizontalAlignmentParameter.None;

        [Parameter]
        public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        [Parameter]
        public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }
}
