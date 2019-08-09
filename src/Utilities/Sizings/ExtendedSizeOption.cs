using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    [DebuggerDisplay("ExtendedTextualSize: {Value}")]
    public class ExtendedSizeOption : Option, IExtendedTextualSize, ISpanOption
    {
        private const string ExtraSmallValue = "xs";
        private const string MediumValue = "md";
        private const string ExtraLargeValue = "xl";

        public override string Value { get; }

        private ExtendedSizeOption(string size)
        {
            Value = size;
        }

        public static ExtendedSizeOption ExtraSmall = new ExtendedSizeOption(ExtraSmallValue);
        public static ExtendedSizeOption Medium = new ExtendedSizeOption(MediumValue);
        public static ExtendedSizeOption ExtraLarge = new ExtendedSizeOption(ExtraLargeValue);

        public static BreakpointWithNumber operator -(ExtendedSizeOption breakpoint, int width)
        {
            return new BreakpointWithNumber(breakpoint, width);
        }

        public static OptionSet<ISpanOption> operator |(ExtendedSizeOption option1, int width)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(int width, ExtendedSizeOption option1)
        {
            return new OptionSet<ISpanOption>() { option1, (Number)width };
        }

        public static OptionSet<ISpanOption> operator |(ExtendedSizeOption option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, ExtendedSizeOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, ExtendedSizeOption option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set) { option };
            return spanSet;
        }
    }
}