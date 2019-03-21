namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class NoneOrderOption : OrderOption
    {
        public override string CssClass => string.Empty;
        public static readonly OrderOption Instance = new NoneOrderOption();
    }

}