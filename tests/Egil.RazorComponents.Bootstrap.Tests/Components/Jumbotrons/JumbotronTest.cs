using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Jumbotrons
{
    public class JumbotronTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Jumbotron has correct css class added")]
        public void MyTestMethod1()  
        {
            var expectedHtml = $@"<div class=""jumbotron""></div>";
            
            var result = Component<Jumbotron>().Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Fluid Jumbotron has the correct css class added")]
        public void MyTestMethod2()
        {
            var expectedHtml = $@"<div class=""jumbotron jumbotron-fluid""></div>";

            var result = Component<Jumbotron>().WithParams(("fluid", true)).Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
