using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public class NavTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Nav renders correctly without any parameters")]
        public void MyTestMethod()
        {
            var expectedHtml = $@"<nav></nav>";

            var result = Component<Nav>().Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
