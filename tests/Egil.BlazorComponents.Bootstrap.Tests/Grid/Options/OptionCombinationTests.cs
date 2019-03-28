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
using Egil.BlazorComponents.Bootstrap.Grid.Options;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    /// <summary>
    /// Tests all legal and illigal combinations of options using the | operator.
    /// 
    /// The following is legal for each type of option set:
    /// 
    /// SharedOptionSet:         Number, BreakpointNumber.
    /// OptionSet<ISpanOption> : Number, Auto, Breakpoint, BreakpointNumber, BreakpointAuto.
    /// OptionSet<IOrderOption>: Number, First, Last, BreakpointNumber, BreakpointFirst, BreakpointLast.
    /// </summary>
    public class OptionCombinationTests
    {
        private readonly ITestOutputHelper output;

        public OptionCombinationTests(ITestOutputHelper output)
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

        [Fact(Skip = "generator", DisplayName = "Generator for all combinations of SharedOptions")]
        public void OptionSetPermutationsCreator()
        {
            var options = new[] { "n", "bpn", "bpn", };
            var permutations = Get(options).Select(x => string.Join(" | ", x));
            foreach (var item in permutations)
            {
                output.WriteLine($"({item}).ShouldBeOfType<OptionSet>();");
            }
        }


        [Fact(Skip = "generator", DisplayName = "Generator for all combinations of SpanOptions")]
        public void SpanOptionSetPermutationsCreator()
        {
            var options = new[] { "n", "auto", "bp", "bpn", "bpa", };
            var permutations = Get(options).Select(x => string.Join(" | ", x));
            foreach (var item in permutations)
            {
                output.WriteLine($"({item}).ShouldBeOfType<OptionSet<ISpanOption>>();");
            }
        }

        [Fact(Skip = "generator", DisplayName = "Generator for all combinations of OrderOptions")]
        public void OrderOptionSetPermutationsCreator()
        {
            // number, first, last, breakpointWithNumber, breakpointFirst, breakpointLast
            var options = new[] { "n", "first", "last", "bpn", "bpf", "bpl" };
            var permutations = Get(options).Select(x => string.Join(" | ", x));
            foreach (var item in permutations)
            {
                output.WriteLine($"({item}).ShouldBeOfType<OptionSet<IOrderOption>>();");
            }
        }
        #endregion

        [Fact(DisplayName = "When SharedOption's are combined the result is a SharedOptionSet")]
        public void SharedOptionsCombinedResultsInSharedOptionsSet()
        {
            int n = 2;
            BreakpointNumber bpn = new BreakpointNumber();

            (n | bpn | bpn).ShouldBeOfType<SharedOptionsSet>();
            (n | bpn | bpn).ShouldBeOfType<SharedOptionsSet>();
            (bpn | n | bpn).ShouldBeOfType<SharedOptionsSet>();
            (bpn | bpn | n).ShouldBeOfType<SharedOptionsSet>();
            (bpn | n | bpn).ShouldBeOfType<SharedOptionsSet>();
            (bpn | bpn | n).ShouldBeOfType<SharedOptionsSet>();
        }

        [Fact(DisplayName = "When SpanOption's are combined with a SharedOptionSet the result is a OptionSet<ISpanOption>")]
        public void SpanOptionSetCombinedWithOptionSet()
        {
            SharedOptionsSet optSet = new SharedOptionsSet();
            Auto auto = new Auto();
            Breakpoint bp = new Breakpoint();
            BreakpointAuto bpa = new BreakpointAuto();

            (optSet | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | optSet).ShouldBeOfType<OptionSet<ISpanOption>>();
            (optSet | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | optSet).ShouldBeOfType<OptionSet<ISpanOption>>();
            (optSet | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | optSet).ShouldBeOfType<OptionSet<ISpanOption>>();
        }

        [Fact(DisplayName = "SpanOption's can be combined in all possible ways")]
        public void SpanOptionCanBeCombined()
        {
            int n = 2;
            Auto auto = new Auto();
            Breakpoint bp = new Breakpoint();
            BreakpointNumber bpn = new BreakpointNumber();
            BreakpointAuto bpa = new BreakpointAuto();

            (bp | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();

            #region Generated combinations 
            (n | auto | bp | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | auto | bp | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | auto | bpn | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | auto | bpn | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | auto | bpa | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | auto | bpa | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bp | auto | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bp | auto | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bp | bpn | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bp | bpn | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bp | bpa | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bp | bpa | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpn | auto | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpn | auto | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpn | bp | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpn | bp | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpn | bpa | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpn | bpa | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpa | auto | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpa | auto | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpa | bp | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpa | bp | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpa | bpn | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (n | bpa | bpn | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | n | bp | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | n | bp | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | n | bpn | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | n | bpn | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | n | bpa | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | n | bpa | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | n | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | n | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpn | n | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpn | bpa | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpa | n | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpa | bpn | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | n | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | n | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bp | n | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bp | bpa | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bpa | n | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bpa | bp | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | n | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | n | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bp | n | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bp | bpn | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bpn | n | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bpn | bp | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | n | auto | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | n | auto | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | n | bpn | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | n | bpn | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | n | bpa | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | n | bpa | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | n | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | n | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpn | n | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpn | bpa | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpa | n | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpa | bpn | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | n | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | n | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | auto | n | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | auto | bpa | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | bpa | n | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | bpa | auto | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | n | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | n | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | auto | n | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | auto | bpn | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | bpn | n | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | bpn | auto | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | n | auto | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | n | auto | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | n | bp | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | n | bp | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | n | bpa | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | n | bpa | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | n | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | n | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bp | n | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bp | bpa | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bpa | n | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bpa | bp | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | n | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | n | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | auto | n | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | auto | bpa | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | bpa | n | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | bpa | auto | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | n | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | n | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | auto | n | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | auto | bp | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | bp | n | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | bp | auto | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | n | auto | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | n | auto | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | n | bp | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | n | bp | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | n | bpn | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | n | bpn | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | n | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | n | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bp | n | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bp | bpn | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bpn | n | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bpn | bp | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | n | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | n | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | auto | n | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | auto | bpn | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | bpn | n | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | bpn | auto | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | n | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | n | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | auto | n | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | auto | bp | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | bp | n | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | bp | auto | n).ShouldBeOfType<OptionSet<ISpanOption>>();
            #endregion
        }

        [Fact(DisplayName = "When OrderOption's are combined with a SharedOptionSet the result is a OptionSet<IOrderOption>")]
        public void OrderOptionSetCombinedWithOptionSet()
        {
            SharedOptionsSet optSet = new SharedOptionsSet();
            First first = new First();
            Last last = new Last();
            BreakpointFirst bpf = new BreakpointFirst();
            BreakpointLast bpl = new BreakpointLast();

            (optSet | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | optSet).ShouldBeOfType<OptionSet<IOrderOption>>();
            (optSet | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | optSet).ShouldBeOfType<OptionSet<IOrderOption>>();
            (optSet | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | optSet).ShouldBeOfType<OptionSet<IOrderOption>>();
            (optSet | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | optSet).ShouldBeOfType<OptionSet<IOrderOption>>();
        }

        [Fact(DisplayName = "OrderOption's can be combined in all possible ways")]
        public void OrderOptionsCanBeCombined()
        {
            int n = 2;
            First first = new First();
            Last last = new Last();
            BreakpointNumber bpn = new BreakpointNumber();
            BreakpointFirst bpf = new BreakpointFirst();
            BreakpointLast bpl = new BreakpointLast();

            (bpf | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();

            #region Generated combinations 
            (n | first | last | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | last | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | last | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | last | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | last | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | last | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpn | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpn | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpn | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpn | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpn | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpn | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpf | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpf | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpf | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpf | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpf | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpf | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpl | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpl | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpl | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpl | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpl | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | first | bpl | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | first | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | first | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | first | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | first | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | first | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | first | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpn | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpn | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpn | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpn | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpn | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpn | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpf | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpf | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpf | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpf | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpf | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpf | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpl | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpl | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpl | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpl | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpl | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | last | bpl | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | first | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | first | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | first | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | first | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | first | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | first | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | last | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | last | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | last | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | last | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | last | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | last | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpf | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpf | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpf | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpf | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpf | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpf | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpl | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpl | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpl | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpl | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpl | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpn | bpl | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | first | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | first | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | first | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | first | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | first | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | first | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | last | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | last | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | last | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | last | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | last | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | last | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpn | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpn | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpn | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpn | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpn | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpn | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpl | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpl | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpl | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpl | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpl | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpf | bpl | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | first | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | first | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | first | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | first | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | first | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | first | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | last | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | last | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | last | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | last | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | last | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | last | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpn | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpn | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpn | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpn | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpn | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpn | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpf | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpf | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpf | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpf | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpf | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (n | bpl | bpf | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | last | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | last | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | last | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | last | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | last | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | last | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpn | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpn | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpn | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpn | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpn | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpn | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpf | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpf | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpf | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpf | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpf | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpf | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpl | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpl | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpl | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpl | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpl | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | n | bpl | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | n | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | n | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | n | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | n | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | n | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | n | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | n | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | n | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpf | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpf | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpl | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpl | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | n | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | n | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpn | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpn | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpl | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpl | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | n | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | n | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpn | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpn | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpf | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpf | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | n | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | n | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | n | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | n | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | n | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | n | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | n | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | n | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpf | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpf | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpl | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpl | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | n | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | n | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | last | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | last | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | bpl | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | bpl | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | n | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | n | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | last | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | last | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | bpf | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | bpf | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | n | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | n | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | n | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | n | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | n | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | n | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | n | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | n | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpn | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpn | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpl | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpl | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | n | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | n | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | last | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | last | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | bpl | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | bpl | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | n | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | n | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | last | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | last | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | bpn | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | bpn | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | n | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | n | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | n | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | n | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | n | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | n | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | n | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | n | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpn | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpn | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpf | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpf | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | n | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | n | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | last | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | last | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | bpf | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | bpf | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | n | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | n | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | last | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | last | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | bpn | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | bpn | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | first | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | first | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | first | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | first | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | first | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | first | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpn | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpn | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpn | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpn | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpn | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpn | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpf | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpf | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpf | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpf | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpf | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpf | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpl | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpl | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpl | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpl | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpl | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | n | bpl | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | n | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | n | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | n | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | n | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | n | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | n | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | n | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | n | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpf | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpf | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpl | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpl | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | n | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | n | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpn | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpn | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpl | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpl | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | n | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | n | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpn | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpn | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpf | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpf | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | n | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | n | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | n | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | n | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | n | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | n | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | n | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | n | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpf | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpf | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpl | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpl | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | n | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | n | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | first | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | first | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | bpl | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | bpl | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | n | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | n | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | first | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | first | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | bpf | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | bpf | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | n | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | n | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | n | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | n | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | n | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | n | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | n | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | n | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpn | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpn | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpl | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpl | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | n | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | n | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | first | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | first | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | bpl | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | bpl | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | n | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | n | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | first | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | first | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | bpn | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | bpn | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | n | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | n | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | n | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | n | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | n | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | n | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | n | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | n | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpn | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpn | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpf | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpf | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | n | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | n | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | first | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | first | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | bpf | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | bpf | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | n | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | n | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | first | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | first | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | bpn | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | bpn | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | first | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | first | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | first | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | first | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | first | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | first | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | last | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | last | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | last | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | last | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | last | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | last | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpf | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpf | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpf | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpf | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpf | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpf | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpl | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpl | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpl | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpl | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpl | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | n | bpl | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | n | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | n | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | n | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | n | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | n | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | n | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | n | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | n | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpf | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpf | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpl | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpl | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | n | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | n | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | last | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | last | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | bpl | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | bpl | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | n | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | n | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | last | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | last | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | bpf | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | bpf | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | n | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | n | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | n | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | n | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | n | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | n | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | n | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | n | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpf | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpf | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpl | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpl | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | n | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | n | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | first | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | first | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | bpl | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | bpl | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | n | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | n | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | first | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | first | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | bpf | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | bpf | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | n | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | n | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | n | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | n | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | n | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | n | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | n | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | n | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | last | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | last | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | bpl | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | bpl | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | n | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | n | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | first | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | first | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | bpl | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | bpl | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | n | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | n | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | first | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | first | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | last | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | last | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | n | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | n | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | n | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | n | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | n | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | n | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | n | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | n | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | last | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | last | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | bpf | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | bpf | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | n | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | n | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | first | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | first | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | bpf | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | bpf | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | n | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | n | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | first | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | first | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | last | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | last | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | first | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | first | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | first | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | first | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | first | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | first | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | last | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | last | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | last | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | last | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | last | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | last | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpn | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpn | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpn | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpn | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpn | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpn | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpl | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpl | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpl | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpl | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpl | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | n | bpl | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | n | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | n | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | n | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | n | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | n | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | n | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | n | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | n | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpn | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpn | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpl | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpl | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | n | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | n | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | last | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | last | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | bpl | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | bpl | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | n | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | n | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | last | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | last | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | bpn | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | bpn | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | n | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | n | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | n | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | n | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | n | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | n | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | n | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | n | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpn | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpn | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpl | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpl | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | n | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | n | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | first | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | first | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | bpl | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | bpl | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | n | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | n | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | first | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | first | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | bpn | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | bpn | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | n | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | n | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | n | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | n | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | n | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | n | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | n | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | n | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | last | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | last | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | bpl | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | bpl | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | n | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | n | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | first | n | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | first | bpl | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | bpl | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | bpl | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | n | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | n | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | first | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | first | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | last | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | last | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | n | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | n | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | n | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | n | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | n | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | n | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | n | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | n | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | last | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | last | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | bpn | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | bpn | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | n | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | n | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | first | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | first | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | bpn | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | bpn | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | n | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | n | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | first | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | first | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | last | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | last | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | first | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | first | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | first | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | first | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | first | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | first | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | last | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | last | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | last | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | last | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | last | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | last | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpn | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpn | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpn | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpn | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpn | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpn | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpf | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpf | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpf | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpf | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpf | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | n | bpf | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | n | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | n | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | n | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | n | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | n | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | n | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | n | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | n | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpn | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpn | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpf | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpf | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | n | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | n | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | last | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | last | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | bpf | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | bpf | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | n | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | n | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | last | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | last | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | bpn | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | bpn | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | n | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | n | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | n | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | n | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | n | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | n | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | n | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | n | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpn | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpn | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpf | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpf | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | n | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | n | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | first | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | first | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | bpf | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | bpf | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | n | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | n | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | first | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | first | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | bpn | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | bpn | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | n | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | n | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | n | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | n | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | n | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | n | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | n | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | n | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | last | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | last | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | bpf | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | bpf | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | n | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | n | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | first | n | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | first | bpf | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | bpf | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | bpf | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | n | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | n | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | first | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | first | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | last | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | last | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | n | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | n | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | n | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | n | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | n | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | n | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | n | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | n | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | last | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | last | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | bpn | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | bpn | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | n | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | n | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | first | n | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | first | bpn | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | bpn | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | bpn | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | n | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | n | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | first | n | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | first | last | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | last | n | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | last | first | n).ShouldBeOfType<OptionSet<IOrderOption>>();
            #endregion
        }

        [Fact(DisplayName = "OrderOptions and SpanOptions cannot be combined - fail to compile")]
        public void OrderOptionsAndSpanOptionsCannotCombine()
        {
            ShouldNotCompile<OptionSet<IOrderOption>, Auto>();
            ShouldNotCompile<OptionSet<IOrderOption>, Breakpoint>();
            ShouldNotCompile<OptionSet<IOrderOption>, BreakpointAuto>();
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

            ShouldNotCompile<OptionSet<ISpanOption>, First>();
            ShouldNotCompile<OptionSet<ISpanOption>, Last>();
            ShouldNotCompile<OptionSet<ISpanOption>, BreakpointFirst>();
            ShouldNotCompile<OptionSet<ISpanOption>, BreakpointLast>();
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

            void ShouldNotCompile<TOption1, TOption2>()
            {
                var combinationExpression = "(option1, option2) => { var dummy = option1 | option2; }";
                var options = ScriptOptions.Default.AddReferences(typeof(OptionCombinationTests).Assembly);
                var actual = Should.Throw<CompilationErrorException>(() =>
                {
                    return CSharpScript.EvaluateAsync<Action<TOption1, TOption2>>(combinationExpression, options);
                });
                actual.Message.ShouldContain("error CS0019: Operator '|' cannot be applied to operands of type");
            }
        }
    }
}

