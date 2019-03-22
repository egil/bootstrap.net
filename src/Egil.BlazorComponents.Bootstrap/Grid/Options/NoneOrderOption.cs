namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class NoneOrderOption : OrderParameter
    {
        public override string CssClass => string.Empty;
        public static readonly OrderParameter Instance = new NoneOrderOption();
    }

}