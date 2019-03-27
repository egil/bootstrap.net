namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class OptionSet<T> : BaseOptionSet<T> where T : IOption<T>
    {
        public static OptionSet<T> operator |(OptionSet<T> set, int number)
        {
            set.Add(new Number<T>(number));
            return set;
        }

        public static OptionSet<T> operator |(OptionSet<T> set, T option)
        {
            set.Add(option);
            return set;
        }
    }

}