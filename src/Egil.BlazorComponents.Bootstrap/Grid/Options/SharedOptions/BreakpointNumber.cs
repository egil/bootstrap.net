namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointNumber : SharedOption
    {
        private Breakpoint breakpoint;
        private GridNumber<ISharedOption> width;

        public BreakpointNumber(Breakpoint breakpoint, GridNumber<ISharedOption> width)
        {
            this.breakpoint = breakpoint;
            this.width = width;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, width.Value);
    }

}