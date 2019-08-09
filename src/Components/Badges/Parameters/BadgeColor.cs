using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Badges.Parameters
{
    public class BadgeColor : ICssClassPrefix
    {
        private const string BadgePrefix = "badge";

        public string Prefix { get; } = BadgePrefix;
    }
}
