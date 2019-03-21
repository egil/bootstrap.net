using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public abstract class OptionTests
    {
        public static readonly IEnumerable<object[]> BreakpointTypes = new EnumEnumerator<BreakpointType>();

        protected readonly TestComponent sut = new TestComponent();        

        public class TestComponent
        {
            public OrderOption Order { get; set; } = NoneOrderOption.Instance;
            public SpanOption Span { get; set; } = new SpanOption();
        }
    }

    public class SpanOptionTests : OptionTests
    {
        void AssertCorrectCssClass(Option appliedOption)
        {
            sut.Span.CssClass.ShouldBe($"col-{appliedOption.CssClass}");
        }

        [Fact(DisplayName = "CssClass returns 'col' by default")]
        public void CssClassReturnsColByDefault()
        {
            sut.Span.CssClass.ShouldBe("col");
        }

        [Fact(DisplayName = "Span can have a width specified by assignment")]
        public void SpanCanHaveWidthSpecifiedByAssignment()
        {
            var width = 4;
            sut.Span = width;
            sut.Span.CssClass.ShouldBe($"col-{width}");
        }

        [Fact(DisplayName = "Span can have a breakpoint specified by assignment")]
        public void SpanCanHaveBreakpointSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            sut.Span = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Span can have a breakpoint with width specified by assignment")]
        public void SpanCanHaveBreakpointWithWidthSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - 4;
            sut.Span = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Span can have a breakpoint with auto option specified by assignment")]
        public void SpanCanHaveBreakpointWithAutoOptionSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - new AutoOption();
            sut.Span = bp;
            AssertCorrectCssClass(bp);
        }

        //[Fact(DisplayName = "Span width and breakpoint can be combined using the | operator")]
        //public void SpanWidthAndBreakpointCanBeCombinedUsingOrOperator()
        //{
        //    var bp = new Breakpoint(BreakpointType.Large);
        //    int width = 3;
        //    sut.Span = 3 | bp;
        //}



        //[Fact(DisplayName = "Breakpoint and number option combined with a number using | operator")]
        //public void BreakpointAndNumberOption()
        //{
        //    var number = 3;
        //    var bp = new Breakpoint(BreakpointType.Medium);
        //    sut.Span = bp | number;
        //    os.Option.ShouldBe(bp);
        //    os.Number.ShouldBe(number);

        //    sut.Span = number | bp;
        //    osReverse.Breakpoint.ShouldBe(bp);
        //    osReverse.Number.ShouldBe(number);
        //}
    }

    public class OrderOptionTests : OptionTests
    {
        void AssertCorrectCssClass(Option appliedOption)
        {
            sut.Order.CssClass.ShouldBe($"order-{appliedOption.CssClass}");
        }

        [Theory(DisplayName = "Order can have valid index number specified by assignment")]
        [NumberRangeData(1, 12)]
        public void OrderCanHaveValidIndexNumberSpecifiedByAssignment(int number)
        {
            sut.Order = number;
            sut.Order.CssClass.ShouldBe($"order-{number}");
        }

        [Fact(DisplayName = "Specifying an invalid index number throws")]
        public void SpecifyinInvalidIndexNumberThrows()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut.Order = 0);
            Should.Throw<ArgumentOutOfRangeException>(() => sut.Order = 13);
        }

        [Fact(DisplayName = "Order can have a breakpoint with index specified by assignment")]
        public void OrderCanHaveBreakpointWithIndexSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - 4;
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a first option specified by assignment")]
        public void OrderCanHaveFirstOptionSpecifiedByAssignment()
        {
            var first = new FirstOption();
            sut.Order = first;
            AssertCorrectCssClass(first);
        }

        [Fact(DisplayName = "Order can have a last option specified by assignment")]
        public void OrderCanHaveLastOptionSpecifiedByAssignment()
        {
            var last = new LastOption();
            sut.Order = last;
            AssertCorrectCssClass(last);
        }

        [Fact(DisplayName = "Order can have a breakpoint with first optoin specified by assignment")]
        public void OrderCanHaveBreakpointWithFirstOptionSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - new FirstOption();
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a breakpoint with last optoin specified by assignment")]
        public void OrderCanHaveBreakpointWithLastOptionSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - new LastOption();
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }
    }
}