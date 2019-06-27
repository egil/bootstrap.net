namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
#pragma warning disable IDE1006 // Naming Styles
    public static class Factory
    {
        public static readonly ColorOption Primary = new ColorOption(Colors.Primary);
        public static readonly ColorOption Secondary = new ColorOption(Colors.Secondary);
        public static readonly ColorOption Success = new ColorOption(Colors.Success);
        public static readonly ColorOption Danger = new ColorOption(Colors.Danger);
        public static readonly ColorOption Warning = new ColorOption(Colors.Warning);
        public static readonly ColorOption Info = new ColorOption(Colors.Info);
        public static readonly ColorOption Light = new ColorOption(Colors.Light);
        public static readonly ColorOption Dark = new ColorOption(Colors.Dark);
        public static readonly ColorOption Body = new ColorOption(Colors.Body);
        public static readonly ColorOption Muted = new ColorOption(Colors.Muted);
        public static readonly ColorOption White = new ColorOption(Colors.White);
        public static readonly ColorOption Black50 = new ColorOption(Colors.Black50);
        public static readonly ColorOption White50 = new ColorOption(Colors.White50);

        public static class LowerCase
        {

            public static readonly ColorOption primary = Primary;
            public static readonly ColorOption secondary = Secondary;
            public static readonly ColorOption success = Success;
            public static readonly ColorOption danger = Danger;
            public static readonly ColorOption warning = Warning;
            public static readonly ColorOption info = Info;
            public static readonly ColorOption light = Light;
            public static readonly ColorOption dark = Dark;
            public static readonly ColorOption body = Body;
            public static readonly ColorOption muted = Muted;
            public static readonly ColorOption white = White;
            public static readonly ColorOption black50 = Black50;
            public static readonly ColorOption white50 = White50;
        }
    }
#pragma warning restore IDE1006 // Naming Styles
}
