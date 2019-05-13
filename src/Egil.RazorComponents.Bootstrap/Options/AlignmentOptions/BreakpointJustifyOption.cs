using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.AlignmentOptions
{
    [DebuggerDisplay("JustifyOption: {Value}")]
    public sealed class BreakpointJustifyOption : OptionPair<Breakpoint, JustifyOption>, IJustifyOption
    {
        public BreakpointJustifyOption(Breakpoint leftOption, JustifyOption rightOption) : base(leftOption, rightOption)
        {
        }

        public static IOptionSet<IJustifyOption> operator |(BreakpointJustifyOption option1, IJustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static IOptionSet<IJustifyOption> operator |(AlignmentOption option1, BreakpointJustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static IOptionSet<IJustifyOption> operator |(BreakpointAlignmentOption option1, BreakpointJustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static IOptionSet<IJustifyOption> operator |(OptionSet<IJustifyOption> set, BreakpointJustifyOption option2)
        {
            set.Add(option2);
            return set;
        }
    }
}
