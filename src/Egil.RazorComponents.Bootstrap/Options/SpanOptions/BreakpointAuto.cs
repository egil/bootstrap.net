using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options
{
    [DebuggerDisplay("Auto: {Value}")]
    public class BreakpointAuto : OptionPair<Breakpoint, AutoOption>, IAutoOption
    {
        public BreakpointAuto(Breakpoint leftOption, AutoOption rightOption) : base(leftOption, rightOption)
        {
        }

        public static OptionSet<IAutoOption> operator |(BreakpointAuto option1, int number)
        {
            return new OptionSet<IAutoOption>() { option1, (Number)number };
        }

        public static OptionSet<IAutoOption> operator |(int number, BreakpointAuto option1)
        {
            return new OptionSet<IAutoOption>() { option1, (Number)number };
        }

        public static OptionSet<IAutoOption> operator |(BreakpointAuto option1, IAutoOption option2)
        {
            return new OptionSet<IAutoOption>() { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(BreakpointAuto option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpacingOption> operator |(BreakpointAuto option1, ISpacingOption option2)
        {
            return new OptionSet<ISpacingOption>() { option1, option2 };
        }

        public static OptionSet<IAutoOption> operator |(OptionSet<IAutoOption> set, BreakpointAuto option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, BreakpointAuto option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<ISpacingOption> set, BreakpointAuto option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, BreakpointAuto option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}