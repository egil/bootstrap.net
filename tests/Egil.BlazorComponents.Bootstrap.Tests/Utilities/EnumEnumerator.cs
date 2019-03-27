using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Egil.BlazorComponents.Bootstrap.Grid
{    
    public class EnumEnumerator<T> : IEnumerable<object[]> where T : struct
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return Enum.GetValues(typeof(T))
                .OfType<T>()
                .Select(x => new object[] { x })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
