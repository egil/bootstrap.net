using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public class CssClassValueProvider : CssClassParameterBase
    {
        public CssClassValueProvider(string? value = null)
        {
            Value = value;
        }

        public string? Value { get; set; }

        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            if (!(Value is null)) yield return Value;
        }
        
        public static readonly CssClassValueProvider None = new NoneParameter();

        class NoneParameter : CssClassValueProvider
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
