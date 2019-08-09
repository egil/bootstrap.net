using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Html.Parameters
{
    public class ButtonColor : ICssClassPrefix
    {
        private const string ButtonPrefix = "btn";
        private const string ButtonOutlinedPrefix = "btn-outline";

        public bool Outlined { get; set; } = false;

        public string Prefix => Outlined ? ButtonOutlinedPrefix : ButtonPrefix;
    }
}
