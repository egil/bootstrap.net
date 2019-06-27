using Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Utilities.Colors.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts
{
    public class AlertTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "Alert render with no parameters")]
        public void MyTestMethod()
        {
            var expectedHtml = $@"<div class=""alert fade show"" role=""alert""></div>";

            var result = Component<Alert>().Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Alert adds color when specified")]
        public void MyTestMethod2()
        {
            var expectedHtml = $@"<div class=""alert fade show alert-primary"" role=""alert""></div>";

            var component = Component<Alert>().WithParams(("Color", (ColorParameter<AlertColor>)primary));

            var result = component.Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Providing a role overrides default role value")]
        public void MyTestMethod3()
        {
            var role = "ALERT";
            var expectedHtml = $@"<div class=""alert fade show"" role=""{role}""></div>";

            var result = Component<Alert>().WithParams(("Role", role)).Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting Dismisasable to true renderes dismiss button")]
        public void MyTestMethod4()
        {
            var expectedHtml = $@"<div class=""alert fade show alert-dismissible"" role=""alert"">
                                    <button type=""button"" class=""close"" aria-label=""Close"">
                                      <span aria-hidden=""true"">&amp;times;</span>
                                    </button>
                                  </div>";

            var result = Component<Alert>().WithParams(("Dismissable", true)).Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Setting DismissAriaLabel and DismissText overrides defaults")]
        public void MyTestMethod42()
        {
            var dismissAriaLabel = "DISMISSARIALABEL";
            var dismissText = "DISMISSTEXT";
            var expectedHtml = $@"<div class=""alert fade show alert-dismissible"" role=""alert"">
                                    <button type=""button"" class=""close"" aria-label=""{dismissAriaLabel}"">
                                      <span aria-hidden=""true"">{dismissText}</span>
                                    </button>
                                  </div>";

            var result = Component<Alert>().WithParams(
                ("Dismissable", true),
                ("DismissAriaLabel", dismissAriaLabel),
                ("DismissText", dismissText)
            ).Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Nested A components have their DefaultCssClass set to alert-link")]
        public void MyTestMethod5()
        {
            var expectedHtml = $@"<div class=""alert fade show"" role=""alert"">
                                    <a class=""alert-link""></a>
                                  </div>";

            var result = Component<Alert>().WithChildContent(Fragment<A>()).Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "Nested Heading components have their DefaultCssClass set to alert-heading")]
        public void MyTestMethod6()
        {
            var expectedHtml = $@"<div class=""alert fade show"" role=""alert"">
                                    <h1 class=""alert-heading""></h1>
                                  </div>";

            var result = Component<Alert>().WithChildContent(Fragment<H1>()).Render();

            result.ShouldBe(expectedHtml);
        }

        // TODO: Add tests for event handling, State, Dismiss(), and Show()
    }
}
