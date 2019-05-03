using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Grid
{
    public class SpanOptionTests : ParameterFixture
    {
        void AssertCorrectCssClass(ISpanOption appliedOption)
        {
            sut.Span.Single().ShouldBe($"col-{appliedOption.Value}");
        }

        [Fact(DisplayName = "Value returns 'col' by default")]
        public void CssClassReturnsColByDefault()
        {
            sut.Span.Single().ShouldBe("col");
        }

        [Fact(DisplayName = "Span can have a width specified by assignment")]
        public void SpanCanHaveWidthSpecifiedByAssignment()
        {
            var width = 4;
            sut.Span = width;
            sut.Span.Single().ShouldBe($"col-{width}");
        }

        [Fact(DisplayName = "Span can have a breakpoint specified by assignment")]
        public void SpanCanHaveBreakpointSpecifiedByAssignment()
        {
            var bp = lg;
            sut.Span = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Span can have a breakpoint with width specified by assignment")]
        public void SpanCanHaveBreakpointWithWidthSpecifiedByAssignment()
        {
            var bp = lg - 4;
            sut.Span = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Span can have a breakpoint with auto option specified by assignment")]
        public void SpanCanHaveBreakpointWithAutoOptionSpecifiedByAssignment()
        {
            var bp = lg - auto;
            sut.Span = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Span can have multiple combined span-only options specified ")]
        public void SpanCanHaveOptionsSpecifiedViaOptionSetOfISpanOption()
        {
            sut.Span = md | auto | lg-auto;
            sut.Span.ShouldAllBe(x => x.StartsWith("col-"));
            sut.Span.Count().ShouldBe(3);
        }

        [Fact(DisplayName = "Span can have multiple combined 'breakpoint-number' options specified")]
        public void SpanCanHaveOptionsSpecifiedViaSharedOptionSet()
        {
            sut.Span = md - 4 | lg - 8;
            sut.Span.Count().ShouldBe(2);
            sut.Span.ShouldAllBe(x => x.StartsWith("col-"));
        }
    }
}
