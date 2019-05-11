using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions
{
    public static class Factory
    {
        public static readonly SpacingSide Top = new SpacingSide(SpacingSideType.Top);
        public static readonly SpacingSide Bottom = new SpacingSide(SpacingSideType.Bottom);
        public static readonly SpacingSide Left = new SpacingSide(SpacingSideType.Left);
        public static readonly SpacingSide Right = new SpacingSide(SpacingSideType.Right);
        public static readonly SpacingSide Horizontal = new SpacingSide(SpacingSideType.Horizontal);
        public static readonly SpacingSide Vertical = new SpacingSide(SpacingSideType.Vertical);

        public static class LowerCase
        {
            public static readonly SpacingSide top = Top;
            public static readonly SpacingSide bottom = Bottom;
            public static readonly SpacingSide left = Left;
            public static readonly SpacingSide right = Right;
            public static readonly SpacingSide horizontal = Horizontal;
            public static readonly SpacingSide vertical = Vertical;

            public class Abbr
            {
                public static readonly SpacingSide t = Top;
                public static readonly SpacingSide b = Bottom;
                public static readonly SpacingSide l = Left;
                public static readonly SpacingSide r = Right;
                public static readonly SpacingSide x = Horizontal;
                public static readonly SpacingSide y = Vertical;
            }
        }
    }
}
