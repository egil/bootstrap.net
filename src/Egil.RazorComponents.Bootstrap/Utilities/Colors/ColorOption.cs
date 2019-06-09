using Egil.RazorComponents.Bootstrap.Options;

namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
    public class ColorOption : IOption
    {
        public ColorOption(string color)
        {
            Value = color;
        }

        public string Value { get; }
    }
}