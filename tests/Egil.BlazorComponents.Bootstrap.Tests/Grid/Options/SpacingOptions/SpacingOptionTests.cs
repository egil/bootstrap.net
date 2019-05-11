using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;
using Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions.Factory.LowerCase;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.SpacingOptions
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

        [Theory(DisplayName = "Combining a side with an invalid size number throws")]
        [InlineData(-6)]
        [InlineData(6)]
        public void CanHaveBreakpointWithSizeThrowsWithInvalidSize(int invalidSize)
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
    }
}

