using System;
using System.Collections.Generic;
using System.Linq;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public abstract class OptionFixture<TSutOption> where TSutOption : class, IOption
    {
        public static readonly IReadOnlyList<IOption> AllOptions = new List<IOption>
        {
            center, sm-start, // alignment
            sm, auto, sm-auto, // span
            first, last, lg-first, md-last, // order
            (GridNumber)12, sm-1, // grid breakpoint 
        };

        public static readonly IReadOnlyList<TSutOption> SutOptions = AllOptions.OfType<TSutOption>().ToList();

        public static IEnumerable<object[]> SutOptionsFixtureData => SutOptions.ToFixtureData();

        public static IEnumerable<object[]> SutOptionsPairsFixtureData
        {
            get
            {
                var pairs = SutOptions.AllPairs().ToArray();
                var reversePairs = pairs.ReversePairs().Where(x => x.first != x.second);
                return pairs.Concat(reversePairs).ToFixtureData(x => new object[] { x.Item1, x.Item2 });
            }
        }

        public static IEnumerable<object[]> SutOptionsPairedWithIncompatibleOptionsFixtureData
        {
            get
            {
                var incompatibleOptions = AllOptions.Except(SutOptions);
                var pairs = SutOptions.AllPairsWith<IOption, IOption>(incompatibleOptions);
                var reversePairs = pairs.ReversePairs();
                return pairs.Concat(reversePairs).ToFixtureData(x => new[] { x.Item1, x.Item2 });
            }
        }
    }
}
