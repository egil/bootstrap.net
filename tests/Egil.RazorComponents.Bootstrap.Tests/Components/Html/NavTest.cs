using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public class NavTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Nav renders correctly without any parameters")]
        public void MyTestMethod()
        {
            var expectedHtml = $@"<nav></nav>";

            var result = RenderComponent<Nav>();

            result.ShouldBe(expectedHtml);
        }
    }
}
