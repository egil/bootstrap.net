using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class P : BootstrapHtmlElementComponentBase
    {
        /// <summary>
        /// Gets or sets the padding of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        /// <summary>
        /// Gets or sets the margin of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        public P()
        {
            DefaultElementName = HtmlTags.P;
        }
    }
}
