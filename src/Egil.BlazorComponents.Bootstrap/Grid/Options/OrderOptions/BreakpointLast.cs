using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{

    public class BreakpointLast : OrderOption
    {
        private Breakpoint breakpoint;
        private Last option;

        public BreakpointLast(Breakpoint breakpoint, Last option)
        {
            this.breakpoint = breakpoint;
            this.option = option;
        }

        public override string Value => string.Concat(breakpoint.Value, OptionSeparator, option.Value);
    }

}