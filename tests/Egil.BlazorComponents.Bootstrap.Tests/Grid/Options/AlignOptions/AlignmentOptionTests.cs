using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.CodeAnalysis.Operations;
using Xunit;
using Xunit.Abstractions;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.AlignOptions
{
    public class AlignmentOptionTests : OptionFixture<IAlignmentOption>
    {
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

        [Fact(DisplayName = "Alignment option returns correct css class")]
        public void AlignOptionReturnsCorrectCssClass()
        {
            new AlignmentOption(AlignmentType.Start).Value.ShouldBe("start");
            new AlignmentOption(AlignmentType.End).Value.ShouldBe("end");
            new AlignmentOption(AlignmentType.Center).Value.ShouldBe("center");
            new AlignmentOption(AlignmentType.Stretch).Value.ShouldBe("stretch");
        }

        [Fact(DisplayName = "Breakpoint can be combined with alignment option using - operator")]
        public void BreakpointWithÁlignOption()
        {
            var bp = new Breakpoint(BreakpointType.Large);
            var align = new AlignmentOption(AlignmentType.Center);
            var bpa = bp - align;
            bpa.Value.ShouldBe($"{bp.Value}-{align.Value}");
        }

        [Theory(DisplayName = "Alignment options should be combineable with other alignment options")]
        [MemberData(nameof(SutOptionsPairsFixtureData))]
        public void AlignmentOptionsCombineable(IAlignmentOption first, IAlignmentOption second)
        {
            var set = first.CombinedWith(second).ShouldBeAssignableTo<IOptionSet<IAlignmentOption>>();
            set.ShouldContain(first);
            set.ShouldContain(second);
        }

        [Theory(DisplayName = "Alignment options should NOT be combineable with non-alignment options")]
        [MemberData(nameof(SutOptionsPairedWithIncompatibleOptionsFixtureData))]
        public void AlignmentOptionsNotCombineableWithOtherOptions(IOption first, IOption second)
        {
            first.ShouldNotBeCombineableWith(second);
        }

        [Theory(DisplayName = "Alignment options should be combineable with OptionSet of IAlignmentOption types")]
        [MemberData(nameof(SutOptionsFixtureData))]
        public void AlignmentOptionShouldBeCombineableWithOptionSet(IAlignmentOption sutOption)
        {
            IOptionSet<IAlignmentOption> set = new OptionSet2<IAlignmentOption>();
            set.CombinedWith(sutOption)
                .ShouldBeAssignableTo<IOptionSet<IAlignmentOption>>()
                .ShouldContain(sutOption);
        }
    }
}