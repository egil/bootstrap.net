using Egil.BlazorComponents.Bootstrap.Grid.Options;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointWithOption : BreakpointBase
    {
        private readonly Option option;

        public BreakpointWithOption(BreakpointType type, Option option) : base(type)
        {
            this.option = option;
        }

        public override string CssClass => string.Concat(base.CssClass, "-", option.CssClass);
    }
}