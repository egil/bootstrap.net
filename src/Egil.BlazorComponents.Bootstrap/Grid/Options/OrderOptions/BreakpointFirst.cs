namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointFirst : OrderOption
    {
        private Breakpoint breakpoint;
        private FirstOption option;

        public BreakpointFirst(Breakpoint breakpoint, FirstOption option)
        {
            this.breakpoint = breakpoint;
            this.option = option;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, option.Value);
    }

}