using Egil.RazorComponents.Bootstrap.Components.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;
using System;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
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
        [NumberRangeData(1, 11)]
        public void OrderCanHaveValidIndexNumberSpecifiedByAssignment(int number)
        {
            sut = number;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, number);
        }

        [Theory(DisplayName = "Specifying an invalid index number throws")]
        [InlineData(0)]
        [InlineData(12)]
        public void SpecifyinInvalidIndexNumberThrows(int index)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = index);
        }

        [Theory(DisplayName = "Offset can have a breakpoint with index specified by assignment")]
        [NumberRangeData(0, 11)]
        public void CanHaveBreakpointWithIndexSpecifiedByAssignment(int number)
        {
            var option = lg - number;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Specifying an invalid index number with breakpoint throws")]
        [InlineData(-1)]
        [InlineData(12)]
        public void SpecifyinInvalidIndexNumberWithBreakpointThrows(int number)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = lg - number);
        }

        [Theory(DisplayName = "Offset can have option sets of offset-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Index of number and brekapoint with number in sets are validated on assignment")]
        [InlineData(0, -1)]
        [InlineData(12, 12)]
        public void InvalidIndexInSetThrows(int numberOnly, int breakpointNumber)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = numberOnly | lg - 1);
            Should.Throw<ArgumentOutOfRangeException>(() => sut = lg - breakpointNumber | 0);
        }

        [Theory(DisplayName = "Offset can NOT have option sets of none-offset-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}
