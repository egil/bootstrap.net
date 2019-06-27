using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Shouldly;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.AlignmentOptions
{
    public class AlignmentOptionTests
    {
        [Fact(DisplayName = "Alignment option returns correct css class")]
        public void AlignOptionReturnsCorrectCssClass()
        {
            new AlignmentOption(AlignmentType.Start).Value.ShouldBe("start");
            new AlignmentOption(AlignmentType.End).Value.ShouldBe("end");
            new AlignmentOption(AlignmentType.Center).Value.ShouldBe("center");
        }

        [Fact(DisplayName = "Breakpoint can be combined with alignment option using - operator")]
        public void BreakpointWithAlignOption()
        {
            var bp = md;
            var align = center;
            var bpa = bp - align;
            bpa.Value.ShouldBeCombinationOf(bp, align);
        }
    }
}