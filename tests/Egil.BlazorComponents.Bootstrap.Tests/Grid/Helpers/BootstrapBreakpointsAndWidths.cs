using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Helpers
{

    public class BootstrapGridSpanOptionsAndWidths : IEnumerable<object[]>
    {
        private static readonly BootstrapGridSpanOptions options = new BootstrapGridSpanOptions();

        private static readonly BootstrapGridWidths widths = new BootstrapGridWidths();

        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var option in options)
            {
                foreach (var width in widths)
                {
                    yield return new object[] { option[0], width[0] };
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
