using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;

namespace Egil.RazorComponents.Bootstrap.Options.SimpleOptions
{
    public sealed class BottomOption : ISideOption, IOption
    {
        public string Value { get; } = "bottom";

        string ISideOption.Value { get; } = "b";

        public static SpacingOption operator -(BottomOption side, int size)
        {
            return new SpacingOption(side, Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(BottomOption side, AutoOption auto)
        {
            return new SpacingOption(side, auto);
        }

        public static IntermediateSpacingOption operator -(BottomOption side, ISpanOption breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }
}
