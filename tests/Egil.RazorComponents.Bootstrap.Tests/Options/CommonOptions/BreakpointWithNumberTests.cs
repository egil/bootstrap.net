using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.CommonOptions
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
