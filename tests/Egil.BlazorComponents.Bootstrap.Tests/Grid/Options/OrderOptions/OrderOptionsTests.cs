using System;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
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
            bpf.Value.ShouldBe($"{bp.Value}-{first.Value}");
            bpf.ShouldBeOfType<BreakpointFirst>();
        }

        [Fact(DisplayName = "Breakpoint can be combined with last option via - operator")]
        public void BreakpoingWithLast()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var last = new LastOption();
            var bpl = bp - last;
            bpl.Value.ShouldBe($"{bp.Value}-{last.Value}");
            bpl.ShouldBeOfType<BreakpointLast>();
        }
    }
}
