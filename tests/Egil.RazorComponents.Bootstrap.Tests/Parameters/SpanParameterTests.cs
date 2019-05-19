using Egil.RazorComponents.Bootstrap.Grid;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;
using static Egil.RazorComponents.Bootstrap.Options.SpacingOptions.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
{
    public class SpanParameterTests : ParameterFixture<ISpanOption>
    {
        private static readonly string ParamPrefix = "col";
        private SpanParameter? sut;

        [Fact(DisplayName = "Value returns 'col' by default")]
        public void CssClassReturnsColByDefault()
        {
            SpanParameter.Default.Single().ShouldBe(ParamPrefix);
            SpanParameter.Default.Count.ShouldBe(1);
        }

        [Theory(DisplayName = "Span can have a width specified by assignment")]
        [NumberRangeData(1, 12)]
        public void SpanCanHaveWidthSpecifiedByAssignment(int width)
        {
            sut = width;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, width);
        }

        [Theory(DisplayName = "Specifying an invalid index number throws")]
        [InlineData(0)]
        [InlineData(13)]
        public void SpecifyinInvalidIndexNumberThrows(int index)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = index);
        }

        [Fact(DisplayName = "Span can have a breakpoint specified by assignment")]
        public void SpanCanHaveBreakpointSpecifiedByAssignment()
        {
            var option = lg;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Fact(DisplayName = "Span can have a breakpoint of type extra small specified by assignment where value becomes 'col'")]
        public void SpanCanHaveBreakpointXsSpecifiedByAssignment()
        {
            var option = xs;
            sut = option;
            sut.Single().ShouldBe(ParamPrefix);
        }

        [Fact(DisplayName = "Span can have a auto specified by assignment")]
        public void SpanCanHaveAutoSpecifiedByAssignment()
        {
            var option = auto;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Span can have a breakpoint with width specified by assignment")]
        [NumberRangeData(1, 12)]
        public void SpanCanHaveBreakpointWithWidthSpecifiedByAssignment(int number)
        {
            var option = lg - number;
            sut = option;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }

        [Theory(DisplayName = "Specifying an invalid size number with breakpoint throws")]
        [InlineData(0)]
        [InlineData(13)]
        public void SpecifyinInvalidIndexNumberWithBreakpointThrows(int invalidSize)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = lg - invalidSize);
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

        [Fact(DisplayName = "When xs (default breakpoint) is in a set, the resulting value produced is 'col'")]
        public void DefaultBreakpointInSet()
        {
            var set = new OptionSet<ISpanOption>() | xs;
            sut = set;
            sut.Single().ShouldBe(ParamPrefix);
        }

        [Fact(DisplayName = "When a set contains both xs and a number, only 'col-number' is returned")]
        public void DefaultBreakpointAndNumberInSet()
        {
            var width = 4;
            var set = xs | width;
            sut = set;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, width);
            sut.ShouldNotContain(ParamPrefix);

            var reversedSet = width | xs;
            sut = reversedSet;
            sut.ShouldContainOptionsWithPrefix(ParamPrefix, width);
            sut.ShouldNotContain(ParamPrefix);
        }

        [Theory(DisplayName = "Span breakpoint with span number in sets are validated on assignment")]
        [InlineData(0)]
        [InlineData(13)]
        public void InvalidNumberInSetThrows(int number)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut = number | lg - 2);
            Should.Throw<ArgumentOutOfRangeException>(() => sut = lg - number | 3);
        }

        [Theory(DisplayName = "Span can NOT have option sets of none-span-options assigned")]
        [MemberData(nameof(IncompatibleOptionSetsFixtureData))]
        public void CanNOTHaveOptionSetOfOffsetOptionsSpecified(dynamic set)
        {
            Assert.Throws<RuntimeBinderException>(() => sut = set);
        }
    }
}
