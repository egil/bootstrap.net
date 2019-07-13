using Egil.RazorComponents.Bootstrap.Base;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class Nav : ParentComponentBase
    {
        [Parameter] public bool Pills { get; set; } = false;

        public Nav()
        {
            DefaultElementTag = HtmlTags.NAV;
        }
    }
}
