using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Shouldly;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;
using static Egil.RazorComponents.Bootstrap.Utilities.Spacing.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.OrderOptions
{
    public class OrderOptionsTests
    {
        [Fact(DisplayName = "First returns 'first' as value")]
        public void FirstOptionReturnsCorrectCssClass()
        {
            new FirstOption().Value.ShouldBe("first");
        }

        [Fact(DisplayName = "Last returns 'last' as value")]
        public void LastOptionReturnsCorrectCssClass()
        {
            new LastOption().Value.ShouldBe("last");
        }

        [Fact(DisplayName = "Breakpoint can be combined with first option via - operator")]
        public void BreakpoingWithFirst()
        {
            var bp = lg;
            var bpf = bp - first;
            bpf.Value.ShouldBeCombinationOf(bp, first);
        }

        [Fact(DisplayName = "Breakpoint can be combined with last option via - operator")]
        public void BreakpoingWithLast()
        {
            var bp = md;
            var bpl = bp - last;
            bpl.Value.ShouldBeCombinationOf(bp, last);
        }
    }
}
