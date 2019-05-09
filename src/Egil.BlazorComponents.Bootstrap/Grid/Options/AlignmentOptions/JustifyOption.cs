using System;
using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions
{
    [DebuggerDisplay("JustifyOption: {Value}")]
    public sealed class JustifyOption : IJustifyOption
    {
        private readonly JustifyType type;

        public JustifyOption(JustifyType type)
        {
            this.type = type;
        }

        public string Value => type switch
        {
            JustifyType.Between => "between",
            JustifyType.Around => "around",
            _ => throw new InvalidOperationException($"Unknown {nameof(IJustifyOption)} specified in {nameof(JustifyOption)}.")
        };

        public static BreakpointJustifyOption operator -(Breakpoint breakpoint, JustifyOption option)
        {
            return new BreakpointJustifyOption(breakpoint, option);
        }

        public static OptionSet<IJustifyOption> operator |(JustifyOption option1, JustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static OptionSet<IJustifyOption> operator |(JustifyOption option1, IJustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static OptionSet<IJustifyOption> operator |(AlignmentOption option1, JustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static OptionSet<IJustifyOption> operator |(BreakpointAlignmentOption option1, JustifyOption option2)
        {
            return new OptionSet<IJustifyOption> { option1, option2 };
        }

        public static OptionSet<IJustifyOption> operator |(OptionSet<IJustifyOption> set, JustifyOption option2)
        {
            set.Add(option2);
            return set;
        }
    }
}
