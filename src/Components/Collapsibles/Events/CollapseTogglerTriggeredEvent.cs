using System;
using Egil.RazorComponents.Bootstrap.Services.EventBus;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles.Events
{
    internal readonly struct CollapseTogglerTriggeredEventType : IEventType
    {
        public string Key { get; }

        public bool CacheLatest { get; }

        public CollapseTogglerTriggeredEventType(string collapseId)
        {
            Key = $"{nameof(CollapseTogglerTriggeredEventType)}-{collapseId}";
            CacheLatest = false;
        }
    }

    internal class CollapseTogglerTriggeredEvent : IEvent<CollapseTogglerTriggeredEventType, ComponentBase>
    {
        public CollapseTogglerTriggeredEventType Type { get; }

        public ComponentBase Source { get; }

        public CollapseTogglerTriggeredEvent(ComponentBase source, string collapseId)
        {
            if (collapseId is null) throw new ArgumentNullException(nameof(collapseId), "Target's ID cannot be null");
            Type = new CollapseTogglerTriggeredEventType(collapseId);
            Source = source;
        }
    }
}