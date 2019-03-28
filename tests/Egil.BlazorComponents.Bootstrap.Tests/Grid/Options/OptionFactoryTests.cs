using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class OptionFactoryTests
    {
        [Fact(DisplayName = "Static factory options are of correct type")]
        public void FactoryOptionsCorrectType()
        {
            sm.ShouldBeOfType<Breakpoint>()
                .Value.ShouldBe(new Breakpoint(BreakpointType.Small).Value);
            md.ShouldBeOfType<Breakpoint>()
                .Value.ShouldBe(new Breakpoint(BreakpointType.Medium).Value);
            lg.ShouldBeOfType<Breakpoint>()
                .Value.ShouldBe(new Breakpoint(BreakpointType.Large).Value);
            xl.ShouldBeOfType<Breakpoint>()
                .Value.ShouldBe(new Breakpoint(BreakpointType.ExtraLarge).Value);
            first.ShouldBeOfType<First>();
            last.ShouldBeOfType<Last>();
            auto.ShouldBeOfType<Auto>();
        }
    }
}
