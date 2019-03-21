using Shouldly;
using System;
using System.Text;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests
{

    public class BreakpointTests
    {
        [Fact]
        public void CssClassReturnsCorrectBootstrapBreakpoint()
        {
            new Breakpoint(BreakpointType.None).CssClass.ShouldBe("");
            new Breakpoint(BreakpointType.Small).CssClass.ShouldBe("sm");
            new Breakpoint(BreakpointType.Medium).CssClass.ShouldBe("md");
            new Breakpoint(BreakpointType.Large).CssClass.ShouldBe("lg");
            new Breakpoint(BreakpointType.ExtraLarge).CssClass.ShouldBe("xl");
        }

        [Fact]
        public void BreakpointCanHaveWidthAddedViaMinusOperator()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var span = 2;
            var bpw = bp - span;
            bpw.CssClass.ShouldBe($"{bp.CssClass}-{span}");
        }

        [Fact]
        public void NoneBreakpointCannotHaveWidth()
        {
            Should.Throw<InvalidOperationException>(() => new BreakpointWithSpan(BreakpointType.None, 2));
        }

        [Fact]
        public void BreakpoingWithFirst()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var first = new FirstOption();
            var bpf = bp - first;
            bpf.CssClass.ShouldBe($"{bp.CssClass}-{first.CssClass}");
        }

        [Fact]
        public void BreakpoingWithLast()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var last = new LastOption();
            var bpf = bp - last;
            bpf.CssClass.ShouldBe($"{bp.CssClass}-{last.CssClass}");
        }
    }
}