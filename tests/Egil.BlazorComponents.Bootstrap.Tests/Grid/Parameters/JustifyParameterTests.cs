using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using Xunit;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Microsoft.CSharp.RuntimeBinder;

using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;


namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    public class JustifyParameterTests : ParameterFixture<IJustifyOption, IAlignmentOption>
    {
        private static readonly string ParamPrefix = "justify-content";
        private HorizontalAlignmentParameter? sut;

        [Fact(DisplayName = "JustifyParameter.None returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            HorizontalAlignmentParameter.None.ShouldBeEmpty();
            HorizontalAlignmentParameter.None.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "Justify can have a justify-options assigned")]
        public void CanHaveJustifyOptionsSpecifiedAssignment()
        {
            var option = between;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Justify can have a alignment-options assigned")]
        public void CanHaveAlignmentOptionsSpecifiedAssignment()
        {
            var option = center;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Justify can have option sets of justify-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfJustifyOptionsSpecified(dynamic set)
        {
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Justify can NOT have option sets of none-justify-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}
