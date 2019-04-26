using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions
{
    [DebuggerDisplay("AlignmentOption: {Value}")]
    public sealed class AlignmentOption : IAlignmentOption
    {
        private readonly AlignmentType type;

        public AlignmentOption(AlignmentType type)
        {
            this.type = type;
        }

        public string Value => type switch
        {
            AlignmentType.Start => "start",
            AlignmentType.End => "end",
            AlignmentType.Center => "center",
            AlignmentType.Stretch => "stretch",
            _ => throw new InvalidOperationException($"Unknown {nameof(AlignmentType)} specified in {nameof(AlignmentOption)}.")
        };

        public static BreakpointAlignmentOption operator -(Breakpoint breakpoint, AlignmentOption option)
        {
            return new BreakpointAlignmentOption(breakpoint, option);
        }

        public static IOptionSet<IAlignmentOption> operator |(AlignmentOption option1, IAlignmentOption option2)
        {
            return new OptionSet2<IAlignmentOption> { option1, option2 };
        }

        public static IOptionSet<IAlignmentOption> operator |(IOptionSet<IAlignmentOption> set, AlignmentOption option2)
        {
            set.Add(option2);
            return set;
        }
    }
}
