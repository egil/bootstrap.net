using Egil.RazorComponents.Bootstrap.Parameters;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Layout.Parameters
{
    public class NoGuttersParameter : CssClassProviderBase, ICssClassProvider
    {
        private const string Value = "no-gutter";

        private NoGuttersParameter() { }

        public override int Count => 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return Value;
        }

        public static implicit operator NoGuttersParameter(bool hasNoGutter)
        {
            return hasNoGutter ? NoGutter : Default;
        }

        public static readonly NoGuttersParameter Default = new NoneParameter();
        public static readonly NoGuttersParameter NoGutter = new NoGuttersParameter();

        class NoneParameter : NoGuttersParameter
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator() { yield break; }
        }
    }
}
