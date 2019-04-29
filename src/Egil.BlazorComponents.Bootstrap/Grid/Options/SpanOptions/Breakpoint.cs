using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;
using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class Breakpoint : ISpanOption
    {
        private BreakpointType type;

        public Breakpoint(BreakpointType type)
        {
            this.type = type;
        }

        public string Value => type switch
        {
            BreakpointType.Small => "sm",
            BreakpointType.Medium => "md",
            BreakpointType.Large => "lg",
            BreakpointType.ExtraLarge => "xl",
            _ => throw new InvalidOperationException($"Unknown {nameof(BreakpointType)} specified in {nameof(Breakpoint)}.")
        };

        public static GridBreakpoint operator -(Breakpoint breakpoint, int width)
        {
            return new GridBreakpoint(breakpoint, width);
        }

        public static BreakpointFirst operator -(Breakpoint breakpoint, FirstOption option)
        {
            return new BreakpointFirst(breakpoint, option);
        }

        public static BreakpointLast operator -(Breakpoint breakpoint, LastOption option)
        {
            return new BreakpointLast(breakpoint, option);
        }

        public static BreakpointAuto operator -(Breakpoint breakpoint, AutoOption option)
        {
            return new BreakpointAuto(breakpoint, option);
        }

        public static OptionSet<ISpanOption> operator |(Breakpoint option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(ISpanOption option1, Breakpoint option2)
        {
            return new OptionSet<ISpanOption> { option2, option1 };
        }
    }
}