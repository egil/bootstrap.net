using System.Diagnostics;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("Auto: {Value}")]
    public class BreakpointAuto : OptionPair<Breakpoint, AutoOption>, IAutoOption, ISpanOption, ISpacingOption
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