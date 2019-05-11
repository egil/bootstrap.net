using System;
using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.CommonOptions
{
    public class BreakpointWithNumberTests
    {
        [Fact(DisplayName = "Breakpoint can have width specified via - operator")]
        public void BreakpointCanHaveWidthSpecifiedViaMinusOperator()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var span = 2;
            var bpw = bp - span;
            bpw.Value.ShouldBeCombinationOf(bp, (Number)span);
        }
    }
}
