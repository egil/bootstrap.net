using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointFirst : OrderOption
    {
        private Breakpoint breakpoint;
        private First option;

        public BreakpointFirst(Breakpoint breakpoint, First option)
        {
            this.breakpoint = breakpoint;
            this.option = option;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, option.Value);
    }

}