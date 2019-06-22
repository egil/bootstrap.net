using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class Svg : BootstrapHtmlElementComponentBase
    {
        public Svg()
        {
            DefaultElementName = HtmlTags.SVG;
        }
    }
}
