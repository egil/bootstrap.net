using Egil.RazorComponents.Bootstrap.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Components.Common
{
    public class Heading : BootstrapParentComponentBase
    {
        public static readonly string[] HeadingTags = { "h1", "h2", "h3", "h4", "h5", "h6" };
        protected int _size = 1;

        [Parameter]
        public int Size
        {
            get => _size; set
            {
                if (value < 1 || value > 6) throw new ArgumentOutOfRangeException("A HTML heading size can only between 1 and 6.");
                _size = value;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(HeadingTags[_size - 1]);
            builder.AddClassAttribute(CssClassValue);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }

    public sealed class H1 : Heading
    {
        public H1() { Size = 1; }
    }
    public sealed class H2 : Heading
    {
        public H2() { Size = 2; }
    }
    public sealed class H3 : Heading
    {
        public H3() { Size = 3; }
    }
    public sealed class H4 : Heading
    {
        public H4() { Size = 4; }
    }
    public sealed class H5 : Heading
    {
        public H5() { Size = 5; }
    }
    public sealed class H6 : Heading
    {
        public H6() { Size = 6; }
    }
}
