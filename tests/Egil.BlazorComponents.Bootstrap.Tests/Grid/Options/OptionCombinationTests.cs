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
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

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
        private int num = 2;
        private BreakpointNumber bpn = md - 4;
        private SharedOptionsSet optSet = new SharedOptionsSet();
        private Breakpoint bp = md;
        private BreakpointAuto bpa = md - auto;
        private BreakpointFirst bpf = xl - first;
        private BreakpointLast bpl = sm - last;

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
            (num | bpn | bpn).ShouldBeOfType<SharedOptionsSet>();
            (num | bpn | bpn).ShouldBeOfType<SharedOptionsSet>();
            (bpn | num | bpn).ShouldBeOfType<SharedOptionsSet>();
            (bpn | bpn | num).ShouldBeOfType<SharedOptionsSet>();
            (bpn | num | bpn).ShouldBeOfType<SharedOptionsSet>();
            (bpn | bpn | num).ShouldBeOfType<SharedOptionsSet>();
        }

        [Fact(DisplayName = "When SpanOption's are combined with a SharedOptionSet the result is a OptionSet<ISpanOption>")]
        public void SpanOptionSetCombinedWithOptionSet()
        {
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
            (bp | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();

            #region Generated combinations 
            (num | auto | bp | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | auto | bp | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | auto | bpn | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | auto | bpn | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | auto | bpa | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | auto | bpa | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bp | auto | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bp | auto | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bp | bpn | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bp | bpn | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bp | bpa | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bp | bpa | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpn | auto | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpn | auto | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpn | bp | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpn | bp | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpn | bpa | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpn | bpa | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpa | auto | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpa | auto | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpa | bp | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpa | bp | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpa | bpn | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (num | bpa | bpn | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | num | bp | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | num | bp | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | num | bpn | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | num | bpn | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | num | bpa | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | num | bpa | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | num | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | num | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpn | num | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpn | bpa | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpa | num | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bp | bpa | bpn | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | num | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | num | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bp | num | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bp | bpa | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bpa | num | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpn | bpa | bp | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | num | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | num | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bp | num | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bp | bpn | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bpn | num | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (auto | bpa | bpn | bp | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | num | auto | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | num | auto | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | num | bpn | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | num | bpn | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | num | bpa | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | num | bpa | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | num | bpn | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | num | bpa | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpn | num | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpn | bpa | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpa | num | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | auto | bpa | bpn | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | num | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | num | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | auto | num | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | auto | bpa | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | bpa | num | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpn | bpa | auto | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | num | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | num | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | auto | num | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | auto | bpn | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | bpn | num | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bp | bpa | bpn | auto | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | num | auto | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | num | auto | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | num | bp | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | num | bp | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | num | bpa | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | num | bpa | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | num | bp | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | num | bpa | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bp | num | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bp | bpa | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bpa | num | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | auto | bpa | bp | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | num | auto | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | num | bpa | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | auto | num | bpa).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | auto | bpa | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | bpa | num | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bp | bpa | auto | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | num | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | num | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | auto | num | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | auto | bp | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | bp | num | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpn | bpa | bp | auto | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | num | auto | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | num | auto | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | num | bp | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | num | bp | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | num | bpn | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | num | bpn | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | num | bp | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | num | bpn | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bp | num | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bp | bpn | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bpn | num | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | auto | bpn | bp | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | num | auto | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | num | bpn | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | auto | num | bpn).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | auto | bpn | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | bpn | num | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bp | bpn | auto | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | num | auto | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | num | bp | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | auto | num | bp).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | auto | bp | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | bp | num | auto).ShouldBeOfType<OptionSet<ISpanOption>>();
            (bpa | bpn | bp | auto | num).ShouldBeOfType<OptionSet<ISpanOption>>();
            #endregion
        }

        [Fact(DisplayName = "When OrderOption's are combined with a SharedOptionSet the result is a OptionSet<IOrderOption>")]
        public void OrderOptionSetCombinedWithOptionSet()
        {
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
            (bpf | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();

            #region Generated combinations 
            (num | first | last | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | last | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | last | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | last | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | last | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | last | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpn | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpn | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpn | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpn | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpn | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpn | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpf | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpf | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpf | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpf | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpf | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpf | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpl | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpl | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpl | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpl | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpl | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | first | bpl | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | first | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | first | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | first | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | first | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | first | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | first | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpn | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpn | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpn | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpn | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpn | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpn | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpf | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpf | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpf | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpf | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpf | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpf | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpl | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpl | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpl | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpl | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpl | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | last | bpl | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | first | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | first | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | first | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | first | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | first | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | first | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | last | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | last | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | last | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | last | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | last | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | last | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpf | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpf | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpf | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpf | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpf | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpf | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpl | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpl | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpl | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpl | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpl | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpn | bpl | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | first | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | first | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | first | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | first | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | first | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | first | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | last | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | last | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | last | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | last | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | last | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | last | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpn | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpn | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpn | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpn | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpn | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpn | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpl | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpl | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpl | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpl | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpl | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpf | bpl | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | first | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | first | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | first | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | first | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | first | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | first | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | last | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | last | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | last | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | last | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | last | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | last | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpn | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpn | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpn | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpn | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpn | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpn | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpf | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpf | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpf | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpf | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpf | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (num | bpl | bpf | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | last | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | last | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | last | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | last | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | last | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | last | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpn | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpn | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpn | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpn | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpn | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpn | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpf | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpf | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpf | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpf | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpf | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpf | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpl | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpl | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpl | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpl | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpl | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | num | bpl | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | num | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | num | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | num | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | num | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | num | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | num | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | num | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | num | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpf | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpf | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpl | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpn | bpl | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | num | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | num | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpn | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpn | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpl | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpf | bpl | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | num | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | num | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpn | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpn | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpf | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | last | bpl | bpf | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | num | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | num | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | num | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | num | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | num | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | num | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | num | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | num | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpf | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpf | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpl | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | last | bpl | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | num | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | num | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | last | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | last | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | bpl | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpf | bpl | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | num | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | num | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | last | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | last | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | bpf | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpn | bpl | bpf | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | num | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | num | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | num | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | num | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | num | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | num | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | num | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | num | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpn | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpn | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpl | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | last | bpl | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | num | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | num | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | last | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | last | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | bpl | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpn | bpl | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | num | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | num | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | last | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | last | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | bpn | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpf | bpl | bpn | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | num | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | num | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | num | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | num | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | num | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | num | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | num | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | num | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpn | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpn | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpf | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | last | bpf | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | num | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | num | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | last | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | last | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | bpf | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpn | bpf | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | num | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | num | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | last | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | last | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | bpn | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (first | bpl | bpf | bpn | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | first | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | first | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | first | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | first | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | first | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | first | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpn | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpn | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpn | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpn | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpn | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpn | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpf | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpf | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpf | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpf | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpf | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpf | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpl | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpl | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpl | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpl | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpl | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | num | bpl | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | num | bpn | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | num | bpn | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | num | bpf | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | num | bpf | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | num | bpl | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | num | bpl | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | num | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | num | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpf | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpf | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpl | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpn | bpl | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | num | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | num | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpn | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpn | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpl | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpf | bpl | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | num | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | num | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpn | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpn | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpf | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | first | bpl | bpf | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | num | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | num | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | num | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | num | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | num | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | num | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | num | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | num | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpf | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpf | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpl | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | first | bpl | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | num | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | num | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | first | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | first | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | bpl | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpf | bpl | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | num | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | num | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | first | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | first | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | bpf | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpn | bpl | bpf | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | num | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | num | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | num | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | num | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | num | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | num | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | num | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | num | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpn | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpn | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpl | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | first | bpl | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | num | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | num | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | first | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | first | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | bpl | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpn | bpl | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | num | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | num | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | first | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | first | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | bpn | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpf | bpl | bpn | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | num | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | num | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | num | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | num | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | num | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | num | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | num | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | num | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpn | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpn | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpf | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | first | bpf | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | num | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | num | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | first | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | first | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | bpf | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpn | bpf | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | num | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | num | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | first | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | first | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | bpn | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (last | bpl | bpf | bpn | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | first | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | first | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | first | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | first | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | first | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | first | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | last | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | last | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | last | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | last | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | last | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | last | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpf | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpf | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpf | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpf | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpf | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpf | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpl | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpl | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpl | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpl | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpl | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | num | bpl | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | num | last | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | num | last | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | num | bpf | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | num | bpf | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | num | bpl | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | num | bpl | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | num | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | num | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpf | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpf | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpl | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | last | bpl | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | num | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | num | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | last | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | last | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | bpl | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpf | bpl | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | num | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | num | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | last | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | last | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | bpf | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | first | bpl | bpf | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | num | first | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | num | first | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | num | bpf | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | num | bpf | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | num | bpl | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | num | bpl | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | num | bpf | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | num | bpl | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpf | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpf | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpl | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | first | bpl | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | num | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | num | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | first | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | first | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | bpl | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpf | bpl | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | num | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | num | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | first | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | first | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | bpf | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | last | bpl | bpf | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | num | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | num | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | num | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | num | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | num | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | num | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | num | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | num | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | last | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | last | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | bpl | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | first | bpl | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | num | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | num | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | first | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | first | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | bpl | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | last | bpl | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | num | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | num | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | first | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | first | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | last | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpf | bpl | last | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | num | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | num | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | num | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | num | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | num | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | num | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | num | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | num | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | last | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | last | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | bpf | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | first | bpf | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | num | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | num | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | first | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | first | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | bpf | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | last | bpf | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | num | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | num | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | first | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | first | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | last | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpn | bpl | bpf | last | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | first | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | first | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | first | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | first | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | first | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | first | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | last | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | last | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | last | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | last | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | last | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | last | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpn | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpn | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpn | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpn | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpn | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpn | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpl | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpl | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpl | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpl | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpl | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | num | bpl | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | num | last | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | num | last | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | num | bpn | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | num | bpn | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | num | bpl | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | num | bpl | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | num | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | num | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpn | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpn | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpl | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | last | bpl | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | num | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | num | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | last | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | last | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | bpl | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpn | bpl | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | num | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | num | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | last | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | last | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | bpn | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | first | bpl | bpn | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | num | first | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | num | first | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | num | bpn | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | num | bpn | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | num | bpl | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | num | bpl | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | num | bpn | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | num | bpl | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpn | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpn | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpl | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | first | bpl | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | num | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | num | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | first | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | first | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | bpl | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpn | bpl | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | num | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | num | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | first | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | first | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | bpn | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | last | bpl | bpn | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | num | first | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | num | first | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | num | last | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | num | last | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | num | bpl | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | num | bpl | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | num | last | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | num | bpl | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | last | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | last | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | bpl | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | first | bpl | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | num | first | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | num | bpl | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | first | num | bpl).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | first | bpl | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | bpl | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | last | bpl | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | num | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | num | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | first | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | first | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | last | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpn | bpl | last | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | num | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | num | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | num | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | num | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | num | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | num | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | num | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | num | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | last | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | last | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | bpn | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | first | bpn | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | num | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | num | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | first | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | first | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | bpn | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | last | bpn | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | num | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | num | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | first | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | first | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | last | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpf | bpl | bpn | last | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | first | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | first | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | first | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | first | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | first | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | first | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | last | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | last | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | last | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | last | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | last | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | last | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpn | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpn | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpn | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpn | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpn | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpn | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpf | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpf | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpf | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpf | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpf | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | num | bpf | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | num | last | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | num | last | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | num | bpn | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | num | bpn | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | num | bpf | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | num | bpf | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | num | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | num | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpn | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpn | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpf | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | last | bpf | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | num | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | num | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | last | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | last | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | bpf | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpn | bpf | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | num | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | num | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | last | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | last | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | bpn | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | first | bpf | bpn | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | num | first | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | num | first | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | num | bpn | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | num | bpn | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | num | bpf | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | num | bpf | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | num | bpn | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | num | bpf | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpn | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpn | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpf | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | first | bpf | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | num | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | num | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | first | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | first | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | bpf | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpn | bpf | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | num | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | num | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | first | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | first | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | bpn | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | last | bpf | bpn | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | num | first | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | num | first | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | num | last | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | num | last | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | num | bpf | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | num | bpf | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | num | last | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | num | bpf | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | last | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | last | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | bpf | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | first | bpf | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | num | first | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | num | bpf | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | first | num | bpf).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | first | bpf | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | bpf | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | last | bpf | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | num | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | num | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | first | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | first | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | last | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpn | bpf | last | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | num | first | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | num | first | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | num | last | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | num | last | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | num | bpn | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | num | bpn | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | num | last | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | num | bpn | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | last | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | last | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | bpn | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | first | bpn | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | num | first | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | num | bpn | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | first | num | bpn).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | first | bpn | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | bpn | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | last | bpn | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | num | first | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | num | last | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | first | num | last).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | first | last | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | last | num | first).ShouldBeOfType<OptionSet<IOrderOption>>();
            (bpl | bpf | bpn | last | first | num).ShouldBeOfType<OptionSet<IOrderOption>>();
            #endregion
        }

        [Fact(DisplayName = "OrderOptions and SpanOptions cannot be combined - fail to compile")]
        public void OrderOptionsAndSpanOptionsCannotCombine()
        {
            ShouldNotCompile<OptionSet<IOrderOption>, AutoOption>();
            ShouldNotCompile<OptionSet<IOrderOption>, Breakpoint>();
            ShouldNotCompile<OptionSet<IOrderOption>, BreakpointAuto>();
            ShouldNotCompile<FirstOption, AutoOption>();
            ShouldNotCompile<LastOption, AutoOption>();
            ShouldNotCompile<BreakpointFirst, AutoOption>();
            ShouldNotCompile<BreakpointLast, AutoOption>();
            ShouldNotCompile<FirstOption, Breakpoint>();
            ShouldNotCompile<LastOption, Breakpoint>();
            ShouldNotCompile<BreakpointFirst, Breakpoint>();
            ShouldNotCompile<BreakpointLast, Breakpoint>();
            ShouldNotCompile<FirstOption, BreakpointAuto>();
            ShouldNotCompile<LastOption, BreakpointAuto>();
            ShouldNotCompile<BreakpointFirst, BreakpointAuto>();
            ShouldNotCompile<BreakpointLast, BreakpointAuto>();

            ShouldNotCompile<OptionSet<ISpanOption>, FirstOption>();
            ShouldNotCompile<OptionSet<ISpanOption>, LastOption>();
            ShouldNotCompile<OptionSet<ISpanOption>, BreakpointFirst>();
            ShouldNotCompile<OptionSet<ISpanOption>, BreakpointLast>();
            ShouldNotCompile<AutoOption, FirstOption>();
            ShouldNotCompile<Breakpoint, FirstOption>();
            ShouldNotCompile<BreakpointAuto, FirstOption>();
            ShouldNotCompile<AutoOption, LastOption>();
            ShouldNotCompile<Breakpoint, LastOption>();
            ShouldNotCompile<BreakpointAuto, LastOption>();
            ShouldNotCompile<AutoOption, BreakpointFirst>();
            ShouldNotCompile<Breakpoint, BreakpointFirst>();
            ShouldNotCompile<BreakpointAuto, BreakpointFirst>();
            ShouldNotCompile<AutoOption, BreakpointLast>();
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

