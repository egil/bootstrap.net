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

        [Fact(Skip ="TODO: Figure out how to compare generated IDs", 
              DisplayName = "Accordion will turn card>header into a clickable item that toggles the card in the accordion")]
        public void MyTestMethod2()
        {

            var expectedHtml = $@"<div class=""accordion"">
                                    <div class=""card-header"" role=""button"" aria-expanded=""false"" aria-controls=""""></div>
                                    <div  class=""collapse"" aria-labelledby="""">
                                      <div class=""card-body""></div>
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
    }
}
