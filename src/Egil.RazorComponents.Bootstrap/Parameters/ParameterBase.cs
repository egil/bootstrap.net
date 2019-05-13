using System.Collections;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Parameters
{

    public abstract class ParameterBase : IParameter
    {
        public abstract int Count { get; }

        public abstract IEnumerator<string> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
