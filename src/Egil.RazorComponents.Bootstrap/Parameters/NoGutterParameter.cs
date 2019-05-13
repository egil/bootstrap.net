using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public class NoGutterParameter : Parameter
    {
        private const string Value = "no-gutter";

        private NoGutterParameter() { }

        public override int Count => 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return Value;
        }

        public static implicit operator NoGutterParameter(bool hasNoGutter)
        {
            return hasNoGutter ? NoGutter : Default;
        }

        public static readonly NoGutterParameter Default = new NoneParameter();
        public static readonly NoGutterParameter NoGutter = new NoGutterParameter();

        class NoneParameter : NoGutterParameter
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator() { yield break; }
        }
    }
}
