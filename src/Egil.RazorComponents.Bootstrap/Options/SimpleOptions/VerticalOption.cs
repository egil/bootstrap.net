using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;

namespace Egil.RazorComponents.Bootstrap.Options.SimpleOptions
{
    public sealed class VerticalOption : ISideOption, IOption
    {
        public string Value { get; } = "vertical";

        string ISideOption.Value { get; } = "y";

        public static SpacingOption operator -(VerticalOption side, int size)
        {
            return new SpacingOption(side, Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(VerticalOption side, AutoOption auto)
        {
            return new SpacingOption(side, auto);
        }

        public static IntermediateSpacingOption operator -(VerticalOption side, ISpanOption breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }
}
