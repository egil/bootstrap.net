using Egil.RazorComponents.Bootstrap.Options.CommonOptions;

namespace Egil.RazorComponents.Bootstrap.Options.SpacingOptions
{
    public class SpacingOption : ISpacingOption
    {
        public string Value { get; }

        public SpacingOption(SpacingSide side, Number size)
        {
            Value = string.Concat(side.Value, Option.OptionSeparator, size.Value);
        }

        public SpacingOption(SpacingOption spacing, Number size)
        {
            Value = string.Concat(spacing.Value, Option.OptionSeparator, size.Value);
        }

        public SpacingOption(SpacingSide side, BreakpointWithNumber breakpoint)
        {
            breakpoint.Number.ValidateAsSpacingNumber();
            Value = string.Concat(side.Value, Option.OptionSeparator, breakpoint.Value);
        }

        public SpacingOption(SpacingSide side, AutoOption auto)
        {
            Value = string.Concat(side.Value, Option.OptionSeparator, auto.Value);
        }

        public SpacingOption(SpacingSide side, BreakpointAuto breakpointAuto)
        {
            Value = string.Concat(side.Value, Option.OptionSeparator, breakpointAuto.Value);
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
            OptionSet<ISpacingOption> spacingSet = new OptionSet<ISpacingOption>(set);
            spacingSet.Add(option);
            return spacingSet;
        }

        public static OptionSet<ISpacingOption> operator |(OptionSet<ISpacingOption> set, SpacingOption option)
        {
            set.Add(option);
            return set;
        }
    }
}