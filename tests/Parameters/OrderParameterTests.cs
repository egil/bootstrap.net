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
    public class OrderParameterTests : ParameterFixture<IOrderOption>
    {
        private static readonly string ParamPrefix = "order";
        private OrderParameter? sut;

        [Fact(DisplayName = "OrderParameter.None.Value returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            OrderParameter.None.ShouldBeEmpty();
            OrderParameter.None.Count.ShouldBe(0);
        }

        [Theory(DisplayName = "Order can have valid index number specified by assignment")]
        [NumberRangeData(0, 12)]
        public void OrderCanHaveValidIndexNumberSpecifiedByAssignment(int number)
        {
            sut = number;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, number);
        }

        [Theory(DisplayName = "Specifying an invalid index number throws")]
        [InlineData(-1)]
        [InlineData(13)]
        public void SpecifyinInvalidIndexNumberThrows(int number)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = number);
        }

        [Theory(DisplayName = "Order can have a breakpoint with index specified by assignment")]
        [NumberRangeData(0, 12)]
        public void OrderCanHaveBreakpointWithIndexSpecifiedByAssignment(int number)
        {
            var option = lg - number;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Specifying an invalid index number with breakpoint throws")]
        [InlineData(-1)]
        [InlineData(13)]
        public void SpecifyinInvalidIndexNumberWithBreakpointThrows(int number)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = md - number);
        }

        [Fact(DisplayName = "Order can have a first option specified by assignment")]
        public void OrderCanHaveFirstOptionSpecifiedByAssignment()
        {
            var option = first;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Order can have a last option specified by assignment")]
        public void OrderCanHaveLastOptionSpecifiedByAssignment()
        {
            var option = last;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Order can have a breakpoint with first option specified by assignment")]
        public void OrderCanHaveBreakpointWithFirstOptionSpecifiedByAssignment()
        {
            var option = sm - first;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Order can have a breakpoint with last option specified by assignment")]
        public void OrderCanHaveBreakpointWithLastOptionSpecifiedByAssignment()
        {
            var option = lg - last;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Order can have option sets of order-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Index of number and brekapoint with number in sets are validated on assignment")]
        [InlineData(-1)]
        [InlineData(13)]
        public void InvalidIndexInSetThrows(int number)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = number | lg - 3);
            Should.Throw<ArgumentOutOfRangeException>(() => sut = lg - number | 3);
        }

        [Theory(DisplayName = "Order can NOT have option sets of none-order-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}