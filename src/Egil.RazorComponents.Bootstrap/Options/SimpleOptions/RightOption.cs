using Egil.RazorComponents.Bootstrap.Components.Dropdowns.Parameters;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;

namespace Egil.RazorComponents.Bootstrap.Options.SimpleOptions
{
    public sealed class RightOption : ISideOption, IDirectionOption, IOption
    {
        public string Value { get; } = "right";

        string ISideOption.Value { get; } = "r";

        public static SpacingOption operator -(RightOption side, int size)
        {
            return new SpacingOption(side, Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(RightOption side, AutoOption auto)
        {
            return new SpacingOption(side, auto);
        }

        public static IntermediateSpacingOption operator -(RightOption side, ISpanOption breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }
}
