using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

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
