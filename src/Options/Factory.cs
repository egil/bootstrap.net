using Egil.RazorComponents.Bootstrap.Components.Layout.Parameters;
using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Options.SimpleOptions;

namespace Egil.RazorComponents.Bootstrap.Options
{
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1034 // Nested types should not be visible
    public static class Factory
    {
        public static readonly ContainerTypeParameter Fluid = ContainerTypeParameter.Fluid;

        public static readonly DefaultSizeOption ExtraSmall = DefaultSizeOption.Default;
        public static readonly SizeOption Small = SizeOption.Small;
        public static readonly SizeOption Medium = SizeOption.Medium;
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

        public static readonly LeftOption Left = new LeftOption();
        public static readonly RightOption Right = new RightOption();
        public static readonly UpOption Up = new UpOption();
        public static readonly DownOption Down = new DownOption();
        public static readonly TopOption Top = new TopOption();
        public static readonly BottomOption Bottom = new BottomOption();
        public static readonly HorizontalOption Horizontal = new HorizontalOption();
        public static readonly VerticalOption Vertical = new VerticalOption();

        public static class LowerCase
        {
            public static readonly ContainerTypeParameter fluid = Fluid;

            public static readonly DefaultSizeOption extraSmall = ExtraSmall;
            public static readonly SizeOption small = Small;
            public static readonly SizeOption medium = Medium;
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

            public static readonly LeftOption left = Left;
            public static readonly RightOption right = Right;
            public static readonly UpOption up = Up;
            public static readonly DownOption down = Down;
            public static readonly TopOption top = Top;
            public static readonly BottomOption bottom = Bottom;
            public static readonly HorizontalOption horizontal = Horizontal;
            public static readonly VerticalOption vertical = Vertical;

            public class Abbr
            {
                public static readonly ContainerTypeParameter fluid = Fluid;

                public static readonly DefaultSizeOption xs = ExtraSmall;
                public static readonly SizeOption sm = Small;
                public static readonly SizeOption md = Medium;
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

                public static readonly LeftOption left = Left;
                public static readonly RightOption right = Right;
                public static readonly UpOption up = Up;
                public static readonly DownOption down = Down;
                public static readonly TopOption top = Top;
                public static readonly BottomOption bottom = Bottom;
                public static readonly HorizontalOption horizontal = Horizontal;
                public static readonly VerticalOption vertical = Vertical;
            }
        }
    }
#pragma warning restore IDE1006 // Naming Styles
}

