namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class NumberOption : Option
    {
        private readonly int number;

        public NumberOption(int number)
        {
            this.number = number;
        }

        public override string CssClass => number.ToString();
    }
}
