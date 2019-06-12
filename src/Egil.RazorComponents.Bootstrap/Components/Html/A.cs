using Egil.RazorComponents.Bootstrap.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    // TODO: Copy all properties to NavLink with p6
    public sealed class A : BootstrapParentComponentBase
    {
        [Parameter]
        public string? Href { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<NavLink>();
            builder.AddClassAttribute(CssClassValue);
            builder.AddAttribute("href", Href);
            builder.AddAttribute(RenderTreeBuilder.ChildContent, ChildContent);
            builder.CloseComponent();
        }
    }
}
