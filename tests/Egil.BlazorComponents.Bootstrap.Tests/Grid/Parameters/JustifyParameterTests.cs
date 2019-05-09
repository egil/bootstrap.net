using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using Xunit;
using Microsoft.CSharp.RuntimeBinder;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    //public class JustifyParameterTests : ParameterFixture<IJustifyOption>
    //{
    //    private static readonly string ParamPrefix = "justify-content";
    //    private JustifyParameter? sut;

    //    [Fact(DisplayName = "JustifyParameter.None returns empty string")]
    //    public void NoOptionsResultsInEmptyValue()
    //    {
    //        JustifyParameter.None.ShouldBeEmpty();
    //        JustifyParameter.None.Count.ShouldBe(0);
    //    }

    //    [Fact(DisplayName = "Justify can have a justify-options assigned")]
    //    public void CanHaveAlignmentWithIndexSpecifiedByAssignment()
    //    {
    //        var option = between;
    //        sut = option;
    //        sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
    //    }

    //    [Theory(DisplayName = "Justify can have option sets of justify-options assigned")]
    //    [MemberData(nameof(SutOptionSetsFixtureData))]
    //    public void CanHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
    //    {
    //        sut = set;
    //        sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
    //    }

    //    [Theory(DisplayName = "Justify can NOT have option sets of none-justify-options assigned")]
    //    [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
    //    public void CanNOTHaveOptionSetOfAlignmentOptionsSpecified(dynamic set)
    //    {
    //        Assert.Throws<RuntimeBinderException>(() => sut = set);
    //    }
    //}
}
