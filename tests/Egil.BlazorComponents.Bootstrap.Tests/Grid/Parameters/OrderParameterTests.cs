using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shouldly;
using System;
using System.Linq;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Grid
{

    public class OrderParameterTests : ParameterFixture
    {
        void AssertCorrectCssClass(IOrderOption appliedOption)
        {
            sut.Order.Single().ShouldBe($"order-{appliedOption.Value}");
        }

        [Fact(DisplayName = "OrderParameter.None.Value returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            OrderParameter.None.ShouldBeEmpty();
        }

        [Theory(DisplayName = "Order can have valid index number specified by assignment")]
        [NumberRangeData(1, 12)]
        public void OrderCanHaveValidIndexNumberSpecifiedByAssignment(int number)
        {
            sut.Order = number;
            sut.Order.Single().ShouldBe($"order-{number}");
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
            var bp = lg - 4;
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a first option specified by assignment")]
        public void OrderCanHaveFirstOptionSpecifiedByAssignment()
        {
            sut.Order = first;
            AssertCorrectCssClass(first);
        }

        [Fact(DisplayName = "Order can have a last option specified by assignment")]
        public void OrderCanHaveLastOptionSpecifiedByAssignment()
        {
            sut.Order = last;
            AssertCorrectCssClass(last);
        }

        [Fact(DisplayName = "Order can have a breakpoint with first option specified by assignment")]
        public void OrderCanHaveBreakpointWithFirstOptionSpecifiedByAssignment()
        {
            var bp = sm - first;
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a breakpoint with last option specified by assignment")]
        public void OrderCanHaveBreakpointWithLastOptionSpecifiedByAssignment()
        {
            var bp = lg - last;
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have multiple combined order-only options specified")]
        public void OrderCanHaveOptionsSpecifiedViaOptionSetOfIOrderOption()
        {
            sut.Order = first | last | sm - first | md - last;
            sut.Order.ShouldAllBe(x => x.StartsWith("order-"));
            sut.Order.Count().ShouldBe(4);
        }

        [Fact(DisplayName = "Order can have multiple combined grid-breakpoints options specified")]
        public void OrderCanHaveOptionsSpecifiedViaSharedOptionSet()
        {
            sut.Order = 2 | md - 4 | lg - 8;
            sut.Order.Count().ShouldBe(3);
            sut.Order.ShouldAllBe(x => x.StartsWith("order-"));
        }
    }
}