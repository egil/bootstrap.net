using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options
{
    public class OptionSetTests : OptionFixture
    {
        private class OptionValueCopy : IOption
        {
            public string Value { get; }

            public OptionValueCopy(IOption sourceOption)
            {
                Value = sourceOption.Value;
            }
        }

        [Theory(DisplayName = "Duplicated options are discarded when added to a option set based on their value")]
        [MemberData(nameof(AllOptionsFixtureData))]
        public void OptionSetDoesNotAllowDuplicatedOptions(IOption option)
        {
            var set = new OptionSet<IOption>();
            var optionCopy = new OptionValueCopy(option);

            set.Add(option);
            set.Add(optionCopy);

            option.Value.ShouldBe(optionCopy.Value);
            set.Count.ShouldBe(1);
            set.ShouldContain(option);
            set.ShouldNotContain(optionCopy);
        }
    }
}
