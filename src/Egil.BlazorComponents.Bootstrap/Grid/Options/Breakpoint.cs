using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointBase : Option
    {
        protected readonly BreakpointType type;
        protected BreakpointBase(BreakpointType type)
        {
            this.type = type;
        }

        public override string CssClass => type switch
        {
            BreakpointType.Small => "sm",
            BreakpointType.Medium => "md",
            BreakpointType.Large => "lg",
            BreakpointType.ExtraLarge => "xl",
            _ => throw new InvalidOperationException($"Unknown {nameof(BreakpointType)} specified in {nameof(Breakpoint)}.")
        };

        public static BreakpointWithNumber operator -(BreakpointBase breakpoint, int width)
        {
            return new BreakpointWithNumber(breakpoint.type, width);
        }

        public static BreakpointWithFirstOption operator -(BreakpointBase breakpoint, FirstOption option)
        {
            return new BreakpointWithFirstOption(breakpoint.type, option);
        }

        public static BreakpointWithLastOption operator -(BreakpointBase breakpoint, LastOption option)
        {
            return new BreakpointWithLastOption(breakpoint.type, option);
        }

        public static BreakpointWithAutoOption operator -(BreakpointBase breakpoint, AutoOption option)
        {
            return new BreakpointWithAutoOption(breakpoint.type, option);
        }
    }

    public class Breakpoint : BreakpointBase
    {
        public Breakpoint(BreakpointType type) : base(type)
        {
        }
    }
}