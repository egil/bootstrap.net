using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public static class OptionFactory
    {
        public static readonly Breakpoint sm = new Breakpoint(BreakpointType.Small);
        public static readonly Breakpoint md = new Breakpoint(BreakpointType.Medium);
        public static readonly Breakpoint lg = new Breakpoint(BreakpointType.Large);
        public static readonly Breakpoint xl = new Breakpoint(BreakpointType.ExtraLarge);

        public static readonly First first = new First();
        public static readonly Last last = new Last();
        public static readonly Auto auto = new Auto();
    }
}

