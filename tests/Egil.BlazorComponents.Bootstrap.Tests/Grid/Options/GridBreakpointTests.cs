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
    public class GridBreakpointTests
    {
        [Fact(DisplayName = "Breakpoint can have width specified via - operator")]
        public void BreakpointCanHaveWidthSpecifiedViaMinusOperator()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var span = 2;
            var bpw = bp - span;
            bpw.Value.ShouldBe($"{bp.Value}-{span}");
        }
    }
}
