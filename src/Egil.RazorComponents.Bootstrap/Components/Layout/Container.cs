using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Layout
{
    public sealed class Container : BootstrapParentComponentBase
    {
        [Parameter, CssClassToggleParameter("container-fluid", "container")]
        public bool Fluid { get; set; } = false;

        [Parameter]
        public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        [Parameter]
        public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;
    }
}
