using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Options.SpacingOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Shouldly;
using System;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;
using static Egil.RazorComponents.Bootstrap.Options.SpacingOptions.Factory.LowerCase;

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
            new SpacingSide(SpacingSideType.Top).Value.ShouldBe("t");
            new SpacingSide(SpacingSideType.Bottom).Value.ShouldBe("b");
            new SpacingSide(SpacingSideType.Left).Value.ShouldBe("l");
            new SpacingSide(SpacingSideType.Right).Value.ShouldBe("r");
            new SpacingSide(SpacingSideType.Horizontal).Value.ShouldBe("x");
            new SpacingSide(SpacingSideType.Vertical).Value.ShouldBe("y");
        }

        [Theory(DisplayName = "A side and a size can be combined into a spacing option")]
        [NumberRangeData(-5, 5)]
        public void SideAndSizeCombines(int size)
        {
            var side = left;
            var spacing = side - size;
            spacing.Value.ShouldBeCombinationOf(side, (Number)size);
        }

        [Fact(DisplayName = "A side and auto option can be combined into a spacing option")]
        public void SideAndAutoCombines()
        {
            var side = right;
            var spacing = side - auto;
            spacing.Value.ShouldBeCombinationOf(side, auto);
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
            spacing.Value.ShouldBeCombinationOf(side, breakpoint, (Number)size);
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

