using Egil.RazorComponents.Bootstrap.Base;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class Figure : ParentComponentBase
    {
        public Figure()
        {
            DefaultElementTag = HtmlTags.FIGURE;
        }
    }
}
