using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;


namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.AlignmentOptions
{
    public class AlignmentOptionCombineTests : OptionCombineFixture<IAlignmentOption>
    {
        [Fact]
        public void MyTestMethod()
        {
            var x = around | between;
        }
    }
}
