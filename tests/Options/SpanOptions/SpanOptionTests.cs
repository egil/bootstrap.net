using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.SpanOptions
{
    public class SpanOptionTests
    {
        [Fact(DisplayName = "Breakpoint returns correct value based on breakpoint type")]
        public void CssClassReturnsCorrectBootstrapBreakpoint()
        {
            DefaultSizeOption.Default.Value.ShouldBe(string.Empty);
            SizeOption.Small.Value.ShouldBe("sm");
            SizeOption.Medium.Value.ShouldBe("md");
            SizeOption.Large.Value.ShouldBe("lg");
            ExtendedSizeOption.ExtraLarge.Value.ShouldBe("xl");
        }

        [Fact(DisplayName = "Auto returns 'auto' as value")]
        public void AutoOptionReturnsCorrectCssClass()
        {
            new AutoOption().Value.ShouldBe("auto");
        }

        [Fact(DisplayName = "Breakpoint can be combined with auto option using - operator")]
        public void BreakpointWithAutoOption()
        {
            var bp = SizeOption.Large;
            var auto = new AutoOption();
            var bpl = bp - auto;
            bpl.Value.ShouldBeCombinationOf(bp, auto);
        }
    }
}
