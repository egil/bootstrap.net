using Egil.BlazorComponents.Bootstrap.Grid;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Helpers
{
    class BootstrapBreakpoints : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return Enum.GetValues(typeof(BreakpointType))
                .OfType<BreakpointType>()
                .Select(x => new object[] { x })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
