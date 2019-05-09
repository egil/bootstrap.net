using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using System.Collections.Generic;
using System.Linq;

using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Grid
{
    public abstract class ParameterFixture<TSutOption>
        where TSutOption : class, IOption
    {
        public static readonly IReadOnlyList<IOptionSet<IOption>> AllOptionSets = new List<IOptionSet<IOption>>
        {
            new OptionSet<IOption>(),
            new OptionSet<IOrderOption>() { first, last },
            new OptionSet<ISpanOption>() { auto },
            new OptionSet<IAlignmentOption>() { start, center } ,
            //new OptionSet<IJustifyOption>() { between, around } ,
            new OptionSet<IOffsetOption>() { (GridNumber)1, md-3 },
            new OptionSet<IGridBreakpoint>(){ (GridNumber)1, md-3 },
        };

        public static readonly IReadOnlyList<IOptionSet<TSutOption>> SutOptionSets = AllOptionSets.OfType<IOptionSet<TSutOption>>().ToList();

        public static IEnumerable<object[]> SutOptionSetsFixtureData => SutOptionSets.ToFixtureData();

        public static IEnumerable<object[]> IncompatibleOptionSetsFixtureData => AllOptionSets.Except(SutOptionSets).ToFixtureData();
    }
}
