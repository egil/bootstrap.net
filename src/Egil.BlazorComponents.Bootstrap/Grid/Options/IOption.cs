using System.Collections.Generic;
using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public interface IOption
    {
        string Value { get; }
    }
    public interface IOption<out T> : IOption { }
    public interface ISharedOption : IOption<ISharedOption>, IOrderOption, ISpanOption { }
    public interface IOrderOption : IOption<IOrderOption> { }
    public interface ISpanOption : IOption<ISpanOption> { }
    public interface IAlignmentOption : IOption { }
    public interface IOffsetOption : IOption { }

    public interface IOptionSet<TOption> : IReadOnlyCollection<TOption> where TOption : IOption
    {
        void Add(TOption option);
    }
}