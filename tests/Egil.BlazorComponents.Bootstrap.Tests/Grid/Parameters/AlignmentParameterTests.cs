using System;
using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;
using Microsoft.CSharp.RuntimeBinder;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    public class RowAlignmentParameterTests : ParameterFixture<IAlignmentOption>
    {
        private static readonly string ParamPrefix = "align-items";
        private RowAlignmentParameter? sut;

        [Fact(DisplayName = "AlignmentParameter.None returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            RowAlignmentParameter.None.ShouldBeEmpty();
            RowAlignmentParameter.None.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "Alignment can have a alignment-options assigned")]
        public void CanHaveAlignmentWithIndexSpecifiedByAssignment()
        {
            var option = start;
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

    public class ColAlignmentParameterTests : ParameterFixture<IAlignmentOption>
    {
        private static readonly string ParamPrefix = "align-self";
        private ColAlignmentParameter? sut;

        [Fact(DisplayName = "AlignmentParameter.None returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            RowAlignmentParameter.None.ShouldBeEmpty();
            RowAlignmentParameter.None.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "Alignment can have a alignment-options assigned")]
        public void CanHaveAlignmentWithIndexSpecifiedByAssignment()
        {
            var option = start;
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
