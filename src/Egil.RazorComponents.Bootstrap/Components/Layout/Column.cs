using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Layout
{
    public sealed class Column : BootstrapParentComponentBase
    {
        [Parameter]
        public SpanParameter Span { get; set; } = SpanParameter.Default;

        [Parameter]
        public OrderParameter Order { get; set; } = OrderParameter.None;

        [Parameter]
        public OffsetParameter Offset { get; set; } = OffsetParameter.None;

        [Parameter]
        public AlignmentParameter<VerticalColumnAlignment> VerticalAlign { get; set; } = AlignmentParameter<VerticalColumnAlignment>.None;

        [Parameter]
        public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        [Parameter]
        public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;
    }
}
