using System.Text;
using Org.XmlUnit.Diff;
using Xunit;
using Xunit.Abstractions;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Accessibility
{

    public class SrOnlyTests : BootstrapComponentBaseTests
    {
        [Fact(DisplayName = "SrOnly does not render anything when ChildContent is null")]
        public void MyTestMethod()
        {
            var expectedHtml = string.Empty;

            var result = RenderComponent<SrOnly>(ParameterCollection.Empty);

            result.ShouldBe(expectedHtml);
        }

        [Fact(DisplayName = "SrOnly renders if ChildContent is not null")]
        public void SrOnlyRenderCorrectlysIfChildContentIsNotNull()
        {
            var content = "CONTENT";
            var expectedHtml = $@"<span class=""sr-only"">{content}</span>";            

            var result = RenderComponent<SrOnly>(builder => builder.AddContent(content));

            result.ShouldBe(expectedHtml);
        }
    }
}
