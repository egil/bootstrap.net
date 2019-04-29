using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class SpanOptionTests : OptionFixture<ISpanOption>
    {
        [Fact(DisplayName = "Breakpoint returns correct value based on breakpoint type")]
        public void CssClassReturnsCorrectBootstrapBreakpoint()
        {
            new Breakpoint(BreakpointType.Small).Value.ShouldBe("sm");
            new Breakpoint(BreakpointType.Medium).Value.ShouldBe("md");
            new Breakpoint(BreakpointType.Large).Value.ShouldBe("lg");
            new Breakpoint(BreakpointType.ExtraLarge).Value.ShouldBe("xl");
        }

        [Fact(DisplayName = "Auto returns 'auto' as value")]
        public void AutoOptionReturnsCorrectCssClass()
        {
            new AutoOption().Value.ShouldBe("auto");
        }

        [Fact(DisplayName = "Breakpoint can be combined with auto option using - operator")]
        public void BreakpointWithAutoOption()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var auto = new AutoOption();
            var bpl = bp - auto;
            bpl.Value.ShouldBe($"{bp.Value}-{auto.Value}");
        }

        [Theory(DisplayName = "Span options should be combineable with other span options")]
        [MemberData(nameof(SutOptionsPairsFixtureData))]
        public void SpanOptionsCombineable(ISpanOption first, ISpanOption second)
        {
            first.CombinedWith(second).ShouldResultInSetOf<IOptionSet<ISpanOption>>().ThatContains(first, second);
        }

        [Theory(DisplayName = "Span options should NOT be combineable with non-span options")]
        [MemberData(nameof(SutOptionsPairedWithIncompatibleOptionsFixtureData))]
        public void SpanOptionsNotCombineableWithOtherOptions(IOption first, IOption second)
        {
            first.CombinedWith(second).ShouldNotResultInSetOf<IOptionSet<ISpanOption>>();
        }

        [Theory(DisplayName = "Span options should be combineable with number")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void SpanOptionShouldBeCombineableWithNumber(ISpanOption sutOption)
        {
            int num = 5;

            num.CombinedWith(sutOption)
                .ShouldResultInSetOf<IOptionSet<ISpanOption>>()
                .ThatContains(sutOption, (GridNumber)5);

            sutOption.CombinedWith(num)
                .ShouldResultInSetOf<IOptionSet<ISpanOption>>()
                .ThatContains((GridNumber)5, sutOption);
        }

        [Theory(DisplayName = "Span options should be combineable with OptionSet of ISpanOption types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void SpanOptionShouldBeCombineableWithOptionSet(ISpanOption sutOption)
        {
            IOptionSet<ISpanOption> set = new OptionSet<ISpanOption>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<IOptionSet<ISpanOption>>()
                .ThatContains(sutOption);
        }

        [Theory(DisplayName = "Span options should be combineable with OptionSet of IGridBreakpoint types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void SpanOptionShouldBeCombineableWithIGridBreakpointOptionSet(ISpanOption sutOption)
        {
            IOptionSet<IGridBreakpoint> set = new OptionSet<IGridBreakpoint>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<IOptionSet<ISpanOption>>()
                .ThatContains(sutOption);
        }
    }
}
