using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    [DebuggerDisplay("Breakpoint: {Value}")]
    public class Breakpoint : ISpanOption
    {
        private readonly BreakpointType type;

        public Breakpoint(BreakpointType type)
        {
            this.type = type;
        }

        public string Value => type switch
        {
            BreakpointType.Small => "sm",
            BreakpointType.Medium => "md",
            BreakpointType.Large => "lg",
            BreakpointType.ExtraLarge => "xl",
            _ => throw new InvalidOperationException($"Unknown {nameof(BreakpointType)} specified in {nameof(Breakpoint)}.")
        };

        public static BreakpointWithNumber operator -(Breakpoint breakpoint, int width)
        {
            return new BreakpointWithNumber(breakpoint, width);
        }

        public static OptionSet<ISpanOption> operator |(Breakpoint option1, int width)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(int width, Breakpoint option1)
        {
            return new OptionSet<ISpanOption>() { option1, (Number)width };
        }

        public static OptionSet<ISpanOption> operator |(Breakpoint option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, Breakpoint option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, Breakpoint option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }

    public class DefaultBreakpoint : ISpanOption
    {
        public string Value => string.Empty;

        public static OptionSet<ISpanOption> operator |(DefaultBreakpoint option1, int width)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(int width, DefaultBreakpoint option1)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(DefaultBreakpoint option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, DefaultBreakpoint option)
        {
            set.Add(option);
            return set;
        }
        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, DefaultBreakpoint option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}