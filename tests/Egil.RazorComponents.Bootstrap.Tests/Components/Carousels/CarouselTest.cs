using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Testing;
using Shouldly;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Utilities.Animations.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels
{
    public class CarouselTest : BootstrapComponentFixture
    {
        private ComponentBuilder<Carousel<TItem>, TItem> BuildCarousel<TItem>()
        {
            return Component<Carousel<TItem>, TItem>();
        }

        [Fact(DisplayName = "Carousel has no controls and uses slides animation by default")]
        public void MyTestMethod1()
        {
            var expectedHtml = $@"<div class=""carousel slide""><div class=""carousel-inner""></div></div>";
            var component = BuildCarousel<string>();

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting Animation parameter to (cross)fade sets CSS class correctly")]
        public void MyTestMethod2()
        {
            var expectedHtml = $@"<div class=""carousel slide carousel-fade""><div class=""carousel-inner""></div></div>";
            var component = BuildCarousel<string>().WithParams(("Animation", fade));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "First item is marked as active by default")]
        public void MyTestMethod3()
        {
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                     <div class=""carousel-inner"">
                                        <div class=""carousel-item active""></div>
                                        <div class=""carousel-item""></div>
                                     </div>
                                  </div>";
            var component = BuildCarousel<string>().WithDefaultItems(2);

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "By setting ActiveIndex parameter, the item matching the index is marked as active")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void MyTestMethod4(ushort activeIndex)
        {
            var activeCssClass = " active";
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                     <div class=""carousel-inner"">
                                        <div class=""carousel-item{(activeIndex == 0 ? activeCssClass : string.Empty)}""></div>
                                        <div class=""carousel-item{(activeIndex == 1 ? activeCssClass : string.Empty)}""></div>
                                        <div class=""carousel-item{(activeIndex == 2 ? activeCssClass : string.Empty)}""></div>
                                     </div>
                                  </div>";
            var component = BuildCarousel<string>().WithDefaultItems(3).WithParams(("ActiveIndex", activeIndex));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "Image types in template will automatically have d-block w-100 classes added")]
        [InlineData(typeof(Img), "img")]
        [InlineData(typeof(Svg), "svg")]
        public void MyTestMethod5(Type imageType, string elementName)
        {
            var expectedHtml = $@"<div class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active"">
                                        <{elementName} class=""d-block w-100"" />
                                      </div>
                                    </div>
                                  </div>";
            var component = BuildCarousel<string>().WithDefaultItems(1).WithTemplate(tb => tb.Component(imageType));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Caption in template will automatically have .carousel-caption classes added and be rendered as DIV element")]
        public void MyTestMethod6()
        {
            var expectedHtml = $@"<div class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active"">
                                        <div class=""carousel-caption"" />
                                      </div>
                                    </div>
                                  </div>";
            var component = BuildCarousel<string>().WithItems("").WithTemplate(tb => tb.Component<Caption>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "When no template is provided and items type is string, " +
                            "then img with src=item is rendered for each item")]
        public void MyTestMethod7()
        {
            var items = new string[] { "https://img-1.jpg", "https://img-2.jpg" };
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active"">
                                        <img class=""d-block w-100"" src=""{items[0]}"" />
                                      </div>
                                      <div class=""carousel-item"">
                                        <img class=""d-block w-100"" src=""{items[1]}"" />
                                      </div>
                                    </div>
                                  </div>";
            var component = BuildCarousel<string>().WithItems(items);

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "When no template is provided and items type is (string, string), " +
                            "then img with src=item.Item1, alt=item.Item2 is rendered for each item")]
        public void MyTestMethod8()
        {
            var items = new (string, string)[] { ("https://img-1.jpg", "img-1"), ("https://img-2.jpg", "img-2") };
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active"">
                                        <img class=""d-block w-100"" src=""{items[0].Item1}"" alt=""{items[0].Item2}"" />
                                      </div>
                                      <div class=""carousel-item"">
                                        <img class=""d-block w-100"" src=""{items[1].Item1}"" alt=""{items[1].Item2}"" />
                                      </div>
                                    </div>
                                  </div>";
            var component = BuildCarousel<(string, string)>().WithItems(items);

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting ShowControls parameter renders default control buttons")]
        public void MyTestMethod9()
        {
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active""></div>
                                      <div class=""carousel-item""></div>
                                    </div>
                                    <a class=""carousel-control-prev"" role=""button"">
                                      <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
                                      <span class=""sr-only"">Previous</span>
                                    </a>
                                    <a class=""carousel-control-next"" role=""button"">
                                      <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
                                      <span class=""sr-only"">Next</span>
                                    </a>
                                  </div>";
            var component = BuildCarousel<string>().WithDefaultItems(2).WithParams(("ShowControls", true));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting SrOnly text for previous and next button replaces defaults")]
        public void MyTestMethod10()
        {
            var prevSrOnlyText = "Forrige";
            var nextSrOnlyText = "Næste";
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active""></div>
                                      <div class=""carousel-item""></div>
                                    </div>
                                    <a class=""carousel-control-prev"" role=""button"">
                                      <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
                                      <span class=""sr-only"">{prevSrOnlyText}</span>
                                    </a>
                                    <a class=""carousel-control-next"" role=""button"">
                                      <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
                                      <span class=""sr-only"">{nextSrOnlyText}</span>
                                    </a>
                                  </div>";
            var component = BuildCarousel<string>()
                .WithDefaultItems(2)
                .WithParams(
                    ("ShowControls", true),
                    ("PreviousControlSrText", prevSrOnlyText),
                    ("NextControlSrText", nextSrOnlyText)
                );

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting ShowIndicators renders indicators matching the number of items")]
        public void MyTestMethod11()
        {
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <ol class=""carousel-indicators"">
                                      <li class=""active""></li>
                                      <li></li>
                                      <li></li>
                                    </ol>
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active""></div>
                                      <div class=""carousel-item""></div>
                                      <div class=""carousel-item""></div>
                                    </div>
                                  </div>";
            var component = BuildCarousel<string>().WithDefaultItems(3).WithParams(("ShowIndicators", true));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Theory(DisplayName = "When ActiveIndex is changed, the corresponding indicators active class is set")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void MyTestMethod12(ushort activeIndex)
        {
            var activeCssClass = " active";
            var activeClassAttribute = @" class = ""active""";
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <ol class=""carousel-indicators"">
                                      <li{(activeIndex == 0 ? activeClassAttribute : string.Empty)}></li>
                                      <li{(activeIndex == 1 ? activeClassAttribute : string.Empty)}></li>
                                      <li{(activeIndex == 2 ? activeClassAttribute : string.Empty)}></li>
                                    </ol>
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item{(activeIndex == 0 ? activeCssClass : string.Empty)}""></div>
                                      <div class=""carousel-item{(activeIndex == 1 ? activeCssClass : string.Empty)}""></div>
                                      <div class=""carousel-item{(activeIndex == 2 ? activeCssClass : string.Empty)}""></div>
                                    </div>
                                  </div>";
            var component = BuildCarousel<string>()
                .WithDefaultItems(3)
                .WithParams(("ShowIndicators", true), ("ActiveIndex", activeIndex));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "If no items are provided and TItem is set to CarouselStatic, then ChildContent is rendered as is")]
        public void MyTestMethodstatic2()
        {
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active"">
                                        <img class=""d-block w-100"" />
                                      </div>
                                      <div class=""carousel-item"">
                                        <img class=""d-block w-100"" />
                                      </div>
                                    </div>
                                  </div>";

            var component = Component<Carousel<CarouselStatic>, CarouselStatic>()
                .WithTemplate(
                    Fragment<CarouselItem>().WithChildContent(
                        Fragment<Img>()
                    ),
                    Fragment<CarouselItem>().WithChildContent(
                        Fragment<Img>()
                    )
                );

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "In static content mode, indicators are rendered correctly after static content is rendered")]
        public void MyTestMethodstatic3()
        {
            var expectedHtml = $@"<div tabindex=""-1"" class=""carousel slide"">
                                    <ol class=""carousel-indicators"">
                                      <li class=""active""></li>
                                      <li></li>
                                    </ol>
                                    <div class=""carousel-inner"">
                                      <div class=""carousel-item active"">
                                        <img class=""d-block w-100"" />
                                      </div>
                                      <div class=""carousel-item"">
                                        <img class=""d-block w-100"" />
                                      </div>
                                    </div>
                                  </div>";

            var component = Component<Carousel<CarouselStatic>, CarouselStatic>()
                .WithParams(("ShowIndicators", true))
                .WithTemplate(
                    Fragment<CarouselItem>().WithChildContent(
                        Fragment<Img>()
                    ),
                    Fragment<CarouselItem>().WithChildContent(
                        Fragment<Img>()
                    )
                );

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "If the immediate children of Carousel isn't CarouselItem, an InvalidChildContent exception is thrown")]
        public void MyTestMethodstatic4()
        {
            var component = Component<Carousel<CarouselStatic>, CarouselStatic>()
                .WithTemplate(Fragment<Img>(), Fragment<Img>());

            Should.Throw<InvalidChildContentException>(() => component.Render());
        }

        //// Add data-interval="" to a .carousel-item to change the amount of time to delay between automatically cycling to the next item 
    }

}
