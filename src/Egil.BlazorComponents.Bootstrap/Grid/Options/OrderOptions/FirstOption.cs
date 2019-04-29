namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class FirstOption : IOrderOption
    {
        private const string OptionText = "first";
        public string Value => OptionText;

        public static OptionSet<IOrderOption> operator |(FirstOption option1, IOrderOption option2)
        {
            return new OptionSet<IOrderOption>() { option1, option2 };
        }
    }
}