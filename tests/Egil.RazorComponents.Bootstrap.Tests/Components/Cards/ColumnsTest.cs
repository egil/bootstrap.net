using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public class ColumnsTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Columns renders as div with class 'card-columns'")]
        public void MyTestMethoddsfasdf()
        {
            var expectedHtml = $@"<div class=""card-columns""></div>";

            var result = Component<Columns>().Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
