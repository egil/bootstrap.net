using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components.Cards;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    public class AccordionTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Accordion has accordion class added")]
        public void MyTestMethod1()
        {
            var expectedHtml = @"<div class=""accordion""></div>";
            var component = Component<Accordion>();

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Accordion will turn card>header into a clickable item that toggles the card in the accordion")]
        public void MyTestMethod2()
        {
            var expectedHtml = $@"<div class=""accordion"">
                                    <div class=""card"">
                                      <div class=""card-header"" id=""RegEx:^header-card"" role=""heading"">
                                        <button aria-expanded=""true"" aria-controls=""RegEx:^body-card"" type=""button"" class=""btn""></button>
                                      </div>
                                      <div class=""collapse show"" id=""RegEx:^body-card"" aria-labelledby=""RegEx:^header-card"">
                                        <div class=""card-body""></div>
                                      </div>
                                    </div>
                                  </div>";
            var component = Component<Accordion>().WithChildContent(
                Fragment<Card>().WithChildContent(
                    Fragment<Header>(),
                    Fragment<Content>()
                )
            );

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Accordion with ExpandedIndex set has the index expanded on init")]
        public void MyTestMethod3()
        {
            var expectedHtml = $@"<div class=""accordion"">
                                    <div class=""card"">
                                      <div class=""card-header"" id=""RegEx:^header-card"" role=""heading"">
                                        <button aria-expanded=""false"" aria-controls=""RegEx:^body-card"" type=""button"" class=""btn""></button>
                                      </div>
                                      <div class=""collapse"" id=""RegEx:^body-card"" aria-labelledby=""RegEx:^header-card"">
                                        <div class=""card-body""></div>
                                      </div>
                                    </div>
                                    <div class=""card"">
                                      <div class=""card-header"" id=""RegEx:^header-card"" role=""heading"">
                                        <button aria-expanded=""true"" aria-controls=""RegEx:^body-card"" type=""button"" class=""btn""></button>
                                      </div>
                                      <div class=""collapse show"" id=""RegEx:^body-card"" aria-labelledby=""RegEx:^header-card"">
                                        <div class=""card-body""></div>
                                      </div>
                                    </div>
                                  </div>";
            var component = Component<Accordion>().WithParams(("ExpandedIndex", "1")).WithChildContent(
                Fragment<Card>().WithChildContent(
                    Fragment<Header>(),
                    Fragment<Content>()
                ),
                Fragment<Card>().WithChildContent(
                    Fragment<Header>(),
                    Fragment<Content>()
                )
            );

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
