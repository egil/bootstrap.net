using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Grid
{
    public class SpanParameterTests : ParameterFixture<ISpanOption>
    {
        private static readonly string ParamPrefix = "col";
        private SpanParameter? sut;

        [Fact(DisplayName = "Value returns 'col' by default")]
        public void CssClassReturnsColByDefault()
        {
            SpanParameter.Default.Single().ShouldBe("col");
            SpanParameter.Default.Count.ShouldBe(1);
        }

        [Theory(DisplayName = "Span can have a width specified by assignment")]
        [NumberRangeData(1, 12)]
        public void SpanCanHaveWidthSpecifiedByAssignment(int width)
        {
            sut = width;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, width);
        }

        [Fact(DisplayName = "Specifying an invalid index number throws")]
        public void SpecifyinInvalidIndexNumberThrows()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = 0);
            Should.Throw<ArgumentOutOfRangeException>(() => sut = 13);
        }

        [Fact(DisplayName = "Span can have a breakpoint specified by assignment")]
        public void SpanCanHaveBreakpointSpecifiedByAssignment()
        {
            var option = lg;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Span can have a auto specified by assignment")]
        public void SpanCanHaveAutoSpecifiedByAssignment()
        {
            var option = auto;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Span can have a breakpoint with width specified by assignment")]
        public void SpanCanHaveBreakpointWithWidthSpecifiedByAssignment()
        {
            var option = lg - 4;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Span can have a breakpoint with auto option specified by assignment")]
        public void SpanCanHaveBreakpointWithAutoOptionSpecifiedByAssignment()
        {
            var option = lg - auto;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Span can have option sets of span-options assigned")]
        [MemberData(nameof(SutOptionSetsFixtureData))]
        public void CanHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, (IOptionSet<IOption>)set);
        }

        [Theory(DisplayName = "Span can NOT have option sets of none-span-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}
