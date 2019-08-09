using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Groups.Parameters
{
    public class GroupSize : ICssClassPrefix
    {
        private const string BtnPrefix = "btn-group";
        public string Prefix { get; } = BtnPrefix;
    }
}
