using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.AlignmentOptions
{
    public class JustifyOptionTests
    {
        [Fact(DisplayName = "Justify option returns correct css class")]
        public void AlignOptionReturnsCorrectCssClass()
        {
            new JustifyOption(JustifyType.Between).Value.ShouldBe("between");
            new JustifyOption(JustifyType.Around).Value.ShouldBe("around");
        }

        [Fact(DisplayName = "Breakpoint can be combined with justify option using - operator")]
        public void BreakpointWithAlignOption()
        {
            var bp = md;
            var justify = around;
            var bpa = bp - justify;
            bpa.Value.ShouldBeCombinationOf(bp, justify);
        }
    }
}