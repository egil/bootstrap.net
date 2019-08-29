using System;
using Egil.RazorComponents.Bootstrap.Services.EventBus;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles.Events
{
    internal readonly struct CollapseStateChangedEventType : IEventType
    {
        public string Key { get; }

        public bool CacheLatest { get; }

        public CollapseStateChangedEventType(string collapseId)
        {
            Key = $"{nameof(CollapseStateChangedEventType)}-{collapseId}";
            CacheLatest = true;
        }
    }

    internal class CollapseStateChangedEvent : IEvent<CollapseStateChangedEventType, Collapse>
    {
        public CollapseStateChangedEventType Type { get; }

        public Collapse Source { get; }

        public CollapseStateChangedEvent(Collapse source)
        {
            if (source.Id is null) throw new ArgumentException("Collapse source's ID cannot be null");
            Type = new CollapseStateChangedEventType(source.Id);
            Source = source;
        }
    }
}