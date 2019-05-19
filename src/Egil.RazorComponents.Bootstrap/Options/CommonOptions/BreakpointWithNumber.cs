using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    [DebuggerDisplay("BreakpointWithNumber: {Value}")]

    public class BreakpointWithNumber : OptionPair<Breakpoint, Number>, IBreakpointWithNumber
    {
        internal Number Number { get; }

        public BreakpointWithNumber(Breakpoint breakpoint, Number number) : base(breakpoint, number)
        {
            Number = number;
        }

        public static OptionSet<IBreakpointWithNumber> operator |(BreakpointWithNumber option1, int number)
        {
            return new OptionSet<IBreakpointWithNumber>() { option1, (Number)number };
        }

        public static OptionSet<IBreakpointWithNumber> operator |(int number, BreakpointWithNumber option1)
        {
            return new OptionSet<IBreakpointWithNumber>() { option1, (Number)number };
        }

        public static OptionSet<IBreakpointWithNumber> operator |(BreakpointWithNumber option1, BreakpointWithNumber option2)
        {
            return new OptionSet<IBreakpointWithNumber>() { option1, option2 };
        }

        public static OptionSet<IOrderOption> operator |(BreakpointWithNumber option1, IOrderOption option2)
        {
            return new OptionSet<IOrderOption>() { option1, option2 };
        }

        public static OptionSet<IAutoOption> operator |(BreakpointWithNumber option1, IAutoOption option2)
        {
            return new OptionSet<IAutoOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(BreakpointWithNumber option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpacingOption> operator |(BreakpointWithNumber option1, ISpacingOption option2)
        {
            return new OptionSet<ISpacingOption> { option1, option2 };
        }

        public static OptionSet<IBreakpointWithNumber> operator |(OptionSet<IBreakpointWithNumber> set, BreakpointWithNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, BreakpointWithNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, BreakpointWithNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOffsetOption> operator |(OptionSet<IOffsetOption> set, BreakpointWithNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<ISpacingOption> set, BreakpointWithNumber option)
        {
            set.Add(option);
            return set;
        }
        public static OptionSet<ISpacingOption> operator |(OptionSet<IAutoOption> set, BreakpointWithNumber option)
        {
            var spacingSet = new OptionSet<ISpacingOption>(set);
            spacingSet.Add(option);
            return spacingSet;
        }
    }
}