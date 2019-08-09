using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.AlignmentOptions
{
    [DebuggerDisplay("AlignmentOption: {Value}")]
    public sealed class AlignmentOption : Option, IAlignmentOption, IJustifyOption
    {
        private readonly AlignmentType type;

        public AlignmentOption(AlignmentType type)
        {
            this.type = type;
        }

        public override string Value => type switch
        {
            AlignmentType.Start => "start",
            AlignmentType.End => "end",
            AlignmentType.Center => "center",
            _ => throw new InvalidOperationException($"Unknown {nameof(AlignmentType)} specified in {nameof(AlignmentOption)}.")
        };

        public static BreakpointAlignmentOption operator -(ISpanOption breakpoint, AlignmentOption option)
        {
            return new BreakpointAlignmentOption(breakpoint, option);
        }

        public static OptionSet<IAlignmentOption> operator |(AlignmentOption option1, IAlignmentOption option2)
        {
            return new OptionSet<IAlignmentOption> { option1, option2 };
        }

        public static OptionSet<IAlignmentOption> operator |(OptionSet<IAlignmentOption> set, AlignmentOption option2)
        {
            set.Add(option2);
            return set;
        }

        public static OptionSet<IJustifyOption> operator |(OptionSet<IJustifyOption> set, AlignmentOption option2)
        {
            set.Add(option2);
            return set;
        }
    }
}
