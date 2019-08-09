using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    internal sealed class EmptyCssClassProvider : CssClassProviderBase
    {
        public override int Count { get; } = 0;

        public override IEnumerator<string> GetEnumerator()
        {
            yield break;
        }
    }
}
