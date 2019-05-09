using Shouldly;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using System.Linq;

namespace Egil.BlazorComponents.Bootstrap.Tests.Utilities
{
    public static class ParameterFixtureExtenions
    {
        public static void ShouldContainOptionsWithPrefix(this Parameter parameter, string prefix, int number)
        {
            parameter.Single().ShouldBe($"{prefix}{Option.OptionSeparator}{number}");
            parameter.Count.ShouldBe(1);
        }

        public static void ShouldContainOptionsWithPrefix(this Parameter parameter, string prefix, IOption option)
        {
            parameter.Single().ShouldBe($"{prefix}{Option.OptionSeparator}{option.Value}");
            parameter.Count.ShouldBe(1);
        }

        public static void ShouldContainOptionsWithPrefix(this Parameter parameter, string prefix, IOptionSet<IOption> set)
        {
            var setWithPrefix = set.Select(option => $"{prefix}{Option.OptionSeparator}{option.Value}");
            parameter.ShouldBe(setWithPrefix);
            parameter.Count.ShouldBe(set.Count);
        }
    }
}
