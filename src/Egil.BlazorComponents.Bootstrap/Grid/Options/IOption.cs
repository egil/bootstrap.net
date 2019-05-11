using System.Collections.Generic;
using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public interface IOption
    {
        string Value { get; }
    }

    public interface IOrderOption : IOption { }
    public interface ISpanOption : IOption { }
    public interface IAlignmentOption : IOption { }
    public interface IJustifyOption : IOption { }
    public interface IOffsetOption : IOption { }
    public interface ISpacingOption : IOption { }
    public interface IAutoOption : ISpacingOption, ISpanOption { }
    public interface IBreakpointWithNumber : ISpanOption, IOrderOption, IOffsetOption { }

    public interface IOptionSet<out TOption> : IReadOnlyCollection<TOption> where TOption : IOption
    { }
}