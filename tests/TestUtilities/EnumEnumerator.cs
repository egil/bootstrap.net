using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Tests.TestUtilities
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
            return GetEnumerator();
        }
    }
}
