using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Utilities
{
    public class UrlHashUtilityTest
    {
        [Theory(DisplayName ="Given a href value and a relativeUrl, a new relativeUrl will be generated with the href appended to it, if it is a hash-only url")]
        [InlineData("", "", "")]
        [InlineData("#", "", "#")]
        [InlineData("#", "#", "#")]
        [InlineData("", "/", "/")]
        [InlineData("#", "/", "/#")]
        [InlineData("#", "/#", "/#")]
        [InlineData("#example", "", "#example")]
        [InlineData("#example", "#", "#example")]
        [InlineData("#example", "#p", "#example")]
        [InlineData("#example", "/", "/#example")]
        [InlineData("#example", "/#", "/#example")]
        [InlineData("#example", "/#p", "/#example")]
        [InlineData("#example", "/page", "/page#example")]
        [InlineData("#example", "/page#", "/page#example")]
        [InlineData("#example", "/page#p", "/page#example")]
        public void GenerateRelativeUrlWithHashTest(string href, string currentRelativeUrl, string expectedRelativeUrl)
        {
            var actual = href.GenerateRelativeUrlWithHash(currentRelativeUrl);
            actual.ShouldBe(expectedRelativeUrl);
        }
    }
}
