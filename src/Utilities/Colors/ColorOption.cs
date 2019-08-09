using Egil.RazorComponents.Bootstrap.Options;

namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
    public class ColorOption : Option, IOption
    {
        public override string Value { get; }

        public ColorOption(string color)
        {
            Value = color;
        }
    }
}