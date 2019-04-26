using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("Option: {Value}")]
    public abstract class Option : IOption
    {
        public const string OptionSeparator = "-";

        public abstract string Value { get; }
    }

    public abstract class Option<T> : Option, IOption<T> where T : IOption<T>
    {
        public static OptionSet<T> operator |(int number, Option<T> option)
        {
            return new OptionSet<T> { new GridNumber<T>(number), option };
        }
        public static OptionSet<T> operator |(Option<T> option, int number)
        {
            return new OptionSet<T> { new GridNumber<T>(number), option };
        }
        public static OptionSet<T> operator |(Option<T> option1, T option2)
        {
            return new OptionSet<T> { option1, option2 };
        }
    }
}