using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components;

namespace Egil.RazorComponents.Bootstrap.Services.EventBus
{
    // TODO: Investigate race conditions between removal and adding - also with cache -> https://docs.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.8
    public class ComponentEventBus : IEventBus
    {
        private readonly Dictionary<string, Delegate> _eventSubscriptions = new Dictionary<string, Delegate>();
        private readonly Dictionary<string, object> _eventCache = new Dictionary<string, object>();

        public Task PublishAsync<TEventType, TSource>(IEvent<TEventType, TSource> publishedEvent) where TEventType : IEventType where TSource : ComponentBase
        {
            CacheLatests<TEventType, TSource>(publishedEvent);

            if (_eventSubscriptions.TryGetValue(publishedEvent.Type.Key, out var handlers))
            {
                return Task.Run(() => ((Action<IEvent<TEventType, TSource>>)handlers)(publishedEvent));
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        private void CacheLatests<TEventType, TSource>(IEvent<TEventType, TSource> latestEvent) where TEventType : IEventType where TSource : ComponentBase
        {
            if (latestEvent.Type.CacheLatest)
            {
                latestEvent.Source.OnDisposedHook = _ => _eventCache.Remove(latestEvent.Type.Key);
                _eventCache[latestEvent.Type.Key] = latestEvent;
            }
        }

        public void Subscribe<TEventType, TSource>(TEventType eventType, Action<IEvent<TEventType, TSource>> addedEventHandler) where TEventType : IEventType where TSource : ComponentBase
        {
            PublishLatests<TEventType, TSource>(eventType, addedEventHandler);

            if (_eventSubscriptions.TryGetValue(eventType.Key, out var existingEventHandler))
            {
                var newHandler = ((Action<IEvent<TEventType, TSource>>)existingEventHandler) + addedEventHandler;
                _eventSubscriptions[eventType.Key] = newHandler;
            }
            else
            {
                _eventSubscriptions[eventType.Key] = addedEventHandler;
            }
        }

        private void PublishLatests<TEventType, TSource>(TEventType eventType, Action<IEvent<TEventType, TSource>> eventHandler) where TEventType : IEventType where TSource : ComponentBase
        {
            if (eventType.CacheLatest && _eventCache.TryGetValue(eventType.Key, out var cachedEvent))
            {
                eventHandler((IEvent<TEventType, TSource>)cachedEvent);
            }
        }

        public void Unsubscribe<TEventType, TSource>(TEventType eventType, Action<IEvent<TEventType, TSource>> removedEventHandler) where TEventType : IEventType where TSource : ComponentBase
        {
            if (_eventSubscriptions.TryGetValue(eventType.Key, out var existingEventHandler))
            {
                var newHandler = ((Action<IEvent<TEventType, TSource>>)existingEventHandler) - removedEventHandler;

                if (newHandler is null)
                {
                    _eventSubscriptions.Remove(eventType.Key);
                }
                else
                {
                    _eventSubscriptions[eventType.Key] = newHandler;
                }
            }
        }
    }
}
