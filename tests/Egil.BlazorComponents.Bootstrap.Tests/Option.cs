using System;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public abstract class Option
    {
        public abstract string CssClass { get; }

        protected static void ValidateGridNumber(int number)
        {
            if (number < 1 || number > 12)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Bootstrap grid has 12 columns. Numbers referring to it must be between 1 and 12.");
        }
    }

}