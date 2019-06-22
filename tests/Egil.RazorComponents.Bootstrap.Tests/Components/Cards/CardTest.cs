using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components.Common;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Layout;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public class CardTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Card renders correctly without any parameters")]
        public void MyTestMethod()
        {
            var expectedHtml = $@"<div class=""card""></div>";

            var result = RenderComponent<Card>();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Card > Content gets card-body set")]
        public void MyTestMethod2()
        {
            var expectedHtml = $@"<div class=""card""><div class=""card-body""></div></div>";

            var result = RenderComponent<Card>(CreateFragment<Content>());

            result.ShouldBe(expectedHtml);
        }


        [Fact(DisplayName = "Card > Content > Headings gets card-title and card-subtitle set for first and following headings")]
        public void MyTestMethod3()
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-body"">
                                      <h1 class=""card-title""></h1>
                                      <h2 class=""card-subtitle""></h2>
                                      <h3 class=""card-subtitle""></h3>
                                      <h4 class=""card-subtitle""></h4>
                                      <h5 class=""card-subtitle""></h5>
                                      <h6 class=""card-subtitle""></h6>
                                    </div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Content>(
                    CreateFragment<H1>(),
                    CreateFragment<H2>(),
                    CreateFragment<H3>(),
                    CreateFragment<H4>(),
                    CreateFragment<H5>(),
                    CreateFragment<H6>()
                )
            );

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Card > Content > P gets card-text set")]
        public void MyTestMethod4()
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-body"">
                                      <p class=""card-text""></p>
                                      <p class=""card-text""></p>
                                    </div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Content>(
                    CreateFragment<P>(),
                    CreateFragment<P>()
                )
            );

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Card > Content > A gets card-link set")]
        public void MyTestMethod5()
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-body"">
                                      <a class=""card-link""></a>
                                      <a class=""card-link""></a>
                                    </div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Content>(
                    CreateFragment<A>(),
                    CreateFragment<A>()
                )
            );

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Card > Img (first-child) gets card-img-top set")]
        [InlineData("svg", typeof(Svg))]
        [InlineData("img", typeof(Img))]
        public void MyTestMethod6(string imageElementName, Type imageType)
        {
            var expectedHtml = $@"<div class=""card"">
                                    <{imageElementName} class=""card-img-top"" />
                                    <div class=""card-body""></div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment(imageType),
                CreateFragment<Content>()
            );

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Card > Img (not first-child) gets card-img-bottom set")]
        [InlineData("svg", typeof(Svg))]
        [InlineData("img", typeof(Img))]
        public void MyTestMethod7(string imageElementName, Type imageType)
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-body""></div>
                                    <{imageElementName} class=""card-img-bottom"" />
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Content>(),
                CreateFragment(imageType)
            );

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Card > Header gets card-header set")]
        public void MyTestMethod8()
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-header""></div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Header>()
            );

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Card > Footer gets card-footer set")]
        public void MyTestMethod9()
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-footer""></div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Footer>()
            );

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Card > Headings (gets card-header set when the component is the first child")]
        [InlineData(typeof(Heading), 1)]
        [InlineData(typeof(H1), 1)]
        [InlineData(typeof(H2), 2)]
        [InlineData(typeof(H3), 3)]
        [InlineData(typeof(H4), 4)]
        [InlineData(typeof(H5), 5)]
        [InlineData(typeof(H6), 6)]
        public void MyTestMethod93213(Type headingType, int headingSize)
        {
            var expectedHtml = $@"<div class=""card"">
                                    <h{headingSize} class=""card-header""></h{headingSize}>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment(headingType)
            );

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Card.Size gets set correctly")]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(75)]
        [InlineData(100)]
        public void MyTestMethod10(int width)
        {
            var expectedHtml = $@"<div class=""card w-{width}""></div>";

            var result = RenderComponent<Card>(("Width", (NumericSizeParameter<WidthSizePrefix>)width));

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Card > Header > Nav gets {0} set")]
        [InlineData("tabs", "nav-tabs card-header-tabs")]
        [InlineData("pills", "nav-pills card-header-pills")]
        public void MyTestMethod12131231(string navKind, string expectedNavCssClasses)
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""card-header"">
                                      <nav class=""nav {expectedNavCssClasses}""></nav>
                                    </div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Header>(
                    CreateFragment<Nav>(("Pills", navKind == "pills"))
                )
            );

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Setting ImageOverlayed to true sets card-img-overlay on Content and card-img on Image")]
        [InlineData("svg", typeof(Svg))]
        [InlineData("img", typeof(Img))]
        public void MyTestMethod1233214(string imageElementName, Type imageType)
        {
            var expectedHtml = $@"<div class=""card"">
                                    <{imageElementName} class=""card-img"" />
                                    <div class=""card-img-overlay""></div>
                                  </div>";
            RenderFragment[] childContent = { CreateFragment(imageType), CreateFragment<Content>() };

            var result = RenderComponent<Card>(childContent, ("ImageOverlayed", true));

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Card > Row > Col > Image sets card-img on img")]
        public void MyTestMethoddsfasdf()
        {
            var expectedHtml = $@"<div class=""card"">
                                    <div class=""row no-gutters"">
                                      <div class=""col"">
                                        <img class=""card-img"" />
                                      </div>
                                      <div class=""col"">
                                        <div class=""card-body"">
                                          <h5 class=""card-title""></h5>
                                          <p class=""card-text""></p>
                                          <a class=""card-link""></a>
                                        </div>
                                      </div>
                                    </div>
                                  </div>";

            var result = RenderComponent<Card>(
                CreateFragment<Row>(
                    CreateFragment<Column>(
                        CreateFragment<Img>()
                    ),
                    CreateFragment<Column>(
                        CreateFragment<Content>(
                            CreateFragment<H5>(),
                            CreateFragment<P>(),
                            CreateFragment<A>()
                        )
                    )
                )
            );

            result.ShouldBe(expectedHtml);
        }
    }
}
