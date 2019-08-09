using System;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;

namespace Egil.RazorComponents.Bootstrap.Options
{
    public interface IOption
    {
        public const string OptionSeparator = "-";

        string Value { get; }

        public string CombineWith(IOption otherOption) => string.Concat(Value, OptionSeparator, otherOption.Value);
    }


    public interface ITextualSize : IOption
    {
    }

    public interface IExtendedTextualSize : IOption
    {

    }

    public interface IOrderOption : IOption { }
    public interface ISpanOption : IOption { }
    public interface IAlignmentOption : IOption { }
    public interface IJustifyOption : IOption { }
    public interface IOffsetOption : IOption { }
    public interface ISideOption : IOption
    {
        new string Value { get; }

        public new string CombineWith(IOption otherOption) => string.Concat(Value, OptionSeparator, otherOption.Value);

        public static SpacingOption operator -(ISideOption side, int size)
        {
            return new SpacingOption(side, Number.ToSpacingNumber(size));
        }

        public static SpacingOption operator -(ISideOption side, AutoOption auto)
        {
            return new SpacingOption(side, auto);
        }

        public static IntermediateSpacingOption operator -(ISideOption side, ISpanOption breakpoint)
        {
            return new IntermediateSpacingOption(side, breakpoint);
        }
    }
    public interface ISpacingOption : IOption { }
    public interface IAutoOption : ISpacingOption, ISpanOption { }
    public interface IBreakpointWithNumber : IAutoOption, IOrderOption, IOffsetOption { }
    public interface IColorOption : IOption { }

    public interface IOptionSet<out TOption> : IReadOnlyCollection<TOption> where TOption : IOption
    { }
}