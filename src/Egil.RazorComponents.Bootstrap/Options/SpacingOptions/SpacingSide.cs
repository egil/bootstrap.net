using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.SpacingOptions
{
    [DebuggerDisplay("SpacingSide: {Value}")]
    public class SpacingSide : ISpacingOption
    {
        private readonly SpacingSideType type;

        public SpacingSide(SpacingSideType type)
        {
            this.type = type;
        }

        public string Value => type switch
        {
            SpacingSideType.Top => "t",
            SpacingSideType.Bottom => "b",
            SpacingSideType.Left => "l",
            SpacingSideType.Right => "r",
            SpacingSideType.Horizontal => "x",
            SpacingSideType.Vertical => "y",
            _ => throw new InvalidOperationException($"Unknown {nameof(SpacingSideType)} specified in {nameof(SpacingSide)}.")
        };

        public static SpacingOption operator -(SpacingSide side, int size)
        {
            return new SpacingOption(side, Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(SpacingSide side, AutoOption auto)
        {
            return new SpacingOption(side, auto);
        }

        public static IntermediateSpacingOption operator -(SpacingSide side, Breakpoint breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }

    public class IntermediateSpacingOption
    {
        private readonly SpacingSide side;
        private readonly Breakpoint breakpoint;

        public IntermediateSpacingOption(SpacingSide side, Breakpoint breakpoint)
        {
            this.side = side;
            this.breakpoint = breakpoint;
        }

        public static SpacingOption operator -(IntermediateSpacingOption intermediate, int size)
        {
            return new SpacingOption(intermediate.side, intermediate.breakpoint - size);
        }

        public static SpacingOption operator -(IntermediateSpacingOption intermediate, AutoOption auto)
        {
            return new SpacingOption(intermediate.side, intermediate.breakpoint - auto);
        }
    }
}

