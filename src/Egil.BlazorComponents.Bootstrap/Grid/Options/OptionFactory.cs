namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public static class OptionFactory
    {
        public static readonly Breakpoint Small = new Breakpoint(BreakpointType.Small);
        public static readonly Breakpoint Medium = new Breakpoint(BreakpointType.Medium);
        public static readonly Breakpoint Large = new Breakpoint(BreakpointType.Large);
        public static readonly Breakpoint ExtraLarge = new Breakpoint(BreakpointType.ExtraLarge);

        public static readonly FirstOption First = new FirstOption();
        public static readonly LastOption Last = new LastOption();
        public static readonly AutoOption Auto = new AutoOption();

        public static class LowerCase
        {
            public static readonly Breakpoint small = Small;
            public static readonly Breakpoint medium = Medium;
            public static readonly Breakpoint large = Large;
            public static readonly Breakpoint extraLarge = ExtraLarge;

            public static readonly FirstOption first = First;
            public static readonly LastOption last = Last;
            public static readonly AutoOption auto = Auto;

            public class Abbr
            {
                public static readonly Breakpoint sm = Small;
                public static readonly Breakpoint md = Medium;
                public static readonly Breakpoint lg = Large;
                public static readonly Breakpoint xl = ExtraLarge;

                public static readonly FirstOption first = First;
                public static readonly LastOption last = Last;
                public static readonly AutoOption auto = Auto;
            }
        }

        public static class UpperCase
        {
            public static readonly Breakpoint SMALL = Small;
            public static readonly Breakpoint MEDIUM = Medium;
            public static readonly Breakpoint LARGE = Large;
            public static readonly Breakpoint EXTRALARGE = ExtraLarge;

            public static readonly FirstOption FIRST = First;
            public static readonly LastOption LAST = Last;
            public static readonly AutoOption AUTO = Auto;

            public static class Abbr
            {
                public static readonly Breakpoint SM = Small;
                public static readonly Breakpoint MD = Medium;
                public static readonly Breakpoint LG = Large;
                public static readonly Breakpoint XL = ExtraLarge;

                public static readonly FirstOption FIRST = First;
                public static readonly LastOption LAST = Last;
                public static readonly AutoOption AUTO = Auto;
            }
        }
    }
}

