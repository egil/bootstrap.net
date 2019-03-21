using System;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public class Breakpoint : Option
    {
        private readonly BreakpointType type;

        public Breakpoint(BreakpointType type)
        {
            this.type = type;
        }

        public static BreakpointWithSpan operator -(Breakpoint breakpoint, int width)
        {
            return new BreakpointWithSpan(breakpoint.type, width);
        }

        public static BreakpointWithOption operator -(Breakpoint breakpoint, Option option)
        {
            return new BreakpointWithOption(breakpoint.type, option);
        }

        public static BreakpointWithFirstOption operator -(Breakpoint breakpoint, FirstOption option)
        {
            return new BreakpointWithFirstOption(breakpoint.type, option);
        }

        public static BreakpointWithLastOption operator -(Breakpoint breakpoint, LastOption option)
        {
            return new BreakpointWithLastOption(breakpoint.type, option);
        }

        public override string CssClass => type switch
        {
            BreakpointType.None => string.Empty,
            BreakpointType.Small => "sm",
            BreakpointType.Medium => "md",
            BreakpointType.Large => "lg",
            BreakpointType.ExtraLarge => "xl",
            _ => throw new InvalidOperationException($"Unknown {nameof(BreakpointType)} specified in {nameof(Breakpoint)}.")
        };
    }
}