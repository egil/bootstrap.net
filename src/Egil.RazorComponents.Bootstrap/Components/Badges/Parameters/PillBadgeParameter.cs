using Egil.RazorComponents.Bootstrap.Parameters;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Components.Badges.Parameters
{
    public class PillBadgeParameter : CssClassProviderBase
    {
        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return "badge-pill";
        }

        public static implicit operator PillBadgeParameter(bool isPillShaped)
        {
            return isPillShaped ? PillShaped : None;
        }

        public static explicit operator bool(PillBadgeParameter pillParam)
        {
            return pillParam.Count > 0;
        }

        public static PillBadgeParameter PillShaped = new PillBadgeParameter();
        public static PillBadgeParameter None = new NoneParameter();

        private sealed class NoneParameter : PillBadgeParameter
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
