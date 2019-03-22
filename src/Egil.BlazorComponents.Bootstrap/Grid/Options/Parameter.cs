using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class Parameter
    {
        protected const string OptionSeparator = "-";
        protected static void ValidateGridNumberInRange(int number)
        {
            if (number < 1 || number > 12)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Bootstrap grid has 12 columns. Numbers referring to it must be between 1 and 12.");
        }
    }
}
