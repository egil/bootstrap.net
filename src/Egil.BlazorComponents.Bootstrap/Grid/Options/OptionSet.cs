using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class OptionSet<TOption> : IOptionSet<TOption> where TOption : IOption
    {
        private readonly HashSet<TOption> options = new HashSet<TOption>();

        public int Count => options.Count;

        public OptionSet()
        {
        }

        public OptionSet(IEnumerable<TOption> options)
        {
            foreach(var option in options)
            {
                Add(option);
            }            
        }

        public void Add(TOption option)
        {
            options.Add(option);
        }

        public IEnumerator<TOption> GetEnumerator() => options.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public static readonly IOptionSet<TOption> Empty = new EmptyOptionSet();

        private class EmptyOptionSet : IOptionSet<TOption>
        {
            public int Count => 0;

            public IEnumerator<TOption> GetEnumerator()
            {
                return Enumerable.Empty<TOption>().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}