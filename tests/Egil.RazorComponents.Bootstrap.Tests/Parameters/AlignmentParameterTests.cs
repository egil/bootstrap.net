using Egil.RazorComponents.Bootstrap.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;
using static Egil.RazorComponents.Bootstrap.Options.SpacingOptions.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
{
    public class RowAlignmentParameterTests : AlignmentParameterTests<VerticalRowAlignment>
    {
        protected override string ParamPrefix => "align-items";
    }

    public class ColumnAlignmentParameterTests : AlignmentParameterTests<VerticalColumnAlignment>
    {
        protected override string ParamPrefix => "align-self";
    }

    public abstract class AlignmentParameterTests<TParamPrefix> : ParameterFixture<IAlignmentOption>
        where TParamPrefix : ICssClassPrefix, new()
    {
        private AlignmentParameter<TParamPrefix>? Sut { get; set; }
        protected abstract string ParamPrefix { get; }

        [Fact(DisplayName = "Parameter prefix returns correct value")]
        public void ParameterPrefixReturnsCorrectValue()
        {
            new TParamPrefix().Prefix.ShouldBe(ParamPrefix);
        }

        [Fact(DisplayName = "AlignmentParameter.None returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            AlignmentParameter<TParamPrefix>.None.ShouldBeEmpty();
            AlignmentParameter<TParamPrefix>.None.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "Alignment can have a alignment-options assigned")]
        public void CanHaveAlignmentWithIndexSpecifiedByAssignment()
        {
            var option = start;
            Sut = option;
            Sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Alignment can have a breakpoint-alignment-options assigned")]
        public void CanHaveBreakpointAlignmentWithIndexSpecifiedByAssignment()
        {
            var option = lg - start;
            Sut = option;
            Sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Alignment can have option sets of alignment-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
        {
            Sut = set;
            Sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Alignment can NOT have option sets of none-alignment-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => Sut = set);
        }
    }
}
