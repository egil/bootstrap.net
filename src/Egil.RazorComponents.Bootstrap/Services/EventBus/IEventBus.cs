using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components;

namespace Egil.RazorComponents.Bootstrap.Services.EventBus
{
    public interface IEventBus
    {
        void Subscribe<TEventType, TSource>(TEventType eventType, Action<IEvent<TEventType, TSource>> eventHandler)
            where TEventType : IEventType
            where TSource : ComponentBase;

        void Unsubscribe<TEventType, TSource>(TEventType eventType, Action<IEvent<TEventType, TSource>> eventHandler)
            where TEventType : IEventType
            where TSource : ComponentBase;

        Task PublishAsync<TEventType, TSource>(IEvent<TEventType, TSource> @event)
            where TEventType : IEventType
            where TSource : ComponentBase;
    }
}
