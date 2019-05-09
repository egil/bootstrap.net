using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.OrderOptions
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
            var bp = new Breakpoint(BreakpointType.Large);
            var first = new FirstOption();
            var bpf = bp - first;
            bpf.Value.ShouldBeCombinationOf(bp, first);
        }

        [Fact(DisplayName = "Breakpoint can be combined with last option via - operator")]
        public void BreakpoingWithLast()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var last = new LastOption();
            var bpl = bp - last;
            bpl.Value.ShouldBeCombinationOf(bp, last);
        }
    }
}
