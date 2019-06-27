using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters
{
    public class AlertColor : ICssClassPrefix
    {
        private const string AlertPrefix = "alert";

        public string Prefix { get; } = AlertPrefix;
    }
}
