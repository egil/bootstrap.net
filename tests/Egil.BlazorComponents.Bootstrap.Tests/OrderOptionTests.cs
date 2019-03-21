using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class OrderOptionTests
    {
        public static readonly IEnumerable<object[]> BreakpointTypes = new EnumEnumerator<BreakpointType>();
        private Component sut = new Component();

        [Theory]
        [NumberRangeData(1, 12)]
        public void GridNumberCanBeAssignedBetween1And12(int number)
        {
            sut.Order = number;
            sut.Order.CssClass.ShouldBe($"order-{number}");
        }

        [Fact]
        public void UsingGridNumberWithInvalidNumberThrows()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => sut.Order = 0);
            Should.Throw<ArgumentOutOfRangeException>(() => sut.Order = 13);
        }

        [Fact]
        public void BreakpointAndWidthShouldBeAssignableToOrderOption()
        {
            var bp = new BreakpointWithSpan(BreakpointType.Medium, 2);
            sut.Order = bp;
            sut.Order.CssClass.ShouldBe($"order-{bp.CssClass}");
        }

        [Fact]
        public void FirstOptionsAssignable()
        {
            var first = new FirstOption();
            sut.Order = first;
            sut.Order.CssClass.ShouldBe($"order-{first.CssClass}");
        }

        [Fact]
        public void LastOptionsAssignable()
        {
            var last = new LastOption();
            sut.Order = last;
            sut.Order.CssClass.ShouldBe($"order-{last.CssClass}");
        }

        [Theory]
        [MemberData(nameof(BreakpointTypes))]
        public void BreakpointWithFirstOptionAssignable(BreakpointType type)
        {
            var bp = new Breakpoint(type) - new FirstOption();
            sut.Order = bp;
            sut.Order.CssClass.ShouldBe($"order-{bp.CssClass}");
        }

        [Theory]
        [MemberData(nameof(BreakpointTypes))]
        public void BreakpointWithLastOptionAssignable(BreakpointType type)
        {
            var bp = new Breakpoint(type) - new FirstOption();
            sut.Order = bp;
            sut.Order.CssClass.ShouldBe($"order-{bp.CssClass}");
        }
    }
}