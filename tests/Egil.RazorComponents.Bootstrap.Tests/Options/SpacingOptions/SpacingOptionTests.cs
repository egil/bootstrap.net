using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Shouldly;
using System;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.SpacingOptions
{
    /// <summary>
    /// {size}
    /// {sides}-{size}
    /// {breakpoint}-{size} //
    /// {sides}-{breakpoint}-{size}
    /// 
    /// sides: t|b|l|r|x|y
    /// size: 0|1|2|3|4|5|auto
    /// negative size: -1|-2|-3|-4|-5
    /// </summary>
    public class SpacingOptionTests
    {
        [Fact(DisplayName = "Spacing sides returns correct value based on side type")]
        public void CssClassReturnsCorrectBootstrapBreakpoint()
        {
            ((ISideOption)top).Value.ShouldBe("t");
            ((ISideOption)bottom).Value.ShouldBe("b");
            ((ISideOption)left).Value.ShouldBe("l");
            ((ISideOption)right).Value.ShouldBe("r");
            ((ISideOption)horizontal).Value.ShouldBe("x");
            ((ISideOption)vertical).Value.ShouldBe("y");
        }

        [Theory(DisplayName = "A side and a size can be combined into a spacing option")]
        [NumberRangeData(-5, 5)]
        public void SideAndSizeCombines(int size)
        {
            var side = left;
            var spacing = side - size;
            spacing.Value.ShouldBeCombinationOf("l", (Number)size);
        }

        [Fact(DisplayName = "A side and auto option can be combined into a spacing option")]
        public void SideAndAutoCombines()
        {
            var side = right;
            var spacing = side - auto;
            spacing.Value.ShouldBeCombinationOf("r", auto);
        }

        [Theory(DisplayName = "Combining a side with an invalid size number throws")]
        [InlineData(-6)]
        [InlineData(6)]
        public void CanHaveSideWithSizeThrowsWithInvalidSize(int invalidSize)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => left - invalidSize);
        }

        [Theory(DisplayName = "A side, breakpoint, and a size can be combined into a spacing option")]
        [NumberRangeData(-5, 5)]
        public void SideAndBreakpointAndSideCombines(int size)
        {
            var side = right;
            var breakpoint = md;
            var spacing = side - breakpoint - size;
            spacing.Value.ShouldBeCombinationOf("r", breakpoint, (Number)size);
        }

        [Theory(DisplayName = "Combining a breakpoint, side with an invalid size number throws")]
        [InlineData(-6)]
        [InlineData(6)]
        public void CanHaveBreakpointWithSizeThrowsWithInvalidSize(int invalidSize)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => left - md - invalidSize);
        }

    }
}

