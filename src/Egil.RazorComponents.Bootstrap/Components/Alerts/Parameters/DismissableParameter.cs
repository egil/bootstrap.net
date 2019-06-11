using Egil.RazorComponents.Bootstrap.Parameters;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters
{
    public class DismissableParameter : CssClassParameterBase
    {
        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return "alert-dismissible";
        }

        public static implicit operator DismissableParameter(bool isDismissable)
        {
            return isDismissable ? Dismissable : Default;
        }

        public static explicit operator bool(DismissableParameter dismissable)
        {
            return dismissable.Count > 0;
        }

        public static DismissableParameter Dismissable = new DismissableParameter();
        public static DismissableParameter Default = new NoneParameter();

        private sealed class NoneParameter : DismissableParameter
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
