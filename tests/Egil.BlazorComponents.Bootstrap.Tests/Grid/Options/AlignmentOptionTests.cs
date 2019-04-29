using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.AlignmentOptions
{
    public class AlignmentOptionTests : OptionFixture<IAlignmentOption>
    {
        [Fact(DisplayName = "Alignment option returns correct css class")]
        public void AlignOptionReturnsCorrectCssClass()
        {
            new AlignmentOption(AlignmentType.Start).Value.ShouldBe("start");
            new AlignmentOption(AlignmentType.End).Value.ShouldBe("end");
            new AlignmentOption(AlignmentType.Center).Value.ShouldBe("center");
            new AlignmentOption(AlignmentType.Stretch).Value.ShouldBe("stretch");
        }

        [Fact(DisplayName = "Breakpoint can be combined with alignment option using - operator")]
        public void BreakpointWithÁlignOption()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var align = new AlignmentOption(AlignmentType.Center);
            var bpa = bp - align;
            bpa.Value.ShouldBe($"{bp.Value}-{align.Value}");
        }

        [Theory(DisplayName = "Alignment options should be combineable with other alignment options")]
        [MemberData(nameof(SutOptionsPairsFixtureData))]
        public void AlignmentOptionsCombineable(IAlignmentOption first, IAlignmentOption second)
        {
            first.CombinedWith(second).ShouldResultInSetOf<IOptionSet<IAlignmentOption>>().ThatContains(first, second);
        }

        [Theory(DisplayName = "Alignment options should NOT be combineable with non-alignment options")]
        [MemberData(nameof(SutOptionsPairedWithIncompatibleOptionsFixtureData))]
        public void AlignmentOptionsNotCombineableWithOtherOptions(IOption first, IOption second)
        {
            first.CombinedWith(second).ShouldNotResultInSetOf<IOptionSet<IAlignmentOption>>();
        }

        [Theory(DisplayName = "Alignment options should be combineable with OptionSet of IAlignmentOption types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void AlignmentOptionShouldBeCombineableWithOptionSet(IAlignmentOption sutOption)
        {
            IOptionSet<IAlignmentOption> set = new OptionSet<IAlignmentOption>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<IOptionSet<IAlignmentOption>>()
                .ThatContains(sutOption);
        }
    }
}