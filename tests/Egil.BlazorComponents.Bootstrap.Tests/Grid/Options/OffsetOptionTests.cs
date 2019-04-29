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
    // Are these tests valuable?
    public class OffsetOptionTests : OptionFixture<IOffsetOption>
    {
        [Theory(DisplayName = "Offset options should be combineable with other offset options")]
        [MemberData(nameof(SutOptionsPairsFixtureData))]
        public void OffsetOptionsCombineable(IOffsetOption first, IOffsetOption second)
        {
            first.CombinedWith(second).ShouldResultInSetOf<IOptionSet<IOffsetOption>>().ThatContains(first, second);
        }

        [Theory(DisplayName = "Offset options should NOT be combineable with non-offset options")]
        [MemberData(nameof(SutOptionsPairedWithIncompatibleOptionsFixtureData))]
        public void OffsetOptionsNotCombineableWithOtherOptions(IOption first, IOption second)
        {
            first.CombinedWith(second).ShouldNotResultInSetOf<IOptionSet<IOffsetOption>>();
        }

        [Theory(DisplayName = "Offset options should be combineable with OptionSet of IOffsetOption types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void OffsetOptionShouldBeCombineableWithOptionSet(IOffsetOption sutOption)
        {
            IOptionSet<IOffsetOption> set = new OptionSet<IOffsetOption>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<IOptionSet<IOffsetOption>>()
                .ThatContains(sutOption);
        }

        [Theory(DisplayName = "Offset options should be combineable with OptionSet of IGridBreakpoint types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void OffsetOptionShouldBeCombineableWithIGridBreakpointOptionSet(IOffsetOption sutOption)
        {
            IOptionSet<IGridBreakpoint> set = new OptionSet<IGridBreakpoint>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<IOptionSet<IOffsetOption>>()
                .ThatContains(sutOption);
        }        
    }
}
