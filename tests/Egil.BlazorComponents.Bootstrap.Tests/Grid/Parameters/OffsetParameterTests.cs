using System;
using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;
using Microsoft.CSharp.RuntimeBinder;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;


namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    public class OffsetParameterTests : ParameterFixture<IOffsetOption>
    {
        private static readonly string ParamPrefix = "offset";
        private OffsetParameter? sut;

        [Fact(DisplayName = "OffsetParameter.None.Value returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            OffsetParameter.None.ShouldBeEmpty();
            OffsetParameter.None.Count.ShouldBe(0);
        }

        [Theory(DisplayName = "Offset can have valid index number specified by assignment")]
        [NumberRangeData(1, 12)]
        public void OrderCanHaveValidIndexNumberSpecifiedByAssignment(int number)
        {
            sut = number;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, number);
        }

        [Fact(DisplayName = "Specifying an invalid index number throws")]
        public void SpecifyinInvalidIndexNumberThrows()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = 0);
            Should.Throw<ArgumentOutOfRangeException>(() => sut = 13);
        }

        [Fact(DisplayName = "Offset can have a breakpoint with index specified by assignment")]
        public void CanHaveBreakpointWithIndexSpecifiedByAssignment()
        {
            var option = lg - 4;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Offset can have option sets of offset-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Offset can NOT have option sets of none-offset-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}
