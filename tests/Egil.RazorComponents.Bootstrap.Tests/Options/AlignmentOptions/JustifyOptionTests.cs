using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Shouldly;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;
using static Egil.RazorComponents.Bootstrap.Options.SpacingOptions.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.AlignmentOptions
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