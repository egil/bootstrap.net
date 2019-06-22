using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public class DeckTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Deck renderes as div with class 'card-deck'")]
        public void MyTestMethoddsfasdf()
        {
            var expectedHtml = $@"<div class=""card-deck""></div>";

            var result = RenderComponent<Deck>();

            result.ShouldBe(expectedHtml);
        }
    }
}
