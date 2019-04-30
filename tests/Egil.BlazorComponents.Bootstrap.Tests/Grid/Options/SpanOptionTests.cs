using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class SpanOptionTests : OptionFixture<ISpanOption>
    {
        [Fact(DisplayName = "Breakpoint returns correct value based on breakpoint type")]
        public void CssClassReturnsCorrectBootstrapBreakpoint()
        {
            new Breakpoint(BreakpointType.Small).Value.ShouldBe("sm");
            new Breakpoint(BreakpointType.Medium).Value.ShouldBe("md");
            new Breakpoint(BreakpointType.Large).Value.ShouldBe("lg");
            new Breakpoint(BreakpointType.ExtraLarge).Value.ShouldBe("xl");
        }

        [Fact(DisplayName = "Auto returns 'auto' as value")]
        public void AutoOptionReturnsCorrectCssClass()
        {
            new AutoOption().Value.ShouldBe("auto");
        }

        [Fact(DisplayName = "Breakpoint can be combined with auto option using - operator")]
        public void BreakpointWithAutoOption()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var auto = new AutoOption();
            var bpl = bp - auto;
            bpl.Value.ShouldBe($"{bp.Value}-{auto.Value}");
        }
    }
}
