namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointAuto : SpanOption
    {
        private Breakpoint breakpoint;
        private AutoOption option;

        public BreakpointAuto(Breakpoint breakpoint, AutoOption option)
        {
            this.breakpoint = breakpoint;
            this.option = option;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, option.Value);
    }

}