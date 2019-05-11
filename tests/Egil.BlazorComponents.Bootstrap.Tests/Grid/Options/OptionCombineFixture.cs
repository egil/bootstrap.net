using System.Collections.Generic;
using System.Linq;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions.Factory.LowerCase;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public abstract class OptionFixture
    {
        public static readonly IReadOnlyList<IOption> AllOptions = new List<IOption>
        {
            center, sm-start, // alignment
            between, md-between, // justify
            sm, auto, sm-auto, // span
            first, last, lg-first, md-last, // order
            sm-1, // common TODO NUMBER
            left-(-4), left-lg-3 // spacing
        };

        public static IEnumerable<object[]> AllOptionsFixtureData => AllOptions.ToFixtureData();
    }

    public abstract class OptionCombineFixture<TSutOption> : OptionFixture
        where TSutOption : class, IOption
    {
        public static readonly IReadOnlyList<TSutOption> SutOptions = AllOptions.OfType<TSutOption>().ToList();

        public static IEnumerable<object[]> SutOptionsFixtureData => SutOptions.ToFixtureData();

        public static IEnumerable<object[]> SutOptionsPairsFixtureData
        {
            get
            {
                var pairs = SutOptions.AllPairs().ToArray();
                var reversePairs = pairs.ReversePairs();
                // when a reversed pair contain the same two options, the pair is already in 'pairs' array
                //.Where(x => x.first != x.second); 
                return pairs.Concat(reversePairs).ToFixtureData(x => new object[] { x.Item1, x.Item2 });
            }
        }

        public static IEnumerable<object[]> IncompatibleOptionsFixtureData => AllOptions.Except(SutOptions).ToFixtureData();

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

        [Theory(DisplayName = "Options should be combineable with other options of same type")]
        [MemberData(nameof(SutOptionsPairsFixtureData))]
        public virtual void OptionsCombineable(TSutOption first, TSutOption second)
        {
            first.CombinedWith(second).ShouldResultInSetOf<TSutOption>().ThatContains(first, second);
        }

        [Theory(DisplayName = "Options should NOT be combineable with options of other types")]
        [MemberData(nameof(SutOptionsPairedWithIncompatibleOptionsFixtureData))]
        public void OptionsNotCombineableWithOtherOptions(IOption first, IOption second)
        {
            first.CombinedWith(second).ShouldNotResultInSetOf<TSutOption>();
        }

        [Theory(DisplayName = "Options should be combineable with OptionSet containing the option type")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void OptionShouldBeCombineableWithOptionSet(TSutOption sutOption)
        {
            IOptionSet<TSutOption> set = new OptionSet<TSutOption>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<TSutOption>()
                .ThatContains(sutOption);
        }

        [Theory(DisplayName = "Incompatible options should NOT be combineable with OptionSet containing the option type")]
        [MemberData(nameof(IncompatibleOptionsFixtureData))]
        public void IncompatibleOptionsNotCombineableWithOptionSet(IOption sutOption)
        {
            IOptionSet<TSutOption> set = new OptionSet<TSutOption>();

            set.CombinedWith(sutOption).ShouldNotResultInSetOf<TSutOption>();
        }
    }

    public abstract class NumberOptionCombineFixture<TSutOption> : OptionCombineFixture<TSutOption>
        where TSutOption : class, IOption
    {
        [Theory(DisplayName = "Options should be combineable with number")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void OptionShouldBeCombineableWithNumber(TSutOption sutOption)
        {
            int num = 5;

            num.CombinedWith(sutOption)
                .ShouldResultInSetOf<TSutOption>()
                .ThatContains(sutOption, (Number)5);

            sutOption.CombinedWith(num)
                .ShouldResultInSetOf<TSutOption>()
                .ThatContains((Number)5, sutOption);
        }
    }

    public abstract class GridOptionCombineFixture<TSutOption> : NumberOptionCombineFixture<TSutOption>
        where TSutOption : class, IOption
    {
        [Theory(DisplayName = "Options should be combineable with OptionSet of IBreakpointWithNumber types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void OptionShouldBeCombineableWithIGridBreakpointOptionSet(TSutOption sutOption)
        {
            IOptionSet<IBreakpointWithNumber> set = new OptionSet<IBreakpointWithNumber>();

            set.CombinedWith(sutOption)
                .ShouldResultInSetOf<TSutOption>()
                .ThatContains(sutOption);
        }
    }
}
