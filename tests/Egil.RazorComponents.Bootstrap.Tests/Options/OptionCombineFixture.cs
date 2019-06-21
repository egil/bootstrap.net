using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Options.Factory.LowerCase.Abbr;
using static Egil.RazorComponents.Bootstrap.Utilities.Spacing.Factory.LowerCase;
using static Egil.RazorComponents.Bootstrap.Utilities.Colors.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Tests.Options
{
    public abstract class OptionFixture
    {
        public static readonly IReadOnlyList<IOption> AllOptions = new List<IOption>
        {
            center, sm-start, md-end, // alignment
            between, sm-around, md-between, // justify
            xs, sm, md, lg, xl, auto, sm-auto, md-auto, // span
            first, last, md-first, lg-first, md-last, lg-last,  // order
            sm-1, md-2, // common TODO NUMBER
            left-(-4), right-md-2, left-lg-3, // spacing
            primary
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
