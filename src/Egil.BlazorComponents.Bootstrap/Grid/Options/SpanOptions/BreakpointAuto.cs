using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointAuto : SpanOption
    {
        private Breakpoint breakpoint;
        private Auto option;

        public BreakpointAuto(Breakpoint breakpoint, Auto option)
        {
            this.breakpoint = breakpoint;
            this.option = option;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, option.Value);
    }

}