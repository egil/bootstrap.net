using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public sealed class FadeOutAnimation : CssClassAnimationBase
    {
        public override int Count => Ready ? 2 : 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return "fade";
            if (Ready) yield return "show";
        }
    }
}
