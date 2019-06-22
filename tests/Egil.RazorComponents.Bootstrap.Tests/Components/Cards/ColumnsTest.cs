using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public class ColumnsTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Columns renderes as div with class 'card-columns'")]
        public void MyTestMethoddsfasdf()
        {
            var expectedHtml = $@"<div class=""card-columns""></div>";

            var result = RenderComponent<Columns>();

            result.ShouldBe(expectedHtml);
        }
    }
}
