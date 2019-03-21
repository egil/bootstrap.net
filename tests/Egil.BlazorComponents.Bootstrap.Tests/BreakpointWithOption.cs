namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class BreakpointWithOption : Breakpoint
    {
        private readonly Option modifier;

        public BreakpointWithOption(BreakpointType type, Option modifier) : base(type)
        {
            this.modifier = modifier;
        }

        public override string CssClass => string.Concat(base.CssClass, "-", modifier.CssClass);
    }

}