using Egil.RazorComponents.Bootstrap.Base;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public class Nav : BootstrapHtmlElementComponentBase
    {
        [Parameter] public bool Pills { get; set; } = false;

        public Nav()
        {
            DefaultElementName = HtmlTags.NAV;
        }
    }
}
