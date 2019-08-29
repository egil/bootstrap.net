using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels.Parameters
{
    #pragma warning disable CA1710 // Identifiers should have correct suffix
    public class AnimationParameter : CssClassProviderBase
    {
        private readonly string? _cssClassValue;

        public override int Count { get; }

        private AnimationParameter(string? cssClassValue = null)
        {
            _cssClassValue = cssClassValue;
            Count = cssClassValue == null ? 0 : 1;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            if(!(_cssClassValue is null))
                yield return _cssClassValue;
        }

        public static readonly AnimationParameter None = new AnimationParameter();
        public static readonly AnimationParameter Slide = new AnimationParameter("slide");
        public static readonly AnimationParameter Fade = new AnimationParameter("slide carousel-fade");
    }
}
