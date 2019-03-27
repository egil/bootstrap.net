//using Egil.BlazorComponents.Bootstrap.Grid.Options;
//using Shouldly;
//using System;
//using Xunit;

//namespace Egil.BlazorComponents.Bootstrap.Grid
//{

//    public class OrderParameterTests : ParameterTests
//    {
//        void AssertCorrectCssClass(Option appliedOption)
//        {
//            sut.Order.Value.ShouldBe($"order-{appliedOption.CssClass}");
//        }

//        [Theory(DisplayName = "Order can have valid index number specified by assignment")]
//        [NumberRangeData(1, 12)]
//        public void OrderCanHaveValidIndexNumberSpecifiedByAssignment(int number)
//        {
//            sut.Order = number;
//            sut.Order.Value.ShouldBe($"order-{number}");
//        }

//        [Fact(DisplayName = "Specifying an invalid index number throws")]
//        public void SpecifyinInvalidIndexNumberThrows()
//        {
//            Should.Throw<ArgumentOutOfRangeException>(() => sut.Order = 0);
//            Should.Throw<ArgumentOutOfRangeException>(() => sut.Order = 13);
//        }

//        [Fact(DisplayName = "Order can have a breakpoint with index specified by assignment")]
//        public void OrderCanHaveBreakpointWithIndexSpecifiedByAssignment()
//        {
//            var bp = new Breakpoint(BreakpointType.Large) - 4;
//            sut.Order = bp;
//            AssertCorrectCssClass(bp);
//        }

//        [Fact(DisplayName = "Order can have a first option specified by assignment")]
//        public void OrderCanHaveFirstOptionSpecifiedByAssignment()
//        {
//            var first = new FirstOption();
//            sut.Order = first;
//            AssertCorrectCssClass(first);
//        }

//        [Fact(DisplayName = "Order can have a last option specified by assignment")]
//        public void OrderCanHaveLastOptionSpecifiedByAssignment()
//        {
//            var last = new LastOption();
//            sut.Order = last;
//            AssertCorrectCssClass(last);
//        }

//        [Fact(DisplayName = "Order can have a breakpoint with first optoin specified by assignment")]
//        public void OrderCanHaveBreakpointWithFirstOptionSpecifiedByAssignment()
//        {
//            var bp = new Breakpoint(BreakpointType.Large) - new FirstOption();
//            sut.Order = bp;
//            AssertCorrectCssClass(bp);
//        }

//        [Fact(DisplayName = "Order can have a breakpoint with last optoin specified by assignment")]
//        public void OrderCanHaveBreakpointWithLastOptionSpecifiedByAssignment()
//        {
//            var bp = new Breakpoint(BreakpointType.Large) - new LastOption();
//            sut.Order = bp;
//            AssertCorrectCssClass(bp);
//        }
//    }
//}