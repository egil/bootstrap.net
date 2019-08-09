using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
{
    public abstract class SpacingParameterTests<TParamPrefix> : ParameterFixture<ISpacingOption>
        where TParamPrefix : ICssClassPrefix, new()
    {
        protected abstract string ParamPrefix { get; }

        private SpacingParameter<TParamPrefix>? Sut { get; set; }

        [Fact(DisplayName = "Parameter prefix returns correct value")]
        public void ParameterPrefixReturnsCorrectValue()
        {
            new TParamPrefix().Prefix.ShouldBe(ParamPrefix);
        }

        [Fact(DisplayName = "SpacingParameter.None.Value returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            OrderParameter.None.ShouldBeEmpty();
            OrderParameter.None.Count.ShouldBe(0);
        }

        [Theory(DisplayName = "Spacing can have valid size number specified by assignment ({property}-{size})")]
        [NumberRangeData(-5, 5)]
        public void CanHaveValidIndexNumberSpecifiedByAssignment(int size)
        {
            Sut = size;
            Sut.ShouldContainOptionsWithPrefix(ParamPrefix, size);
        }

        [Theory(DisplayName = "Spacing can have sides and size specified by assignment ({property}[sides]-{size})")]
        [NumberRangeData(-5, 5)]
        public void CanHaveSideAndSizes(int size)
        {
            var side = left;
            Sut = side - size;
            Sut.Single().ShouldBe($"{ParamPrefix}{"l"}-{((Number)size).Value}");
        }

        [Theory(DisplayName = "Spacing can have breakpoint and size specified by assignment ({property}-{breakpoint}-{size})")]
        [NumberRangeData(-5, 5)]
        public void CanHaveBreakpointWithSize(int size)
        {
            var option = md - size;
            Sut = option;
            Sut.Single().ShouldBe($"{ParamPrefix}-{option.Value}");
        }

        [Theory(DisplayName = "Spacing can have side, breakpoint and size specified by assignment ({property}[sides]-{breakpoint}-{size})")]
        [NumberRangeData(-5, 5)]
        public void CanHaveSideAndBreakpointWithSize(int size)
        {
            var option = left - md - size;
            Sut = option;
            Sut.Single().ShouldBe($"{ParamPrefix}{option.Value}");
        }

        [Fact(DisplayName = "Spacing can have auto option specified by assignment ({property}-{auto})")]
        public void AssignAuto()
        {
            Sut = auto;
            Sut.Single().ShouldBe($"{ParamPrefix}-{auto.Value}");
        }

        [Fact(DisplayName = "Spacing can have breakpoint auto option specified by assignment ({property}-{breakpoint}-{auto})")]
        public void AssignBreakpointWithAuto()
        {
            var option = md - auto;
            Sut = option;
            Sut.Single().ShouldBe($"{ParamPrefix}-{option.Value}");
        }

        [Fact(DisplayName = "Spacing can have side, breakpoint auto option specified by assignment ({property}[side]-{breakpoint}-{auto})")]
        public void AssignSideBreakpointWithAuto()
        {
            var option = left - md - auto;
            Sut = option;
            Sut.Single().ShouldBe($"{ParamPrefix}{option.Value}");
        }

        [Theory(DisplayName = "Specifying an invalid size number throws")]
        [InlineData(-6)]
        [InlineData(6)]
        public void SpecifyinInvalidIndexNumberThrows(int invalidSize)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Sut = invalidSize);
        }

        [Theory(DisplayName = "Specifying a breakpoint invalid size number throws")]
        [InlineData(-6)]
        [InlineData(6)]
        public void CanHaveBreakpointWithSizeThrowsWithInvalidSize(int invalidSize)
        {
            var invalidOption = md - invalidSize;
            Should.Throw<ArgumentOutOfRangeException>(() => Sut = invalidOption);
        }

        [Theory(DisplayName = "Spacing can have option sets of spacing-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfSpacingOptionsSpecified(dynamic set)
        {
            Sut = set;
        }

        [Fact(DisplayName = "Spacing correctly outputs option values specified in sets of spacing-options")]
        public void CorrectlyOutputValuesFromSet()
        {
            var num = 1;
            var bpAuto = md - auto;
            var bpn = md - num;
            var sideBpAuto = right - lg - auto;
            var spacing = left - num;
            var spacingBpNumber = left - md - 4;
            var set = num | auto | bpAuto | bpn | sideBpAuto | spacing | spacingBpNumber;

            Sut = set;

            Sut.OrderBy(x => x).ShouldBe(new[]{
                $"{ParamPrefix}-{num}",
                $"{ParamPrefix}-{auto.Value}",
                $"{ParamPrefix}-{bpAuto.Value}",
                $"{ParamPrefix}-{bpn.Value}",
                $"{ParamPrefix}{sideBpAuto.Value}",
                $"{ParamPrefix}{spacing.Value}",
                $"{ParamPrefix}{spacingBpNumber.Value}",
                }.OrderBy(x => x));
        }


        [Theory(DisplayName = "Spacing number and breakpoint with spacing number in sets are validated on assignment")]
        [InlineData(-6)]
        [InlineData(6)]
        public void InvalidSpacingInSetThrows(int number)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Sut = number | lg - 2);
            Should.Throw<ArgumentOutOfRangeException>(() => Sut = lg - number | 3);
        }

        [Theory(DisplayName = "Spacing can NOT have option sets of none-spacing-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfNoneSpacingOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => Sut = set);
        }

        // TODO: ensure auto related sizes are not combinable with general spacing options
    }
}
