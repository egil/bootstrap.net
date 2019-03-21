using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class StaticOptionTests
    {
        [Fact]
        public void FirstOptionReturnsCorrectCssClass()
        {
            new FirstOption().CssClass.ShouldBe("first");
        }

        [Fact]
        public void LastOptionReturnsCorrectCssClass()
        {
            new LastOption().CssClass.ShouldBe("last");
        }
    }
}