using Egil.RazorComponents.Bootstrap.Components;

namespace Egil.RazorComponents.Bootstrap.Services.EventBus
{
    public interface IEvent<TEventType, TSource>
        where TEventType : IEventType
        where TSource : ComponentBase
    {
        TEventType Type { get; }

        TSource Source { get; }
    }
}
