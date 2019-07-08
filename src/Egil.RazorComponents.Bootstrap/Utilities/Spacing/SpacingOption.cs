using System;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Options.SimpleOptions;

namespace Egil.RazorComponents.Bootstrap.Utilities.Spacing
{
    public class SpacingOption : Option, ISpacingOption
    {
        public override string Value { get; }

        public SpacingOption(ISideOption side, Number size)
        {
            Value = side.CombineWith(size);
        }

        public SpacingOption(SpacingOption spacing, Number size)
        {
            Value = spacing.CombineWith(size);
        }

        public SpacingOption(ISideOption side, BreakpointWithNumber breakpoint)
        {
            breakpoint.Number.ValidateAsSpacingNumber();
            Value = side.CombineWith(breakpoint);
        }

        public SpacingOption(ISideOption side, AutoOption auto)
        {
            Value = side.CombineWith(auto);
        }

        public SpacingOption(ISideOption side, BreakpointAuto breakpointAuto)
        {
            Value = side.CombineWith(breakpointAuto);
        }

        public static SpacingOption operator -(SpacingOption spacing, int size)
        {
            return new SpacingOption(spacing, Number.ToSpacingNumber(size));
        }

        public static OptionSet<ISpacingOption> operator |(SpacingOption option1, int number)
        {
            return new OptionSet<ISpacingOption>() { option1, (Number)number };
        }

        public static OptionSet<ISpacingOption> operator |(int number, SpacingOption option1)
        {
            return new OptionSet<ISpacingOption>() { option1, (Number)number };
        }

        public static OptionSet<ISpacingOption> operator |(SpacingOption option1, ISpacingOption option2)
        {
            return new OptionSet<ISpacingOption>() { option1, option2 };
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<IAutoOption> set, SpacingOption option)
        {
            OptionSet<ISpacingOption> spacingSet = new OptionSet<ISpacingOption>(set) { option };
            return spacingSet;
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<ISpacingOption> set, SpacingOption option)
        {
            set.Add(option);
            return set;
        }
    }
}