using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Moq;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public class BootstrapParentComponentBaseTest
    {

        [Fact(DisplayName = "When no ChildContent is set, nothing is rendered", 
            Skip = "Need way to fake RenderTreeBuilder")]
        public void MyTestMethod()
        {
        }

        [Fact(DisplayName = "Given ChildContent is set, parent is null, and no child context exists" +
                            "When ChildContent renderfragment is rendered" +
                            "Then set ChildContent is passed to renderer", 
            Skip = "Need way to fake RenderTreeBuilder")]
        public void MyTestMethod2()
        {
        }
    }
}
