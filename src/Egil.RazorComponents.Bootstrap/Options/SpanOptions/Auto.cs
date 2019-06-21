using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options
{
    [DebuggerDisplay("Auto: {Value}")]
    public class AutoOption : Option, IAutoOption
    {
        private const string OptionText = "auto";

        public override string Value { get; } = OptionText;

        public static BreakpointAuto operator -(ISpanOption breakpoint, AutoOption option)
        {
            return new BreakpointAuto(breakpoint, option);
        }

        public static OptionSet<IAutoOption> operator |(AutoOption option1, int number)
        {
            return new OptionSet<IAutoOption>() { option1, (Number)number };
        }

        public static OptionSet<IAutoOption> operator |(int number, AutoOption option1)
        {
            return new OptionSet<IAutoOption>() { option1, (Number)number };
        }

        public static OptionSet<IAutoOption> operator |(AutoOption option1, IAutoOption option2)
        {
            return new OptionSet<IAutoOption>() { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(AutoOption option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption>() { option1, option2 };
        }

        public static OptionSet<ISpacingOption> operator |(AutoOption option1, ISpacingOption option2)
        {
            return new OptionSet<ISpacingOption>() { option1, option2 };
        }

        public static OptionSet<IAutoOption> operator |(OptionSet<IAutoOption> set, AutoOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, AutoOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<ISpacingOption> set, AutoOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, AutoOption option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}