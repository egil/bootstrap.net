using Egil.RazorComponents.Bootstrap.Components.Carousels.Parameters;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public static class Factory
    {
        public static readonly AnimationParameter None = AnimationParameter.None;
        public static readonly AnimationParameter Slide = AnimationParameter.Slide;
        public static readonly AnimationParameter Fade = AnimationParameter.Fade;

        public static class LowerCase
        {
            public static readonly AnimationParameter none = None;
            public static readonly AnimationParameter slide = Slide;
            public static readonly AnimationParameter fade = Fade;
        }
    }
}