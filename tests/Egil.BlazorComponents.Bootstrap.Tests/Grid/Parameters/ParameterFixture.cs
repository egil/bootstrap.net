using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using System.Collections.Generic;
using System.Linq;

using static Egil.BlazorComponents.Bootstrap.Grid.Options.Factory.LowerCase.Abbr;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions.Factory.LowerCase;

namespace Egil.BlazorComponents.Bootstrap.Grid
{
    public abstract class ParameterFixture
    {
        public static readonly IReadOnlyList<IOptionSet<IOption>> AllOptionSets = new List<IOptionSet<IOption>>
        {
            new OptionSet<IOption>(),
            new OptionSet<IOrderOption>() { first, last },
            new OptionSet<ISpanOption>() { auto },
            new OptionSet<IAlignmentOption>() { start, center } ,
            new OptionSet<IJustifyOption>() { between, around } ,
            new OptionSet<IOffsetOption>() { (Number)1, md-3 },
            new OptionSet<IBreakpointWithNumber>(){ (Number)1, md-3 },
            new OptionSet<ISpacingOption>(){ (Number)1, right-2, left-md-3 },
        };
    }

    public abstract class ParameterFixture<TSutOption> : ParameterFixture
        where TSutOption : class, IOption
    {
        public static readonly IReadOnlyList<IOptionSet<TSutOption>> SutOptionSets = AllOptionSets.OfType<IOptionSet<TSutOption>>().ToList();

        public static IEnumerable<object[]> SutOptionSetsFixtureData => SutOptionSets.ToFixtureData();

        public static IEnumerable<object[]> IncompatibleOptionSetsFixtureData => AllOptionSets.Except(SutOptionSets).ToFixtureData();
    }

    public abstract class ParameterFixture<TSutOption1, TSutOption2> : ParameterFixture
        where TSutOption1 : class, IOption
        where TSutOption2 : class, IOption
    {
        public static readonly IReadOnlyList<IOptionSet<IOption>> SutOptionSets =
            Enumerable.Concat<IOptionSet<IOption>>(
                AllOptionSets.OfType<IOptionSet<TSutOption1>>(),
                AllOptionSets.OfType<IOptionSet<TSutOption2>>()
            ).ToList();

        public static IEnumerable<object[]> SutOptionSetsFixtureData => SutOptionSets.ToFixtureData();

        public static IEnumerable<object[]> IncompatibleOptionSetsFixtureData => AllOptionSets.Except(SutOptionSets).ToFixtureData();
    }
}