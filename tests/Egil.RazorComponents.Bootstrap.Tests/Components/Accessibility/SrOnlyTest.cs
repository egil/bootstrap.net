using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Accessibility
{
    public class SrOnlyTest : BootstrapComponentFixture
    {
        [Fact(DisplayName = "SrOnly does not render anything when ChildContent is null")]
        public void MyTestMethod()
        {
            var expectedHtml = string.Empty;

            var result = Component<SrOnly>().Render();

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "SrOnly renders if ChildContent is not null")]
        public void SrOnlyRenderCorrectlysIfChildContentIsNotNull()
        {
            var content = "CONTENT";
            var expectedHtml = $@"<span class=""sr-only"">{content}</span>";            

            var result = Component<SrOnly>().WithChildContent(content).Render();

            result.ShouldBe(expectedHtml);
        }
    }
}
