using System.Collections;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class OptionSet<T> : BaseOptionSet<T> where T : IOption<T>
    {
        public static OptionSet<T> operator |(OptionSet<T> set, int number)
        {
            set.Add(new GridNumber<T>(number));
            return set;
        }

        public static OptionSet<T> operator |(OptionSet<T> set, T option)
        {
            set.Add(option);
            return set;
        }
    }

    public class OptionSet2<TOption> : IOptionSet<TOption> where TOption : IOption
    {
        private readonly HashSet<TOption> options = new HashSet<TOption>();

        public int Count => options.Count;

        public IEnumerator<TOption> GetEnumerator() => options.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Add(TOption option)
        {
            options.Add(option);
        }
    }

}