using Egil.BlazorComponents.Bootstrap.Grid.Options;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class FirstOption : Option
    {
        private const string OptionText = "first";

        public override string CssClass => OptionText;
    }

}