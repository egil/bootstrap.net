using System;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class Option
    {
        protected const string OptionSeparator = "-";

        public abstract string CssClass { get; }

        /// <summary>
        /// Checks if the provided number is between 1 and 12. The valid
        /// range for specifying columns in Bootstrap. 
        /// Throws ArgumentOutOfRangeException if the number is outside the valid range.
        /// </summary>
        /// <param name="number">Number to check.</param>
        protected static void ValidateGridNumberInRange(int number)
        {
            if (number < 1 || number > 12)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Bootstrap grid has 12 columns. Numbers referring to it must be between 1 and 12.");
        }

        public static implicit operator Option(int number)
        {
            return new NumberOption(number);
        }

        public static OptionSet operator |(Option option1, Option option2)
        {
            return new OptionSet(option1, option2);
        }
    }
}
