using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class OrderSetTests
    {
        [Fact]
        public void OrderSetDoesNotAllowDuplicatedOptions()
        {
            true.ShouldBeFalse();
        }
    }
}
