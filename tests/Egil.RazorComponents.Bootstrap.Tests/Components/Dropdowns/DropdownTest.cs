using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components.Dropdowns.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Egil.RazorComponents.Testing;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase;
using static Egil.RazorComponents.Bootstrap.Utilities.Colors.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Components.Dropdowns
{
    public class DropdownTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "When text is null and split is false, no buttons are rendered (assume one is provided)")]
        public void MyTestMethod01()
        {
            var expectedHtml = $@"<div class=""dropdown""></div>";
            var component = Component<Dropdown>();

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "A dropdown with Text property set renders a toggle button")]
        public void MyTestMethod12341()
        {
            var text = "Lorem lipsum";
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      {text}
                                    </button>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Text", text));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting Split to true rendered a dedicated dropdown toggle with a caret and css class dropdown-toggle-split " +
                            "and an attached button with text from Text parameter." +
                            "Color settings are shared between the two buttons")]
        public void MyTestMethod2312()
        {
            ColorParameter<ButtonColor> color = secondary;
            var text = "TEXT TEXT";
            var expectedHtml = $@"<div class=""btn-group"">
                                    <button class=""btn {color.First()}"" type=""button"">{text}</button>
                                    <button class=""btn dropdown-toggle dropdown-toggle-split {color.First()}"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      <span class=""sr-only"">Toggle Dropdown</span>
                                    </button>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Split", true), ("Text", text), ("Color", color));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting SrOnlyToggleText sets the screen reader text of the toggle button")]
        public void MyTestMethod1_1()
        {
            var text = "text text";
            var srText = "TEST TEST";
            var expectedHtml = $@"<div class=""btn-group"">
                                    <button class=""btn"" type=""button"">{text}</button>
                                    <button class=""btn dropdown-toggle dropdown-toggle-split"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      <span class=""sr-only"">{srText}</span>
                                    </button>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Text", text), ("SrOnlyToggleText", srText), ("Split", true));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "The color param changes the color of the toggle button")]
        public void MyTestMethod2()
        {
            ColorParameter<ButtonColor> color = secondary;
            var text = "lorem lipsum";
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle {color.First()}"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      {text}
                                    </button>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Color", color), ("Text", text));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "A dropdown without any options and a BUTTON component as first child modifies button to be the toggle button")]
        public void MyTestMethod1()
        {
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"" />
                                  </div>";
            var component = Component<Dropdown>().WithChildContent(Fragment<Button>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "A dropdown without any options and a A component as first child modifies button to be the toggle button")]
        public void MyTestMethod231231()
        {
            var expectedHtml = $@"<div class=""dropdown"">
                                    <a class=""btn dropdown-toggle"" role=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"" />
                                  </div>";
            var component = Component<Dropdown>().WithChildContent(Fragment<A>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "When a single BUTTON (first) component is provided and split is true, button is rendered as two button with second button as caret toggle button")]
        public void MyTestMethod1234()
        {
            var expectedHtml = $@"<div class=""btn-group"">
                                    <button class=""btn"" type=""button""></button>
                                    <button class=""btn dropdown-toggle dropdown-toggle-split"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      <span class=""sr-only"">Toggle Dropdown</span>
                                    </button>
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Split", true))
                .WithChildContent(Fragment<Button>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Color param is passed to child button")]
        public void MyTestMethod44()
        {
            ColorParameter<ButtonColor> color = secondary;
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle {color.First()}"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"" />
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Color", color))
                .WithChildContent(Fragment<Button>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Size param is set on built-in buttons")]
        public void MyTestMethod33()
        {
            SizeParamter<ButtonSize> size = SizeParamter<ButtonSize>.Large;
            var text = "Lorem Textum";
            var expectedHtml = $@"<div class=""btn-group"">
                                    <button class=""btn {size.First()}"" type=""button"">{text}</button>
                                    <button class=""btn dropdown-toggle dropdown-toggle-split {size.First()}"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      <span class=""sr-only"">Toggle Dropdown</span>
                                    </button>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Text", text), ("Split", true), ("Size", size));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Size param is passed to provided button")]
        public void MyTestMethod414214()
        {
            SizeParamter<ButtonSize> size = SizeParamter<ButtonSize>.Large;
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle {size.First()}"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"" />
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Size", size))
                .WithChildContent(Fragment<Button>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Size param is passed to provided A component")]
        public void MyTestMethod41421432()
        {
            SizeParamter<ButtonSize> size = SizeParamter<ButtonSize>.Large;
            var expectedHtml = $@"<div class=""dropdown"">
                                    <a class=""btn dropdown-toggle {size.First()}"" role=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"" />
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Size", size))
                .WithChildContent(Fragment<A>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "When a single A (first) component is provided and split is true, button is rendered as two button with second button as caret toggle button")]
        public void MyTestMethod12321334()
        {
            var expectedHtml = $@"<div class=""btn-group"">
                                    <a class=""btn""></a>
                                    <button class=""btn dropdown-toggle dropdown-toggle-split"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      <span class=""sr-only"">Toggle Dropdown</span>
                                    </button>
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Split", true))
                .WithChildContent(Fragment<A>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Color param is passed to child button")]
        public void MyTestMethod312344()
        {
            ColorParameter<ButtonColor> color = secondary;
            var expectedHtml = $@"<div class=""dropdown"">
                                    <a class=""btn dropdown-toggle {color.First()}"" role=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"" />
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Color", color))
                .WithChildContent(Fragment<A>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Size param is passed to provided button and set on split toggle button")]
        public void MyTestMethod41421423()
        {
            SizeParamter<ButtonSize> size = SizeParamter<ButtonSize>.Large;
            var expectedHtml = $@"<div class=""btn-group"">
                                    <button class=""btn {size.First()}"" type=""button""></button>
                                    <button class=""btn dropdown-toggle dropdown-toggle-split {size.First()}"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      <span class=""sr-only"">Toggle Dropdown</span>
                                    </button>
                                  </div>";
            var component = Component<Dropdown>()
                .WithParams(("Split", true), ("Size", size))
                .WithChildContent(Fragment<Button>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        public static IEnumerable<object[]> DirectionOptions = new object[][]
        {
            new object[]{ up, "btn-group dropup"},
            new object[]{ down, "dropdown"},
            new object[]{ left, "btn-group dropleft"},
            new object[]{ right, "btn-group dropright"},
        };

        [Theory(DisplayName = "Setting Direction toggles the drop down direction")]
        [MemberData(nameof(DirectionOptions))]
        public void MyTestMethod324(dynamic direction, string expectedCssClass)
        {
            DirectionParameter directionParam = direction;
            var expectedHtml = $@"<div class=""{expectedCssClass}""></div>";
            var component = Component<Dropdown>().WithParams(("Direction", directionParam));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "A nested MENU component automatically gets the dropdown-menu class set as default")]
        public void MyTestMethodasasd()
        {
            var text = "Lorem lipsum";
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      {text}
                                    </button>
                                    <div class=""dropdown-menu"" aria-labelledby=""RegEx:^dropdown-toggle""></div>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Text", text)).WithChildContent(Fragment<Menu>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "A nested FORM component automatically gets the dropdown-menu class set as default")]
        public void MyTestMethodasas23()
        {
            var text = "Lorem lipsum";
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      {text}
                                    </button>
                                    <form class=""dropdown-menu"" aria-labelledby=""RegEx:^dropdown-toggle""></form>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Text", text)).WithChildContent(Fragment<Form>());

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "A, BUTTON, SPAN, HR, HEADINGS nested in a MENU automatically gets appropriate css classes as default")]
        public void MyTestMetho23412345d()
        {
            var text = "Lorem lipsum";
            var expectedHtml = $@"<div class=""dropdown"">
                                    <button class=""btn dropdown-toggle"" type=""button"" id=""RegEx:^dropdown-toggle"" aria-haspopup=""true"" aria-expanded=""false"">
                                      {text}
                                    </button>
                                    <div class=""dropdown-menu"" aria-labelledby=""RegEx:^dropdown-toggle"">
                                      <a class=""dropdown-item""></a>
                                      <button class=""dropdown-item"" type=""button""></button>
                                      <span class=""dropdown-item-text""></span>
                                      <div class=""dropdown-divider"" />
                                      <h1 class=""dropdown-header""></h1>
                                      <h2 class=""dropdown-header""></h2>
                                      <h3 class=""dropdown-header""></h3>
                                      <h4 class=""dropdown-header""></h4>
                                      <h5 class=""dropdown-header""></h5>
                                      <h6 class=""dropdown-header""></h6>
                                    </div>
                                  </div>";
            var component = Component<Dropdown>().WithParams(("Text", text))
                .WithChildContent(Fragment<Menu>().WithChildContent(
                    Fragment<A>(),
                    Fragment<Button>(),
                    Fragment<Span>(),
                    Fragment<Hr>(),
                    Fragment<H1>(),
                    Fragment<H2>(),
                    Fragment<H3>(),
                    Fragment<H4>(),
                    Fragment<H5>(),
                    Fragment<H6>()
                    )
                );

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
