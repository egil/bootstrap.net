using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.CommonOptions
{
    [DebuggerDisplay("Number: {Value}")]
    public class Number : IBreakpointWithNumber
    {
        private readonly int number;

        public string Value { get; }

        public void ValidateAsSpanNumber()
        {
            if (!IsValidSpanNumber(number))
                throw new ArgumentOutOfRangeException("Bootstrap grid has 12 columns. Numbers referring to it must be between 1 and 12.");
        }

        public void ValidateAsSpacingNumber()
        {
            if (!IsValidSpacingNumber(number))
                throw new ArgumentOutOfRangeException("Bootstrap spacing numbers must be between -5 and 5.");
        }

        public void ValidateAsOrderNumber()
        {
            if (!IsValidOrderNumber(number))
                throw new ArgumentOutOfRangeException("Order index can be between 0 and 12.");
        }

        public void ValidateAsOffsetNumber()
        {
            if (!IsValidOffsetNumber(number))
                throw new ArgumentOutOfRangeException("When offset is specified without a breakpoint, the number can be between 1 and 11.");
        }

        public void ValidateAsOffsetBreakpointNumber()
        {
            if (!IsValidOffsetBreakpointNumber(number))
                throw new ArgumentOutOfRangeException("When offset is specified with a breakpoint, the number can be between 0 and 11.");
        }

        private Number(int number)
        {
            Value = number < 0 ? "n" + number * -1 : number.ToString();
            this.number = number;
        }

        public static Number ToSpanNumber(int number)
        {
            var res = (Number)number;
            res.ValidateAsSpanNumber();
            return res;
        }

        public static Number ToOrderNumber(int number)
        {
            var res = (Number)number;
            res.ValidateAsOrderNumber();
            return res;
        }

        public static Number ToOffsetNumber(int number)
        {
            var res = (Number)number;
            res.ValidateAsOffsetNumber();
            return res;
        }

        public static Number ToSpacingNumber(int number)
        {
            var res = (Number)number;
            res.ValidateAsSpacingNumber();
            return res;
        }

        public static bool IsValidSpanNumber(int number)
        {
            return number >= 1 && number <= 12;
        }


        public static bool IsValidSpacingNumber(int number)
        {
            return number >= -5 && number <= 5;
        }

        public static bool IsValidOrderNumber(int number)
        {
            return number >= 0 && number <= 12;
        }

        public static bool IsValidOffsetNumber(int number)
        {
            return number >= 1 && number <= 11;
        }

        public static bool IsValidOffsetBreakpointNumber(int number)
        {
            return number >= 0 && number <= 11;
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
