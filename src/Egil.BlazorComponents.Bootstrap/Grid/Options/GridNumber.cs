using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class GridNumber : IGridBreakpoint, ISpanOption, IOrderOption, IOffsetOption
    {
        private readonly int number;

        public GridNumber(int number)
        {
            ValidateGridNumberInRange(number);
            this.number = number;
        }

        public string Value => number.ToString();

        public static implicit operator GridNumber(int number)
        {
            return new GridNumber(number);
        }

        /// <summary>
        /// Checks if the provided number is between 1 and 12. The valid
        /// range for specifying columns in Bootstrap. 
        /// Throws ArgumentOutOfRangeException if the number is outside the valid range.
        /// </summary>
        /// <param name="number">Number to check.</param>
        static void ValidateGridNumberInRange(int number)
        {
            if (number < 1 || number > 12)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Bootstrap grid has 12 columns. Numbers referring to it must be between 1 and 12.");
        }

        public static OptionSet<IOrderOption> operator |(GridNumber option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }
    }
}