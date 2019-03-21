using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class BreakpointBase : Option
    {
        protected readonly BreakpointType type;

        public BreakpointBase(BreakpointType type)
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

        public static IntermediateOptionSet operator |(BreakpointBase breakpoint, int number)
        {
            return new IntermediateOptionSet(breakpoint, number);
        }
    }

    public class Breakpoint : BreakpointBase
    {
        public Breakpoint(BreakpointType type) : base(type)
        {
        }
    }

    public class IntermediateOptionSet
    {
        public Option Option { get; }
        public int Number { get; }

        public IntermediateOptionSet(Option breakpoint, int number)
        {
            Option = breakpoint;
            Number = number;
        }        
    }
}