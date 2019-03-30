namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointNumber : SharedOption
    {
        private Breakpoint breakpoint;
        private Number<ISharedOption> width;

        public BreakpointNumber(Breakpoint breakpoint, Number<ISharedOption> width)
        {
            this.breakpoint = breakpoint;
            this.width = width;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, width.Value);
    }

}