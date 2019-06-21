using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class Header : BootstrapHtmlElementComponentBase
    {
        internal string? CustomHtmlTag { get; set; }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(CustomHtmlTag ?? HtmlTags.HEADER);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }

    public sealed class Footer : BootstrapHtmlElementComponentBase
    {
        internal string? CustomHtmlTag { get; set; }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(CustomHtmlTag ?? HtmlTags.FOOTER);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }
}
