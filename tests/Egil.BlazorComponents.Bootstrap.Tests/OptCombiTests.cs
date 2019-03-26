using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Xunit.Abstractions;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class OptCombiTests
    {
        interface IOption : IOrderOption, ISpanOption { }
        interface IOrderOption { }
        interface ISpanOption { }

        class OptionSet : List<IOption>
        {
            public static OptionSet operator |(OptionSet set, int number)
            {
                set.Add(new Number());
                return set;
            }
            public static OptionSet operator |(OptionSet set, BreakpointNumber option)
            {
                set.Add(option);
                return set;
            }
            public static SpanOptionSet operator |(OptionSet set, Auto option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static SpanOptionSet operator |(Auto option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static SpanOptionSet operator |(OptionSet set, Breakpoint option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static SpanOptionSet operator |(Breakpoint option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static SpanOptionSet operator |(OptionSet set, BreakpointAuto option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static SpanOptionSet operator |(BreakpointAuto option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(OptionSet set, First option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(First option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(OptionSet set, Last option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(Last option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(OptionSet set, BreakpointFirst option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(BreakpointFirst option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(OptionSet set, BreakpointLast option)
            {
                return CreateOrderSetFrom(set, option);
            }
            public static OrderOptionSet operator |(BreakpointLast option, OptionSet set)
            {
                return CreateOrderSetFrom(set, option);
            }
            static OrderOptionSet CreateOrderSetFrom(OptionSet set, IOrderOption option)
            {
                var res = new OrderOptionSet();
                res.AddRange(set);
                res.Add(option);
                return res;
            }
            static SpanOptionSet CreateOrderSetFrom(OptionSet set, ISpanOption option)
            {
                var res = new SpanOptionSet();
                res.AddRange(set);
                res.Add(option);
                return res;
            }
        }

        class SpanOptionSet : List<ISpanOption>
        {
            public static SpanOptionSet operator |(SpanOptionSet set, int number)
            {
                set.Add(new Number());
                return set;
            }

            public static SpanOptionSet operator |(SpanOptionSet set, Auto option)
            {
                set.Add(option);
                return set;
            }

            public static SpanOptionSet operator |(SpanOptionSet set, Breakpoint option)
            {
                set.Add(option);
                return set;
            }

            public static SpanOptionSet operator |(SpanOptionSet set, BreakpointNumber option)
            {
                set.Add(option);
                return set;
            }

            public static SpanOptionSet operator |(SpanOptionSet set, BreakpointAuto option)
            {
                set.Add(option);
                return set;
            }
        }
        class OrderOptionSet : List<IOrderOption>
        {
            public static OrderOptionSet operator |(OrderOptionSet set, int number)
            {
                set.Add(new Number());
                return set;
            }
            public static OrderOptionSet operator |(OrderOptionSet set, First option)
            {
                set.Add(option);
                return set;
            }
            public static OrderOptionSet operator |(OrderOptionSet set, Last option)
            {
                set.Add(option);
                return set;
            }
            public static OrderOptionSet operator |(OrderOptionSet set, BreakpointNumber option)
            {
                set.Add(option);
                return set;
            }
            public static OrderOptionSet operator |(OrderOptionSet set, BreakpointFirst option)
            {
                set.Add(option);
                return set;
            }
            public static OrderOptionSet operator |(OrderOptionSet set, BreakpointLast option)
            {
                set.Add(option);
                return set;
            }
        }

        class Number : IOption, ISpanOption, IOrderOption { }

        class SpanOption<T> : ISpanOption where T : ISpanOption
        {
            public static SpanOptionSet operator |(int number, SpanOption<T> option)
            {
                return new SpanOptionSet { new Number(), option };
            }
            public static SpanOptionSet operator |(SpanOption<T> option, int number)
            {
                return new SpanOptionSet { new Number(), option };
            }
            public static SpanOptionSet operator |(SpanOption<T> option1, Breakpoint option2)
            {
                return new SpanOptionSet { option1, option2 };
            }
            public static SpanOptionSet operator |(SpanOption<T> option1, Auto option2)
            {
                return new SpanOptionSet { option1, option2 };
            }
            public static SpanOptionSet operator |(SpanOption<T> option1, BreakpointNumber option2)
            {
                return new SpanOptionSet { option1, option2 };
            }

            public static SpanOptionSet operator |(SpanOption<T> option1, BreakpointAuto option2)
            {
                return new SpanOptionSet { option1, option2 };
            }
        }

        class Breakpoint : SpanOption<Breakpoint>, ISpanOption
        {
        }

        class BreakpointAuto : SpanOption<BreakpointAuto>, ISpanOption
        {
            //public static SpanOptionSet operator |(int number, BreakpointAuto option)
            //{
            //    return new SpanOptionSet { new Number(), option };
            //}

            //public static SpanOptionSet operator |(BreakpointAuto option, int number)
            //{
            //    return new SpanOptionSet { new Number(), option };
            //}

            //public static SpanOptionSet operator |(BreakpointAuto option1, BreakpointAuto option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
            //public static SpanOptionSet operator |(BreakpointAuto option1, Auto option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
            //public static SpanOptionSet operator |(BreakpointAuto option1, Breakpoint option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
            //public static SpanOptionSet operator |(BreakpointAuto option1, BreakpointNumber option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
        }

        class Auto : SpanOption<Auto>, ISpanOption
        {
            //public static SpanOptionSet operator |(int number, Auto option)
            //{
            //    return new SpanOptionSet { new Number(), option };
            //}
            //public static SpanOptionSet operator |(Auto option, int number)
            //{
            //    return new SpanOptionSet { new Number(), option };
            //}
            //public static SpanOptionSet operator |(Auto option1, Breakpoint option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
            //public static SpanOptionSet operator |(Auto option1, BreakpointNumber option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
            //public static SpanOptionSet operator |(Auto option1, BreakpointAuto option2)
            //{
            //    return new SpanOptionSet { option1, option2 };
            //}
        }

        class BreakpointNumber : IOption, ISpanOption, IOrderOption
        {
            public static OptionSet operator |(int number, BreakpointNumber option)
            {
                return new OptionSet { new Number(), option };
            }

            public static OptionSet operator |(BreakpointNumber option, int number)
            {
                return new OptionSet { new Number(), option };
            }
            public static OptionSet operator |(BreakpointNumber option1, BreakpointNumber option2)
            {
                return new OptionSet { option1, option2 };
            }

            public static SpanOptionSet operator |(BreakpointNumber option1, Auto option2)
            {
                return new SpanOptionSet { option1, option2 };
            }
            public static SpanOptionSet operator |(BreakpointNumber option1, Breakpoint option2)
            {
                return new SpanOptionSet { option1, option2 };
            }
            public static SpanOptionSet operator |(BreakpointNumber option1, BreakpointAuto option2)
            {
                return new SpanOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(BreakpointNumber option1, First option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(BreakpointNumber option1, Last option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(BreakpointNumber option1, BreakpointFirst option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(BreakpointNumber option1, BreakpointLast option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
        }

        class OrderOption<T> : IOrderOption
            where T : IOrderOption
        {
            public static OrderOptionSet operator |(int number, OrderOption<T> option)
            {
                return new OrderOptionSet { new Number(), option };
            }
            public static OrderOptionSet operator |(OrderOption<T> option, int number)
            {
                return new OrderOptionSet { new Number(), option };
            }
            public static OrderOptionSet operator |(OrderOption<T> option1, First option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(OrderOption<T> option1, Last option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(OrderOption<T> option1, BreakpointNumber option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(OrderOption<T> option1, BreakpointFirst option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
            public static OrderOptionSet operator |(OrderOption<T> option1, BreakpointLast option2)
            {
                return new OrderOptionSet { option1, option2 };
            }
        }

        class First : OrderOption<First>, IOrderOption
        {
            //public static OrderOptionSet operator |(int number, First option)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(First option, int number)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(First option1, Last option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(First option1, BreakpointNumber option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(First option1, BreakpointFirst option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(First option1, BreakpointLast option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
        }

        class Last : OrderOption<Last>, IOrderOption
        {
            //public static OrderOptionSet operator |(int number, Last option)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(Last option, int number)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(Last option1, First option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(Last option1, BreakpointNumber option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(Last option1, BreakpointFirst option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(Last option1, BreakpointLast option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
        }

        class BreakpointFirst : OrderOption<BreakpointLast>, IOrderOption
        {
            //public static OrderOptionSet operator |(int number, BreakpointFirst option)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}

            //public static OrderOptionSet operator |(BreakpointFirst option, int number)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(BreakpointFirst option1, BreakpointFirst option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(BreakpointFirst option1, First option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}

            //public static OrderOptionSet operator |(BreakpointFirst option1, Last option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(BreakpointFirst option1, BreakpointNumber option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(BreakpointFirst option1, BreakpointLast option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
        }

        class BreakpointLast : OrderOption<BreakpointLast>, IOrderOption
        {
            //public static OrderOptionSet operator |(int number, BreakpointLast option)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(BreakpointLast option, int number)
            //{
            //    return new OrderOptionSet { new Number(), option };
            //}
            //public static OrderOptionSet operator |(BreakpointLast option1, BreakpointLast option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}

            //public static OrderOptionSet operator |(BreakpointLast option1, First option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(BreakpointLast option1, Last option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(BreakpointLast option1, BreakpointNumber option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
            //public static OrderOptionSet operator |(BreakpointLast option1, BreakpointFirst option2)
            //{
            //    return new OrderOptionSet { option1, option2 };
            //}
        }

        /*
         * SpanOptionSet : number, auto, breakpoint, breakpointWithNumber, breakpoint-auto.
         * OrderOptionSet: number, first, last, breakpointWithNumber, breakpoint-first, breakpoint-last.
         * OptionSet:  number, breakpointWithNumber.
         */
        private readonly ITestOutputHelper output;

        public OptCombiTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        #region Generators
        public static IEnumerable<IEnumerable<T>> Get<T>(IEnumerable<T> set, IEnumerable<T>? subset = null)
        {
            if (subset == null) subset = new T[] { };
            if (!set.Any()) yield return subset;

            for (var i = 0; i < set.Count(); i++)
            {
                var newSubset = set.Take(i).Concat(set.Skip(i + 1));
                foreach (var permutation in Get(newSubset, subset.Concat(set.Skip(i).Take(1))))
                {
                    yield return permutation;
                }
            }
        }

        [Fact(Skip = "generator")]
        public void OptionSetPermutationsCreator()
        {
            var options = new[] { "n", "bpn", "bpn", };
            var permutations = Get(options).Select(x => string.Join(" | ", x));
            foreach (var item in permutations)
            {
                output.WriteLine($"({item}).ShouldBeOfType<OptionSet>();");
            }
        }


        [Fact(Skip = "generator")]
        public void SpanOptionSetPermutationsCreator()
        {
            var options = new[] { "n", "auto", "bp", "bpn", "bpa", };
            var permutations = Get(options).Select(x => string.Join(" | ", x));
            foreach (var item in permutations)
            {
                output.WriteLine($"({item}).ShouldBeOfType<SpanOptionSet>();");
            }
        }

        [Fact(Skip = "generator")]
        public void OrderOptionSetPermutationsCreator()
        {
            // number, first, last, breakpointWithNumber, breakpointFirst, breakpointLast
            var options = new[] { "n", "first", "last", "bpn", "bpf", "bpl" };
            var permutations = Get(options).Select(x => string.Join(" | ", x));
            foreach (var item in permutations)
            {
                output.WriteLine($"({item}).ShouldBeOfType<OrderOptionSet>();");
            }
        }
        #endregion

        [Fact]
        public void OptionSetTypesAreCorrect()
        {
            int n = 2;
            BreakpointNumber bpn = new BreakpointNumber();

            (n | bpn | bpn).ShouldBeOfType<OptionSet>();
            (n | bpn | bpn).ShouldBeOfType<OptionSet>();
            (bpn | n | bpn).ShouldBeOfType<OptionSet>();
            (bpn | bpn | n).ShouldBeOfType<OptionSet>();
            (bpn | n | bpn).ShouldBeOfType<OptionSet>();
            (bpn | bpn | n).ShouldBeOfType<OptionSet>();
        }

        [Fact]
        public void SpanOptionSetCombinedWithOptionSet()
        {
            OptionSet optSet = new OptionSet();
            Auto auto = new Auto();
            Breakpoint bp = new Breakpoint();
            BreakpointAuto bpa = new BreakpointAuto();

            (optSet | auto).ShouldBeOfType<SpanOptionSet>();
            (auto | optSet).ShouldBeOfType<SpanOptionSet>();
            (optSet | bp).ShouldBeOfType<SpanOptionSet>();
            (bp | optSet).ShouldBeOfType<SpanOptionSet>();
            (optSet | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpa | optSet).ShouldBeOfType<SpanOptionSet>();
        }

        [Fact]
        public void SpanOptionSetTypesAreCorrect()
        {
            int n = 2;
            Auto auto = new Auto();
            Breakpoint bp = new Breakpoint();
            BreakpointNumber bpn = new BreakpointNumber();
            BreakpointAuto bpa = new BreakpointAuto();

            (bp | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpa).ShouldBeOfType<SpanOptionSet>();

            #region Generated combinations 
            (n | auto | bp | bpn | bpa).ShouldBeOfType<SpanOptionSet>();
            (n | auto | bp | bpa | bpn).ShouldBeOfType<SpanOptionSet>();
            (n | auto | bpn | bp | bpa).ShouldBeOfType<SpanOptionSet>();
            (n | auto | bpn | bpa | bp).ShouldBeOfType<SpanOptionSet>();
            (n | auto | bpa | bp | bpn).ShouldBeOfType<SpanOptionSet>();
            (n | auto | bpa | bpn | bp).ShouldBeOfType<SpanOptionSet>();
            (n | bp | auto | bpn | bpa).ShouldBeOfType<SpanOptionSet>();
            (n | bp | auto | bpa | bpn).ShouldBeOfType<SpanOptionSet>();
            (n | bp | bpn | auto | bpa).ShouldBeOfType<SpanOptionSet>();
            (n | bp | bpn | bpa | auto).ShouldBeOfType<SpanOptionSet>();
            (n | bp | bpa | auto | bpn).ShouldBeOfType<SpanOptionSet>();
            (n | bp | bpa | bpn | auto).ShouldBeOfType<SpanOptionSet>();
            (n | bpn | auto | bp | bpa).ShouldBeOfType<SpanOptionSet>();
            (n | bpn | auto | bpa | bp).ShouldBeOfType<SpanOptionSet>();
            (n | bpn | bp | auto | bpa).ShouldBeOfType<SpanOptionSet>();
            (n | bpn | bp | bpa | auto).ShouldBeOfType<SpanOptionSet>();
            (n | bpn | bpa | auto | bp).ShouldBeOfType<SpanOptionSet>();
            (n | bpn | bpa | bp | auto).ShouldBeOfType<SpanOptionSet>();
            (n | bpa | auto | bp | bpn).ShouldBeOfType<SpanOptionSet>();
            (n | bpa | auto | bpn | bp).ShouldBeOfType<SpanOptionSet>();
            (n | bpa | bp | auto | bpn).ShouldBeOfType<SpanOptionSet>();
            (n | bpa | bp | bpn | auto).ShouldBeOfType<SpanOptionSet>();
            (n | bpa | bpn | auto | bp).ShouldBeOfType<SpanOptionSet>();
            (n | bpa | bpn | bp | auto).ShouldBeOfType<SpanOptionSet>();
            (auto | n | bp | bpn | bpa).ShouldBeOfType<SpanOptionSet>();
            (auto | n | bp | bpa | bpn).ShouldBeOfType<SpanOptionSet>();
            (auto | n | bpn | bp | bpa).ShouldBeOfType<SpanOptionSet>();
            (auto | n | bpn | bpa | bp).ShouldBeOfType<SpanOptionSet>();
            (auto | n | bpa | bp | bpn).ShouldBeOfType<SpanOptionSet>();
            (auto | n | bpa | bpn | bp).ShouldBeOfType<SpanOptionSet>();
            (auto | bp | n | bpn | bpa).ShouldBeOfType<SpanOptionSet>();
            (auto | bp | n | bpa | bpn).ShouldBeOfType<SpanOptionSet>();
            (auto | bp | bpn | n | bpa).ShouldBeOfType<SpanOptionSet>();
            (auto | bp | bpn | bpa | n).ShouldBeOfType<SpanOptionSet>();
            (auto | bp | bpa | n | bpn).ShouldBeOfType<SpanOptionSet>();
            (auto | bp | bpa | bpn | n).ShouldBeOfType<SpanOptionSet>();
            (auto | bpn | n | bp | bpa).ShouldBeOfType<SpanOptionSet>();
            (auto | bpn | n | bpa | bp).ShouldBeOfType<SpanOptionSet>();
            (auto | bpn | bp | n | bpa).ShouldBeOfType<SpanOptionSet>();
            (auto | bpn | bp | bpa | n).ShouldBeOfType<SpanOptionSet>();
            (auto | bpn | bpa | n | bp).ShouldBeOfType<SpanOptionSet>();
            (auto | bpn | bpa | bp | n).ShouldBeOfType<SpanOptionSet>();
            (auto | bpa | n | bp | bpn).ShouldBeOfType<SpanOptionSet>();
            (auto | bpa | n | bpn | bp).ShouldBeOfType<SpanOptionSet>();
            (auto | bpa | bp | n | bpn).ShouldBeOfType<SpanOptionSet>();
            (auto | bpa | bp | bpn | n).ShouldBeOfType<SpanOptionSet>();
            (auto | bpa | bpn | n | bp).ShouldBeOfType<SpanOptionSet>();
            (auto | bpa | bpn | bp | n).ShouldBeOfType<SpanOptionSet>();
            (bp | n | auto | bpn | bpa).ShouldBeOfType<SpanOptionSet>();
            (bp | n | auto | bpa | bpn).ShouldBeOfType<SpanOptionSet>();
            (bp | n | bpn | auto | bpa).ShouldBeOfType<SpanOptionSet>();
            (bp | n | bpn | bpa | auto).ShouldBeOfType<SpanOptionSet>();
            (bp | n | bpa | auto | bpn).ShouldBeOfType<SpanOptionSet>();
            (bp | n | bpa | bpn | auto).ShouldBeOfType<SpanOptionSet>();
            (bp | auto | n | bpn | bpa).ShouldBeOfType<SpanOptionSet>();
            (bp | auto | n | bpa | bpn).ShouldBeOfType<SpanOptionSet>();
            (bp | auto | bpn | n | bpa).ShouldBeOfType<SpanOptionSet>();
            (bp | auto | bpn | bpa | n).ShouldBeOfType<SpanOptionSet>();
            (bp | auto | bpa | n | bpn).ShouldBeOfType<SpanOptionSet>();
            (bp | auto | bpa | bpn | n).ShouldBeOfType<SpanOptionSet>();
            (bp | bpn | n | auto | bpa).ShouldBeOfType<SpanOptionSet>();
            (bp | bpn | n | bpa | auto).ShouldBeOfType<SpanOptionSet>();
            (bp | bpn | auto | n | bpa).ShouldBeOfType<SpanOptionSet>();
            (bp | bpn | auto | bpa | n).ShouldBeOfType<SpanOptionSet>();
            (bp | bpn | bpa | n | auto).ShouldBeOfType<SpanOptionSet>();
            (bp | bpn | bpa | auto | n).ShouldBeOfType<SpanOptionSet>();
            (bp | bpa | n | auto | bpn).ShouldBeOfType<SpanOptionSet>();
            (bp | bpa | n | bpn | auto).ShouldBeOfType<SpanOptionSet>();
            (bp | bpa | auto | n | bpn).ShouldBeOfType<SpanOptionSet>();
            (bp | bpa | auto | bpn | n).ShouldBeOfType<SpanOptionSet>();
            (bp | bpa | bpn | n | auto).ShouldBeOfType<SpanOptionSet>();
            (bp | bpa | bpn | auto | n).ShouldBeOfType<SpanOptionSet>();
            (bpn | n | auto | bp | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpn | n | auto | bpa | bp).ShouldBeOfType<SpanOptionSet>();
            (bpn | n | bp | auto | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpn | n | bp | bpa | auto).ShouldBeOfType<SpanOptionSet>();
            (bpn | n | bpa | auto | bp).ShouldBeOfType<SpanOptionSet>();
            (bpn | n | bpa | bp | auto).ShouldBeOfType<SpanOptionSet>();
            (bpn | auto | n | bp | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpn | auto | n | bpa | bp).ShouldBeOfType<SpanOptionSet>();
            (bpn | auto | bp | n | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpn | auto | bp | bpa | n).ShouldBeOfType<SpanOptionSet>();
            (bpn | auto | bpa | n | bp).ShouldBeOfType<SpanOptionSet>();
            (bpn | auto | bpa | bp | n).ShouldBeOfType<SpanOptionSet>();
            (bpn | bp | n | auto | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpn | bp | n | bpa | auto).ShouldBeOfType<SpanOptionSet>();
            (bpn | bp | auto | n | bpa).ShouldBeOfType<SpanOptionSet>();
            (bpn | bp | auto | bpa | n).ShouldBeOfType<SpanOptionSet>();
            (bpn | bp | bpa | n | auto).ShouldBeOfType<SpanOptionSet>();
            (bpn | bp | bpa | auto | n).ShouldBeOfType<SpanOptionSet>();
            (bpn | bpa | n | auto | bp).ShouldBeOfType<SpanOptionSet>();
            (bpn | bpa | n | bp | auto).ShouldBeOfType<SpanOptionSet>();
            (bpn | bpa | auto | n | bp).ShouldBeOfType<SpanOptionSet>();
            (bpn | bpa | auto | bp | n).ShouldBeOfType<SpanOptionSet>();
            (bpn | bpa | bp | n | auto).ShouldBeOfType<SpanOptionSet>();
            (bpn | bpa | bp | auto | n).ShouldBeOfType<SpanOptionSet>();
            (bpa | n | auto | bp | bpn).ShouldBeOfType<SpanOptionSet>();
            (bpa | n | auto | bpn | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | n | bp | auto | bpn).ShouldBeOfType<SpanOptionSet>();
            (bpa | n | bp | bpn | auto).ShouldBeOfType<SpanOptionSet>();
            (bpa | n | bpn | auto | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | n | bpn | bp | auto).ShouldBeOfType<SpanOptionSet>();
            (bpa | auto | n | bp | bpn).ShouldBeOfType<SpanOptionSet>();
            (bpa | auto | n | bpn | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | auto | bp | n | bpn).ShouldBeOfType<SpanOptionSet>();
            (bpa | auto | bp | bpn | n).ShouldBeOfType<SpanOptionSet>();
            (bpa | auto | bpn | n | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | auto | bpn | bp | n).ShouldBeOfType<SpanOptionSet>();
            (bpa | bp | n | auto | bpn).ShouldBeOfType<SpanOptionSet>();
            (bpa | bp | n | bpn | auto).ShouldBeOfType<SpanOptionSet>();
            (bpa | bp | auto | n | bpn).ShouldBeOfType<SpanOptionSet>();
            (bpa | bp | auto | bpn | n).ShouldBeOfType<SpanOptionSet>();
            (bpa | bp | bpn | n | auto).ShouldBeOfType<SpanOptionSet>();
            (bpa | bp | bpn | auto | n).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpn | n | auto | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpn | n | bp | auto).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpn | auto | n | bp).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpn | auto | bp | n).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpn | bp | n | auto).ShouldBeOfType<SpanOptionSet>();
            (bpa | bpn | bp | auto | n).ShouldBeOfType<SpanOptionSet>();
            #endregion
        }

        [Fact]
        public void OrderOptionSetCombinedWithOptionSet()
        {
            OptionSet optSet = new OptionSet();
            First first = new First();
            Last last = new Last();
            BreakpointFirst bpf = new BreakpointFirst();
            BreakpointLast bpl = new BreakpointLast();

            (optSet | first).ShouldBeOfType<OrderOptionSet>();
            (first | optSet).ShouldBeOfType<OrderOptionSet>();
            (optSet | last).ShouldBeOfType<OrderOptionSet>();
            (last | optSet).ShouldBeOfType<OrderOptionSet>();
            (optSet | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpf | optSet).ShouldBeOfType<OrderOptionSet>();
            (optSet | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpl | optSet).ShouldBeOfType<OrderOptionSet>();
        }

        [Fact]
        public void OrderOptionSetTypesAreCorrect()
        {
            int n = 2;
            First first = new First();
            Last last = new Last();
            BreakpointNumber bpn = new BreakpointNumber();
            BreakpointFirst bpf = new BreakpointFirst();
            BreakpointLast bpl = new BreakpointLast();

            (bpf | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpl).ShouldBeOfType<OrderOptionSet>();

            #region Generated combinations 
            (n | first | last | bpn | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | first | last | bpn | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | first | last | bpf | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | first | last | bpf | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | first | last | bpl | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | first | last | bpl | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpn | last | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpn | last | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpn | bpf | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpn | bpf | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpn | bpl | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpn | bpl | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpf | last | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpf | last | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpf | bpn | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpf | bpn | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpf | bpl | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpf | bpl | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpl | last | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpl | last | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpl | bpn | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpl | bpn | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpl | bpf | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | first | bpl | bpf | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (n | last | first | bpn | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | last | first | bpn | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | last | first | bpf | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | last | first | bpf | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | last | first | bpl | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | last | first | bpl | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpn | first | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpn | first | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpn | bpf | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpn | bpf | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpn | bpl | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpn | bpl | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpf | first | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpf | first | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpf | bpn | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpf | bpn | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpf | bpl | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpf | bpl | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpl | first | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpl | first | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpl | bpn | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpl | bpn | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpl | bpf | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | last | bpl | bpf | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | first | last | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | first | last | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | first | bpf | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | first | bpf | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | first | bpl | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | first | bpl | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | last | first | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | last | first | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | last | bpf | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | last | bpf | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | last | bpl | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | last | bpl | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpf | first | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpf | first | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpf | last | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpf | last | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpf | bpl | first | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpf | bpl | last | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpl | first | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpl | first | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpl | last | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpl | last | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpl | bpf | first | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpn | bpl | bpf | last | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | first | last | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | first | last | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | first | bpn | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | first | bpn | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | first | bpl | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | first | bpl | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | last | first | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | last | first | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | last | bpn | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | last | bpn | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | last | bpl | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | last | bpl | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpn | first | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpn | first | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpn | last | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpn | last | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpn | bpl | first | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpn | bpl | last | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpl | first | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpl | first | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpl | last | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpl | last | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpl | bpn | first | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpf | bpl | bpn | last | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | first | last | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | first | last | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | first | bpn | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | first | bpn | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | first | bpf | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | first | bpf | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | last | first | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | last | first | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | last | bpn | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | last | bpn | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | last | bpf | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | last | bpf | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpn | first | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpn | first | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpn | last | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpn | last | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpn | bpf | first | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpn | bpf | last | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpf | first | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpf | first | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpf | last | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpf | last | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpf | bpn | first | last).ShouldBeOfType<OrderOptionSet>();
            (n | bpl | bpf | bpn | last | first).ShouldBeOfType<OrderOptionSet>();
            (first | n | last | bpn | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | n | last | bpn | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | n | last | bpf | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | n | last | bpf | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | n | last | bpl | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | n | last | bpl | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpn | last | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpn | last | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpn | bpf | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpn | bpf | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpn | bpl | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpn | bpl | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpf | last | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpf | last | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpf | bpn | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpf | bpn | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpf | bpl | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpf | bpl | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpl | last | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpl | last | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpl | bpn | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpl | bpn | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpl | bpf | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | n | bpl | bpf | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (first | last | n | bpn | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | last | n | bpn | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | last | n | bpf | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | last | n | bpf | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | last | n | bpl | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | last | n | bpl | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpn | n | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpn | n | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpn | bpf | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpn | bpf | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpn | bpl | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpn | bpl | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpf | n | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpf | n | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpf | bpn | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpf | bpn | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpf | bpl | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpf | bpl | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpl | n | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpl | n | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpl | bpn | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpl | bpn | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpl | bpf | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | last | bpl | bpf | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | n | last | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | n | last | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | n | bpf | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | n | bpf | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | n | bpl | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | n | bpl | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | last | n | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | last | n | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | last | bpf | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | last | bpf | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | last | bpl | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | last | bpl | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpf | n | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpf | n | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpf | last | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpf | last | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpf | bpl | n | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpf | bpl | last | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpl | n | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpl | n | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpl | last | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpl | last | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpl | bpf | n | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpn | bpl | bpf | last | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | n | last | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | n | last | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | n | bpn | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | n | bpn | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | n | bpl | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | n | bpl | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | last | n | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | last | n | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | last | bpn | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | last | bpn | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | last | bpl | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | last | bpl | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpn | n | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpn | n | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpn | last | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpn | last | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpn | bpl | n | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpn | bpl | last | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpl | n | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpl | n | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpl | last | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpl | last | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpl | bpn | n | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpf | bpl | bpn | last | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | n | last | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | n | last | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | n | bpn | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | n | bpn | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | n | bpf | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | n | bpf | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | last | n | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | last | n | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | last | bpn | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | last | bpn | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | last | bpf | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | last | bpf | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpn | n | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpn | n | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpn | last | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpn | last | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpn | bpf | n | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpn | bpf | last | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpf | n | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpf | n | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpf | last | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpf | last | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpf | bpn | n | last).ShouldBeOfType<OrderOptionSet>();
            (first | bpl | bpf | bpn | last | n).ShouldBeOfType<OrderOptionSet>();
            (last | n | first | bpn | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | n | first | bpn | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | n | first | bpf | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | n | first | bpf | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | n | first | bpl | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | n | first | bpl | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpn | first | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpn | first | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpn | bpf | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpn | bpf | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpn | bpl | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpn | bpl | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpf | first | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpf | first | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpf | bpn | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpf | bpn | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpf | bpl | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpf | bpl | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpl | first | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpl | first | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpl | bpn | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpl | bpn | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpl | bpf | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | n | bpl | bpf | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (last | first | n | bpn | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | first | n | bpn | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | first | n | bpf | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | first | n | bpf | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | first | n | bpl | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | first | n | bpl | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpn | n | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpn | n | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpn | bpf | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpn | bpf | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpn | bpl | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpn | bpl | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpf | n | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpf | n | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpf | bpn | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpf | bpn | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpf | bpl | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpf | bpl | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpl | n | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpl | n | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpl | bpn | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpl | bpn | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpl | bpf | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | first | bpl | bpf | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | n | first | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | n | first | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | n | bpf | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | n | bpf | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | n | bpl | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | n | bpl | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | first | n | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | first | n | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | first | bpf | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | first | bpf | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | first | bpl | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | first | bpl | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpf | n | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpf | n | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpf | first | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpf | first | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpf | bpl | n | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpf | bpl | first | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpl | n | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpl | n | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpl | first | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpl | first | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpl | bpf | n | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpn | bpl | bpf | first | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | n | first | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | n | first | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | n | bpn | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | n | bpn | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | n | bpl | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | n | bpl | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | first | n | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | first | n | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | first | bpn | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | first | bpn | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | first | bpl | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | first | bpl | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpn | n | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpn | n | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpn | first | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpn | first | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpn | bpl | n | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpn | bpl | first | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpl | n | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpl | n | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpl | first | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpl | first | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpl | bpn | n | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpf | bpl | bpn | first | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | n | first | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | n | first | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | n | bpn | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | n | bpn | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | n | bpf | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | n | bpf | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | first | n | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | first | n | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | first | bpn | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | first | bpn | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | first | bpf | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | first | bpf | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpn | n | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpn | n | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpn | first | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpn | first | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpn | bpf | n | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpn | bpf | first | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpf | n | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpf | n | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpf | first | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpf | first | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpf | bpn | n | first).ShouldBeOfType<OrderOptionSet>();
            (last | bpl | bpf | bpn | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | first | last | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | first | last | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | first | bpf | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | first | bpf | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | first | bpl | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | first | bpl | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | last | first | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | last | first | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | last | bpf | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | last | bpf | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | last | bpl | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | last | bpl | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpf | first | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpf | first | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpf | last | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpf | last | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpf | bpl | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpf | bpl | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpl | first | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpl | first | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpl | last | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpl | last | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpl | bpf | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | n | bpl | bpf | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | n | last | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | n | last | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | n | bpf | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | n | bpf | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | n | bpl | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | n | bpl | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | last | n | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | last | n | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | last | bpf | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | last | bpf | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | last | bpl | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | last | bpl | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpf | n | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpf | n | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpf | last | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpf | last | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpf | bpl | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpf | bpl | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpl | n | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpl | n | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpl | last | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpl | last | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpl | bpf | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | first | bpl | bpf | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | n | first | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | n | first | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | n | bpf | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | n | bpf | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | n | bpl | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | n | bpl | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | first | n | bpf | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | first | n | bpl | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | first | bpf | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | first | bpf | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | first | bpl | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | first | bpl | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpf | n | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpf | n | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpf | first | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpf | first | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpf | bpl | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpf | bpl | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpl | n | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpl | n | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpl | first | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpl | first | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpl | bpf | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | last | bpl | bpf | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | n | first | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | n | first | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | n | last | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | n | last | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | n | bpl | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | n | bpl | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | first | n | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | first | n | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | first | last | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | first | last | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | first | bpl | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | first | bpl | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | last | n | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | last | n | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | last | first | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | last | first | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | last | bpl | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | last | bpl | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | bpl | n | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | bpl | n | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | bpl | first | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | bpl | first | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | bpl | last | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpf | bpl | last | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | n | first | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | n | first | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | n | last | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | n | last | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | n | bpf | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | n | bpf | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | first | n | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | first | n | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | first | last | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | first | last | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | first | bpf | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | first | bpf | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | last | n | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | last | n | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | last | first | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | last | first | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | last | bpf | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | last | bpf | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | bpf | n | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | bpf | n | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | bpf | first | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | bpf | first | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | bpf | last | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpn | bpl | bpf | last | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | first | last | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | first | last | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | first | bpn | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | first | bpn | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | first | bpl | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | first | bpl | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | last | first | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | last | first | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | last | bpn | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | last | bpn | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | last | bpl | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | last | bpl | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpn | first | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpn | first | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpn | last | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpn | last | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpn | bpl | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpn | bpl | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpl | first | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpl | first | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpl | last | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpl | last | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpl | bpn | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | n | bpl | bpn | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | n | last | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | n | last | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | n | bpn | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | n | bpn | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | n | bpl | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | n | bpl | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | last | n | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | last | n | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | last | bpn | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | last | bpn | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | last | bpl | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | last | bpl | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpn | n | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpn | n | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpn | last | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpn | last | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpn | bpl | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpn | bpl | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpl | n | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpl | n | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpl | last | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpl | last | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpl | bpn | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | first | bpl | bpn | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | n | first | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | n | first | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | n | bpn | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | n | bpn | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | n | bpl | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | n | bpl | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | first | n | bpn | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | first | n | bpl | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | first | bpn | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | first | bpn | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | first | bpl | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | first | bpl | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpn | n | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpn | n | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpn | first | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpn | first | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpn | bpl | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpn | bpl | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpl | n | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpl | n | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpl | first | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpl | first | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpl | bpn | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | last | bpl | bpn | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | n | first | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | n | first | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | n | last | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | n | last | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | n | bpl | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | n | bpl | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | first | n | last | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | first | n | bpl | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | first | last | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | first | last | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | first | bpl | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | first | bpl | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | last | n | first | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | last | n | bpl | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | last | first | n | bpl).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | last | first | bpl | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | last | bpl | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | last | bpl | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | bpl | n | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | bpl | n | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | bpl | first | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | bpl | first | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | bpl | last | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpn | bpl | last | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | n | first | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | n | first | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | n | last | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | n | last | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | n | bpn | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | n | bpn | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | first | n | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | first | n | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | first | last | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | first | last | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | first | bpn | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | first | bpn | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | last | n | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | last | n | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | last | first | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | last | first | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | last | bpn | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | last | bpn | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | bpn | n | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | bpn | n | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | bpn | first | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | bpn | first | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | bpn | last | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpf | bpl | bpn | last | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | first | last | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | first | last | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | first | bpn | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | first | bpn | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | first | bpf | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | first | bpf | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | last | first | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | last | first | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | last | bpn | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | last | bpn | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | last | bpf | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | last | bpf | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpn | first | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpn | first | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpn | last | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpn | last | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpn | bpf | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpn | bpf | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpf | first | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpf | first | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpf | last | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpf | last | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpf | bpn | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | n | bpf | bpn | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | n | last | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | n | last | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | n | bpn | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | n | bpn | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | n | bpf | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | n | bpf | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | last | n | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | last | n | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | last | bpn | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | last | bpn | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | last | bpf | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | last | bpf | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpn | n | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpn | n | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpn | last | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpn | last | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpn | bpf | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpn | bpf | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpf | n | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpf | n | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpf | last | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpf | last | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpf | bpn | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | first | bpf | bpn | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | n | first | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | n | first | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | n | bpn | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | n | bpn | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | n | bpf | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | n | bpf | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | first | n | bpn | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | first | n | bpf | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | first | bpn | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | first | bpn | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | first | bpf | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | first | bpf | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpn | n | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpn | n | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpn | first | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpn | first | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpn | bpf | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpn | bpf | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpf | n | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpf | n | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpf | first | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpf | first | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpf | bpn | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | last | bpf | bpn | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | n | first | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | n | first | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | n | last | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | n | last | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | n | bpf | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | n | bpf | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | first | n | last | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | first | n | bpf | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | first | last | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | first | last | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | first | bpf | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | first | bpf | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | last | n | first | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | last | n | bpf | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | last | first | n | bpf).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | last | first | bpf | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | last | bpf | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | last | bpf | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | bpf | n | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | bpf | n | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | bpf | first | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | bpf | first | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | bpf | last | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpn | bpf | last | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | n | first | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | n | first | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | n | last | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | n | last | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | n | bpn | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | n | bpn | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | first | n | last | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | first | n | bpn | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | first | last | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | first | last | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | first | bpn | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | first | bpn | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | last | n | first | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | last | n | bpn | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | last | first | n | bpn).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | last | first | bpn | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | last | bpn | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | last | bpn | first | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | bpn | n | first | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | bpn | n | last | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | bpn | first | n | last).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | bpn | first | last | n).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | bpn | last | n | first).ShouldBeOfType<OrderOptionSet>();
            (bpl | bpf | bpn | last | first | n).ShouldBeOfType<OrderOptionSet>();
            #endregion
        }

        [Fact]
        public void NotPossibleCombinationsAsync()
        {
            ShouldNotCompile<OrderOptionSet, Auto>();
            ShouldNotCompile<OrderOptionSet, Breakpoint>();
            ShouldNotCompile<OrderOptionSet, BreakpointAuto>();
            ShouldNotCompile<First, Auto>();
            ShouldNotCompile<Last, Auto>();
            ShouldNotCompile<BreakpointFirst, Auto>();
            ShouldNotCompile<BreakpointLast, Auto>();
            ShouldNotCompile<First, Breakpoint>();
            ShouldNotCompile<Last, Breakpoint>();
            ShouldNotCompile<BreakpointFirst, Breakpoint>();
            ShouldNotCompile<BreakpointLast, Breakpoint>();
            ShouldNotCompile<First, BreakpointAuto>();
            ShouldNotCompile<Last, BreakpointAuto>();
            ShouldNotCompile<BreakpointFirst, BreakpointAuto>();
            ShouldNotCompile<BreakpointLast, BreakpointAuto>();

            ShouldNotCompile<SpanOptionSet, First>();
            ShouldNotCompile<SpanOptionSet, Last>();
            ShouldNotCompile<SpanOptionSet, BreakpointFirst>();
            ShouldNotCompile<SpanOptionSet, BreakpointLast>();
            ShouldNotCompile<Auto, First>();
            ShouldNotCompile<Breakpoint, First>();
            ShouldNotCompile<BreakpointAuto, First>();
            ShouldNotCompile<Auto, Last>();
            ShouldNotCompile<Breakpoint, Last>();
            ShouldNotCompile<BreakpointAuto, Last>();
            ShouldNotCompile<Auto, BreakpointFirst>();
            ShouldNotCompile<Breakpoint, BreakpointFirst>();
            ShouldNotCompile<BreakpointAuto, BreakpointFirst>();
            ShouldNotCompile<Auto, BreakpointLast>();
            ShouldNotCompile<Breakpoint, BreakpointLast>();
            ShouldNotCompile<BreakpointAuto, BreakpointLast>();
        }

        private static void ShouldNotCompile<TOption1, TOption2>()
        {
            var combinationExpression = "(option1, option2) => { var dummy = option1 | option2; }";
            var options = ScriptOptions.Default.AddReferences(typeof(OptCombiTests).Assembly);
            var actual = Should.Throw<CompilationErrorException>(() =>
            {
                return CSharpScript.EvaluateAsync<Action<TOption1, TOption2>>(combinationExpression, options);
            });
            actual.Message.ShouldContain("error CS0019: Operator '|' cannot be applied to operands of type");
        }
    }
}

