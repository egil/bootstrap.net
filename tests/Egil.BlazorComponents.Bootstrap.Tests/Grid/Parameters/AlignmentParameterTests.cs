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
    public class RowAlignmentParameterTests : AlignmentParameterTests<VerticalRowAlignment>
    {
        protected override string ParamPrefix => "align-items";
    }

    public class ColumnAlignmentParameterTests : AlignmentParameterTests<VerticalColumnAlignment>
    {
        protected override string ParamPrefix => "align-self";
    }

    public abstract class AlignmentParameterTests<TParamPrefix> : ParameterFixture<IAlignmentOption>
        where TParamPrefix : IParameterPrefix, new()
    {
        private AlignmentParameter<TParamPrefix>? sut;
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
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Alignment can have a breakpoint-alignment-options assigned")]
        public void CanHaveBreakpointAlignmentWithIndexSpecifiedByAssignment()
        {
            var option = lg - start;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Alignment can have option sets of alignment-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
        {
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Alignment can NOT have option sets of none-alignment-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}
