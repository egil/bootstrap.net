using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;

namespace Egil.RazorComponents.Bootstrap.Options.SimpleOptions
{
    public sealed class TopOption : ISideOption, IOption
    {
        public string Value { get; } = "top";

        string ISideOption.Value { get; } = "t";

        public static SpacingOption operator -(TopOption side, int size)
        {
            return new SpacingOption(side, Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(TopOption side, AutoOption auto)
        {
            return new SpacingOption(side, auto);
        }

        public static IntermediateSpacingOption operator -(TopOption side, ISpanOption breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }
}
