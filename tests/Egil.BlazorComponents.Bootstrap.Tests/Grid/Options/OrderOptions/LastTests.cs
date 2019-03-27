using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.OrderOptions
{
    public class LastTests
    {
        [Fact]
        public void LastOptionReturnsCorrectCssClass()
        {
            new Last().Value.ShouldBe("last");
        }
    }
}
