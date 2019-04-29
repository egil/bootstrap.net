namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class LastOption : IOrderOption
    {
        private const string OptionText = "last";
        public string Value => OptionText;

        public static OptionSet<IOrderOption> operator |(LastOption option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }
    }

}