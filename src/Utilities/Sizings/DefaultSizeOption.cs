namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    public class DefaultSizeOption : Option, ISpanOption
    {
        public override string Value { get; } = string.Empty;

        private DefaultSizeOption() { }

        public static readonly DefaultSizeOption Default = new DefaultSizeOption();

        public static OptionSet<ISpanOption> operator |(DefaultSizeOption option1, int width)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(int width, DefaultSizeOption option1)
        {
            return new OptionSet<ISpanOption>() { option1, Number.ToSpanNumber(width) };
        }

        public static OptionSet<ISpanOption> operator |(DefaultSizeOption option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, DefaultSizeOption option)
        {
            set.Add(option);
            return set;
        }
        public static OptionSet<ISpanOption> operator |(OptionSet<IBreakpointWithNumber> set, DefaultSizeOption option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set) { option };
            return spanSet;
        }
    }
}