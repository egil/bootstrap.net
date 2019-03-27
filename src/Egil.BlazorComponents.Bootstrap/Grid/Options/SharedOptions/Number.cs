namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class Number<T> : IOption<T>
    {
        private readonly int number;

        public Number(int number)
        {
            this.number = number;
        }

        public string Value => number.ToString();
    }

}