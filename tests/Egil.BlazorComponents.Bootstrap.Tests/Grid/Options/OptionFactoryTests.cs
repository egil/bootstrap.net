using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.UpperCase;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.UpperCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class OptionFactoryTests
    {
        [Fact(DisplayName = "Static factory options are of correct type")]
        public void FactoryOptionsCorrectType()
        {
            Small.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Small).Value);
            Medium.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Medium).Value);
            Large.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Large).Value);
            ExtraLarge.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.ExtraLarge).Value);
            First.ShouldBeOfType<FirstOption>();
            Last.ShouldBeOfType<LastOption>();
            Auto.ShouldBeOfType<AutoOption>();
        }

        [Fact(DisplayName = "Static factory LowerCase options are of correct type")]
        public void FactoryOptionsLowerCaseCorrectType()
        {
            small.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Small).Value);
            medium.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Medium).Value);
            large.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Large).Value);
            extraLarge.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.ExtraLarge).Value);
            LowerCase.first.ShouldBeOfType<FirstOption>();
            LowerCase.last.ShouldBeOfType<LastOption>();
            LowerCase.auto.ShouldBeOfType<AutoOption>();
        }

        [Fact(DisplayName = "Static factory LowerCase Abbr options are of correct type")]
        public void FactoryOptionsLowerCaseAbbrCorrectType()
        {
            sm.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Small).Value);
            md.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Medium).Value);
            lg.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Large).Value);
            xl.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.ExtraLarge).Value);
            LowerCase.Abbr.first.ShouldBeOfType<FirstOption>();
            LowerCase.Abbr.last.ShouldBeOfType<LastOption>();
            LowerCase.Abbr.auto.ShouldBeOfType<AutoOption>();
        }

        [Fact(DisplayName = "Static factory UpperCase options are of correct type")]
        public void FactoryOptionsUpperCaseCorrectType()
        {
            SMALL.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Small).Value);
            MEDIUM.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Medium).Value);
            LARGE.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Large).Value);
            EXTRALARGE.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.ExtraLarge).Value);
            UpperCase.FIRST.ShouldBeOfType<FirstOption>();
            UpperCase.LAST.ShouldBeOfType<LastOption>();
            UpperCase.AUTO.ShouldBeOfType<AutoOption>();
        }

        [Fact(DisplayName = "Static factory UpperCase  Abbr options are of correct type")]
        public void FactoryOptionsUpperCaseAbbrCorrectType()
        {
            SM.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Small).Value);
            MD.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Medium).Value);
            LG.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.Large).Value);
            XL.ShouldBeOfType<Breakpoint>().Value.ShouldBe(new Breakpoint(BreakpointType.ExtraLarge).Value);
            UpperCase.Abbr.FIRST.ShouldBeOfType<FirstOption>();
            UpperCase.Abbr.LAST.ShouldBeOfType<LastOption>();
            UpperCase.Abbr.AUTO.ShouldBeOfType<AutoOption>();
        }
    }
}