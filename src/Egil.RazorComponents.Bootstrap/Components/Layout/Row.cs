using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Layout
{
    public sealed class Row : BootstrapParentComponentBase
    {
        private const string RowCssClass = "row";

        public Row()
        {
            DefaultCssClass = RowCssClass;
        }

        [Parameter, CssClassToggleParameter("no-gutters")] public bool NoGutters { get; set; }

        [Parameter] public AlignmentParameter<VerticalRowAlignment> VerticalAlign { get; set; } = AlignmentParameter<VerticalRowAlignment>.None;

        [Parameter] public HorizontalAlignmentParameter Align { get; set; } = HorizontalAlignmentParameter.None;

        [Parameter] public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        [Parameter] public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;
    }
}
