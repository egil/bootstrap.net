using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    [DebuggerDisplay("SizeOption: {Value}")]
    public class SizeOption : Option, ITextualSize, ISpanOption
    {
        private const string SmallValue = "sm";
        private const string MediumValue = "md";
        private const string LargeValue = "lg";

        public override string Value { get; }

        protected SizeOption(string size)
        {
            Value = size;
        }

        public static SizeOption Small = new SizeOption(SmallValue);
        public static SizeOption Medium = new SizeOption(MediumValue);
        public static SizeOption Large = new SizeOption(LargeValue);

        public static BreakpointWithNumber operator -(SizeOption breakpoint, int width)
        {
            return new BreakpointWithNumber(breakpoint, width);
        }

        public static OptionSet<ISpanOption> operator |(SizeOption option1, int width)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(int width, SizeOption option1)
        {
            return new OptionSet<ISpanOption>() { option1, (Number)width };
        }

        public static OptionSet<ISpanOption> operator |(SizeOption option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, SizeOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, SizeOption option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set) { option };
            return spanSet;
        }
    }
}