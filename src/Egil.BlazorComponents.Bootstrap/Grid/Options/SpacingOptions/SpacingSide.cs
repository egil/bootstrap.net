using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options.SpacingOptions
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

