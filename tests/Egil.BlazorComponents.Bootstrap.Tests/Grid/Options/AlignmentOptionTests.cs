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
    }
}