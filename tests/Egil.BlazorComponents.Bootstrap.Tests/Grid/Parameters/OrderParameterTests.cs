using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Grid
{

    public class OrderParameterTests : ParameterTests
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
            var bp = new Breakpoint(BreakpointType.Large) - 4;
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a first option specified by assignment")]
        public void OrderCanHaveFirstOptionSpecifiedByAssignment()
        {
            var first = new First();
            sut.Order = first;
            AssertCorrectCssClass(first);
        }

        [Fact(DisplayName = "Order can have a last option specified by assignment")]
        public void OrderCanHaveLastOptionSpecifiedByAssignment()
        {
            var last = new Last();
            sut.Order = last;
            AssertCorrectCssClass(last);
        }

        [Fact(DisplayName = "Order can have a breakpoint with first optoin specified by assignment")]
        public void OrderCanHaveBreakpointWithFirstOptionSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - new First();
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a breakpoint with last optoin specified by assignment")]
        public void OrderCanHaveBreakpointWithLastOptionSpecifiedByAssignment()
        {
            var bp = new Breakpoint(BreakpointType.Large) - new Last();
            sut.Order = bp;
            AssertCorrectCssClass(bp);
        }

        [Fact(DisplayName = "Order can have a options specified via OptionSet<IOrderOption>")]
        public void OrderCanHaveOptionsSpecifiedViaOptionSetOfIOrderOption()
        {
            var first = new First();
            var last = new Last();
            OptionSet<IOrderOption> set = first | last;
            sut.Order = set;
            sut.Order.Count().ShouldBe(2);
            sut.Order.First().ShouldBe($"order-{first.Value}");
            sut.Order.Last().ShouldBe($"order-{last.Value}");
        }

        [Fact(DisplayName = "Order can have a options specified via SharedOptionSet")]
        public void OrderCanHaveOptionsSpecifiedViaSharedOptionSet()
        {
            var bpn = new BreakpointNumber(new Breakpoint(BreakpointType.Large), 3);
            SharedOptionsSet set = bpn | bpn;
            sut.Order = set;
            sut.Order.Count().ShouldBe(2);
            sut.Order.First().ShouldBe($"order-{bpn.Value}");
            sut.Order.Last().ShouldBe($"order-{bpn.Value}");
        }

        [Fact(DisplayName = "It should not be possible to assign a OptionSet<ISpanOption> - fails to compile")]
        public void AssignOptionSetOfISpanOptionFailsToCompile()
        {
            ShouldNotCompile<TestComponent, OptionSet<ISpanOption>>();

            void ShouldNotCompile<TOption1, TOption2>()
            {
                var combinationExpression = "(component, set) => { component.Order = set; }";
                var options = ScriptOptions.Default.AddReferences(typeof(OrderParameterTests).Assembly);
                var actual = Should.Throw<CompilationErrorException>(() =>
                {
                    return CSharpScript.EvaluateAsync<Action<TOption1, TOption2>>(combinationExpression, options);
                });
                actual.Message.ShouldContain("error CS0029: Cannot implicitly convert type");
            }
        }
    }
}