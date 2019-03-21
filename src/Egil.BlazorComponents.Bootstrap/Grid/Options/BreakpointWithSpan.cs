using Egil.BlazorComponents.Bootstrap.Grid.Options;
using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointWithNumber : BreakpointBase
    {
        private readonly int number;

        public BreakpointWithNumber(BreakpointType type, int number) : base(type)
        {
            ValidateGridNumberInRange(number);
            this.number = number;
        }

        public override string CssClass => string.Concat(base.CssClass, "-", number);
    }
}