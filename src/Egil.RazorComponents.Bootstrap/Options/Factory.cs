using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Parameters;

namespace Egil.RazorComponents.Bootstrap.Options
{
    public static class Factory
    {
        public static readonly ContainerTypeParameter Fluid = ContainerTypeParameter.Fluid;

        public static readonly DefaultBreakpoint ExtraSmall = new DefaultBreakpoint();
        public static readonly Breakpoint Small = new Breakpoint(BreakpointType.Small);
        public static readonly Breakpoint Medium = new Breakpoint(BreakpointType.Medium);
        public static readonly Breakpoint Large = new Breakpoint(BreakpointType.Large);
        public static readonly Breakpoint ExtraLarge = new Breakpoint(BreakpointType.ExtraLarge);

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

            public static readonly DefaultBreakpoint extraSmall = ExtraSmall;
            public static readonly Breakpoint small = Small;
            public static readonly Breakpoint medium = Medium;
            public static readonly Breakpoint large = Large;
            public static readonly Breakpoint extraLarge = ExtraLarge;

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

                public static readonly DefaultBreakpoint xs = ExtraSmall;
                public static readonly Breakpoint sm = Small;
                public static readonly Breakpoint md = Medium;
                public static readonly Breakpoint lg = Large;
                public static readonly Breakpoint xl = ExtraLarge;

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
}

