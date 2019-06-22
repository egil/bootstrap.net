using Egil.RazorComponents.Bootstrap.Components.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;

namespace Egil.RazorComponents.Bootstrap.Options
{
#pragma warning disable IDE1006 // Naming Styles
    public static class Factory
    {
        public static readonly ContainerTypeParameter Fluid = ContainerTypeParameter.Fluid;

        public static readonly DefaultSizeOption ExtraSmall = DefaultSizeOption.Default;
        public static readonly SizeOption Small = SizeOption.Small;
        public static readonly ExtendedSizeOption Medium = ExtendedSizeOption.Medium;
        public static readonly SizeOption Large = SizeOption.Large;
        public static readonly ExtendedSizeOption ExtraLarge = ExtendedSizeOption.ExtraLarge;

        public static readonly FirstOption First = new FirstOption();
        public static readonly LastOption Last = new LastOption();
        public static readonly AutoOption Auto = new AutoOption();

        public static readonly AlignmentOption Start = new AlignmentOption(AlignmentType.Start);
        public static readonly AlignmentOption Center = new AlignmentOption(AlignmentType.Center);
        public static readonly AlignmentOption End = new AlignmentOption(AlignmentType.End);
        public static readonly JustifyOption Between = new JustifyOption(JustifyType.Between);
        public static readonly JustifyOption Around = new JustifyOption(JustifyType.Around);

        public static class LowerCase
        {
            public static readonly ContainerTypeParameter fluid = Fluid;

            public static readonly DefaultSizeOption extraSmall = ExtraSmall;
            public static readonly SizeOption small = Small;
            public static readonly ExtendedSizeOption medium = Medium;
            public static readonly SizeOption large = Large;
            public static readonly ExtendedSizeOption extraLarge = ExtraLarge;

            public static readonly FirstOption first = First;
            public static readonly LastOption last = Last;
            public static readonly AutoOption auto = Auto;

            public static readonly AlignmentOption start = Start;
            public static readonly AlignmentOption center = Center;
            public static readonly AlignmentOption end = End;
            public static readonly JustifyOption between = Between;
            public static readonly JustifyOption around = Around;

            public class Abbr
            {
                public static readonly ContainerTypeParameter fluid = Fluid;

                public static readonly DefaultSizeOption xs = ExtraSmall;
                public static readonly SizeOption sm = Small;
                public static readonly ExtendedSizeOption md = Medium;
                public static readonly SizeOption lg = Large;
                public static readonly ExtendedSizeOption xl = ExtraLarge;

                public static readonly FirstOption first = First;
                public static readonly LastOption last = Last;
                public static readonly AutoOption auto = Auto;

                public static readonly AlignmentOption start = Start;
                public static readonly AlignmentOption center = Center;
                public static readonly AlignmentOption end = End;
                public static readonly JustifyOption between = Between;
                public static readonly JustifyOption around = Around;
            }
        }
    }
#pragma warning restore IDE1006 // Naming Styles
}

