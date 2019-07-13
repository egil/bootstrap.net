using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public class Content : ParentComponentBase
    {
        public Content()
        {
            DefaultElementTag = HtmlTags.DIV;
        }
    }
}
