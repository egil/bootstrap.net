using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class BreakpointTests
    {
        [Fact(DisplayName = "Breakpoint returns correct value based on breakpoint type")]
        public void CssClassReturnsCorrectBootstrapBreakpoint()
        {
            new Breakpoint(BreakpointType.Small).Value.ShouldBe("sm");
            new Breakpoint(BreakpointType.Medium).Value.ShouldBe("md");
            new Breakpoint(BreakpointType.Large).Value.ShouldBe("lg");
            new Breakpoint(BreakpointType.ExtraLarge).Value.ShouldBe("xl");
        }

        [Fact(DisplayName = "Breakpoint can have width specified via - operator")]
        public void BreakpointCanHaveWidthSpecifiedViaMinusOperator()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var span = 2;
            var bpw = bp - span;
            bpw.Value.ShouldBe($"{bp.Value}-{span}");
            bpw.ShouldBeOfType<GridBreakpoint>();
        }

        [Fact(DisplayName = "Breakpoint can be combined with auto option using - operator")]
        public void BreakpointWithAutoOption()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var auto = new AutoOption();
            var bpl = bp - auto;
            bpl.Value.ShouldBe($"{bp.Value}-{auto.Value}");
            bpl.ShouldBeOfType<BreakpointAuto>();
        }
    }
}
