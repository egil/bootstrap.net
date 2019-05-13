using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    [DebuggerDisplay("Number: {Value}")]
    public class Number : IBreakpointWithNumber, IAutoOption, ISpanOption, IOrderOption, IOffsetOption, ISpacingOption
    {
        private readonly int number;

        public string Value { get; }

        public bool IsValidSpacingNumber() => IsValidSpacingNumber(number);

        private Number(int number)
        {
            Value = number < 0 ? "n" + number * -1 : number.ToString();
            this.number = number;
        }

        public static Number ToGridNumber(int number)
        {
            if (number < 1 || number > 12)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Bootstrap grid has 12 columns. Numbers referring to it must be between 1 and 12.");

            return number;
        }

        public static Number ToSpacingNumber(int number)
        {
            if (!IsValidSpacingNumber(number))
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Bootstrap spacing numbers must be between -5 and 5.");

            return number;
        }

        public static bool IsValidSpacingNumber(int number)
        {
            return number >= -5 && number <= 5;
        }

        public static implicit operator Number(int number)
        {
            return new Number(number);
        }

        public static OptionSet<IBreakpointWithNumber> operator |(Number option1, IBreakpointWithNumber option2)
        {
            var set = new OptionSet<IBreakpointWithNumber>() { option1, option2 };
            return set;
        }

        public static OptionSet<IOffsetOption> operator |(Number option1, IOffsetOption option2)
        {
            var set = new OptionSet<IOffsetOption>() { option1, option2 };
            return set;
        }

        public static OptionSet<IOrderOption> operator |(Number option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }

        public static OptionSet<ISpanOption> operator |(Number option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpacingOption> operator |(Number option1, ISpacingOption option2)
        {
            return new OptionSet<ISpacingOption> { option1, option2 };
        }

        public static OptionSet<IBreakpointWithNumber> operator |(OptionSet<IBreakpointWithNumber> set, Number option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IAutoOption> operator |(OptionSet<IAutoOption> set, Number option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, Number option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<ISpacingOption> set, Number option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, Number option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOffsetOption> operator |(OptionSet<IOffsetOption> set, Number option)
        {
            set.Add(option);
            return set;
        }
    }
}
