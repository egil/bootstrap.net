using Egil.RazorComponents.Bootstrap.Components;

namespace Egil.RazorComponents.Bootstrap.Services.EventBus
{
    public readonly struct Event<TEventType, TSource> : IEvent<TEventType, TSource>
        where TEventType : IEventType
        where TSource : ComponentBase
    {
        public TEventType Type { get; }

        public TSource Source { get; }

        public Event(TEventType type, TSource source)
        {
            Type = type;
            Source = source;
        }
    }
}
