using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.SpanOptions
{
    public class AutoTests
    {
        [Fact(DisplayName = "Auto returns 'auto' as value")]
        public void AutoOptionReturnsCorrectCssClass()
        {
            new Auto().Value.ShouldBe("auto");
        }
    }
}
