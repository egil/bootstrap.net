using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;

namespace Egil.RazorComponents.Bootstrap.Utilities.Spacing
{
    public class IntermediateSpacingOption
    {
        private readonly ISideOption _side;
        private readonly ISpanOption _breakpoint;

        public IntermediateSpacingOption(ISideOption side, ISpanOption breakpoint)
        {
            this._side = side;
            this._breakpoint = breakpoint;
        }

        public static SpacingOption operator -(IntermediateSpacingOption intermediate, int size)
        {
            return new SpacingOption(intermediate._side, intermediate._breakpoint - Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(IntermediateSpacingOption intermediate, AutoOption auto)
        {
            return new SpacingOption(intermediate._side, intermediate._breakpoint - auto);
        }
    }
}

