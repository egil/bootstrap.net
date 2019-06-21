using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Html.Parameters
{
    public class ButtonAsLink : CssClassProviderBase
    {
        private const string Value = "btn-link";
        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return Value;
        }
    }

    public class ButtonOutlined : CssClassProviderBase
    {
        private const string Value = "btn-link";
        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return Value;
        }
    }
}
