using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    public class CssClassValueProvider : CssClassProviderBase
    {
        public CssClassValueProvider(string? value = null)
        {
            Value = value ?? string.Empty;
            Count = value is null ? 0 : 1;
        }

        public string Value { get; }

        public sealed override int Count { get; }

        public override IEnumerator<string> GetEnumerator()
        {
            if (Count == 1) yield return Value;
            yield break;
        }
    }
}
