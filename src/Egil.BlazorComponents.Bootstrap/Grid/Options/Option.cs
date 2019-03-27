namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class Option<T> : IOption<T> where T : IOption<T>
    {
        public abstract string Value { get; }

        public static OptionSet<T> operator |(int number, Option<T> option)
        {
            return new OptionSet<T> { new Number<T>(number), option };
        }
        public static OptionSet<T> operator |(Option<T> option, int number)
        {
            return new OptionSet<T> { new Number<T>(number), option };
        }
        public static OptionSet<T> operator |(Option<T> option1, T option2)
        {
            return new OptionSet<T> { option1, option2 };
        }
    }
}