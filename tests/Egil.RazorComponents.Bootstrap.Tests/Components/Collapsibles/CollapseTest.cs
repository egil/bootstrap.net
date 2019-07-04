using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    public class CollapseTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Collapse is rendered with collapse css class by default")]
        public void MyTestMethod()
        {
            var expectedHtml = @"<div class=""collapse""></div>";
            var component = Component<Collapse>();

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "When Expanded is set, the css class 'show' is added to the component")]
        public void MyTestMethod2()
        {
            var expectedHtml = @"<div class=""collapse show""></div>";
            var component = Component<Collapse>().WithParams(("Expanded", true));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
