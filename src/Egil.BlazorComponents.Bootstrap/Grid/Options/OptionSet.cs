using System.Collections;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class OptionSet : IEnumerable<Option>
    {
        private readonly List<Option> options = new List<Option>();

        public OptionSet(params Option[] options)
        {
            this.options.AddRange(options);
        }

        public IEnumerator<Option> GetEnumerator()
        {
            return options.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static OptionSet operator |(OptionSet optionSet, Option additionalOption)
        {
            optionSet.options.Add(additionalOption);
            return optionSet;
        }
    }
}
