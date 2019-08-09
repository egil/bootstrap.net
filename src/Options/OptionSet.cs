using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Options
{
    [DebuggerDisplay("OptionSet with {Count} options of {OptionTypeName}")]
    public class OptionSet<TOption> : IOptionSet<TOption> where TOption : IOption
    {
        //private static string OptionTypeName { get; } = typeof(TOption).Name;

        private readonly HashSet<TOption> _options = new HashSet<TOption>();

        public int Count => _options.Count;

        public OptionSet() { }

        public OptionSet(IEnumerable<TOption> options)
        {
            foreach (var option in options)
            {
                Add(option);
            }
        }

        public void Add(TOption option)
        {
            _options.Add(option);
        }

        public IEnumerator<TOption> GetEnumerator() => _options.GetEnumerator();

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