namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public interface IOption<out T>
    {
        string Value { get; }
    }
    public interface ISharedOption : IOption<ISharedOption>, IOrderOption, ISpanOption { }
    public interface IOrderOption : IOption<IOrderOption> { }
    public interface ISpanOption : IOption<ISpanOption> { }
}