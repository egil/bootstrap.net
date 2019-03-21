using System;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class BreakpointWithSpan : Breakpoint
    {
        private readonly int number;

        public BreakpointWithSpan(BreakpointType type, int number) : base(type)
        {
            if (type == BreakpointType.None)
                throw new InvalidOperationException($"Cannot assign a width to a breakpoint with type '{nameof(BreakpointType.None)}'.");

            ValidateGridNumber(number);

            this.number = number;
        }

        public override string CssClass => string.Concat(base.CssClass, "-", number);
    }    
}