using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public class CssClassValueProvider : CssClassProviderBase
    {
        public CssClassValueProvider(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return Value;
        }

        public static readonly CssClassValueProvider None = new NoneParameter();

        class NoneParameter : CssClassValueProvider
        {
            public NoneParameter() : base(string.Empty) { }

            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
