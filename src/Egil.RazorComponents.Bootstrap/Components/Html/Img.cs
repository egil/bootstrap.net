using Egil.RazorComponents.Bootstrap.Base;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    // TODO: Create special parent that does not render child content but is parent aware
    public sealed class Img : BootstrapHtmlElementComponentBase
    {
        public Img()
        {
            DefaultElementName = HtmlTags.IMG;
        }
    }
}
