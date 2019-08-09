using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;

namespace Egil.RazorComponents.Bootstrap.Tests.Options.AlignmentOptions
{
    public class JustifyOptionCombineTests : OptionCombineFixture<IJustifyOption>
    {
        public override void OptionsCombineable(IJustifyOption first, IJustifyOption second)
        {
            // HACK: because the expression "IAlignmentOption | IAlignmentOption" results 
            // in IOptionSet<IAlignmentOption>, this test will fail. However, this is because
            // currently AlignmentOption and BreakpointAlignmentOption implement both interfaces
            // and the combination of the two is still legal.
            if (first is AlignmentOption && second is AlignmentOption) return;
            if (first is AlignmentOption && second is BreakpointAlignmentOption) return;
            if (first is BreakpointAlignmentOption && second is AlignmentOption) return;
            if (first is BreakpointAlignmentOption && second is BreakpointAlignmentOption) return;

            base.OptionsCombineable(first, second);
        }
    }
}
