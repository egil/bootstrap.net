using System;
using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("GridNumber: {Value}")]
    public class GridNumber : IGridBreakpoint, ISpanOption, IOrderOption, IOffsetOption
    {
        private readonly int number;

        private GridNumber(int number)
        {
            ValidateGridNumberInRange(number);
            this.number = number;
        }

        public string Value => number.ToString();

        private static readonly GridNumber[] GridNumberPool = new[]
        {
            new GridNumber(1),
            new GridNumber(2),
            new GridNumber(3),
            new GridNumber(4),
            new GridNumber(5),
            new GridNumber(6),
            new GridNumber(7),
            new GridNumber(8),
            new GridNumber(9),
            new GridNumber(10),
            new GridNumber(11),
            new GridNumber(12),
        };

        public static implicit operator GridNumber(int number)
        {
            ValidateGridNumberInRange(number);
            return GridNumberPool[number - 1];
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

        public static OptionSet<IGridBreakpoint> operator |(GridNumber gridNumber1, int gridNumber2)
        {
            return new OptionSet<IGridBreakpoint>() { gridNumber1, (GridNumber)gridNumber2 };
        }

        public static OptionSet<IGridBreakpoint> operator |(int gridNumber1, GridNumber gridNumber2)
        {
            return new OptionSet<IGridBreakpoint>() { (GridNumber)gridNumber1, gridNumber2 };
        }

        public static OptionSet<IGridBreakpoint> operator |(GridNumber option1, IGridBreakpoint option2)
        {
            var set = new OptionSet<IGridBreakpoint>() { option1, option2 };
            return set;
        }

        public static OptionSet<IOrderOption> operator |(GridNumber option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }

        public static OptionSet<ISpanOption> operator |(GridNumber option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<IGridBreakpoint> operator |(OptionSet<IGridBreakpoint> set, GridNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, GridNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, GridNumber option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOffsetOption> operator |(OptionSet<IOffsetOption> set, GridNumber option)
        {
            set.Add(option);
            return set;
        }
    }
}
