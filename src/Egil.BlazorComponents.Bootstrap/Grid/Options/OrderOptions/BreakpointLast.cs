namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{

    public class BreakpointLast : OrderOption
    {
        private Breakpoint breakpoint;
        private LastOption option;

        public BreakpointLast(Breakpoint breakpoint, LastOption option)
        {
            this.breakpoint = breakpoint;
            this.option = option;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, option.Value);
    }

}