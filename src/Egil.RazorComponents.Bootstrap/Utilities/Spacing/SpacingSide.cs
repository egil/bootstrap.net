using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Utilities.Spacing
{
    [DebuggerDisplay("SpacingSide: {Value}")]
    public class SpacingSide : Option, ISpacingOption
    {
        public SpacingSideType Type { get; }

        public SpacingSide(SpacingSideType type)
        {
            Type = type;
        }

        public override string Value => Type switch
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

        public static IntermediateSpacingOption operator -(SpacingSide side, ISpanOption breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }

    public class IntermediateSpacingOption
    {
        private readonly SpacingSide _side;
        private readonly ISpanOption _breakpoint;

        public IntermediateSpacingOption(SpacingSide side, ISpanOption breakpoint)
        {
            this._side = side;
            this._breakpoint = breakpoint;
        }

        public static SpacingOption operator -(IntermediateSpacingOption intermediate, int size)
        {
            return new SpacingOption(intermediate._side, intermediate._breakpoint - Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(IntermediateSpacingOption intermediate, AutoOption auto)
        {
            return new SpacingOption(intermediate._side, intermediate._breakpoint - auto);
        }
    }
}

