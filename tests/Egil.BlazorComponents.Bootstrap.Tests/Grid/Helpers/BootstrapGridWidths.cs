using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Egil.BlazorComponents.Bootstrap.Grid.Grid.Helpers
{
    public class BootstrapGridWidths : IEnumerable<object[]>
    {
        private static readonly List<object[]> Widths = Enumerable.Range(1, 12)
            .Select(x => new object[] { x })
            .ToList();

        public IEnumerator<object[]> GetEnumerator()
        {
            return Widths.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
