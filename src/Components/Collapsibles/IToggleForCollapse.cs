using System;
using System.Linq;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles.Events;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{

    public interface IToggleForCollapse : IComponent
    {
        /// <summary>
        /// Gets or sets the IDs of the <see cref="Collapse"/> components that this 
        /// component should be used to toggle. One or more IDs can be specified 
        /// by separating them with a comma or space.
        /// </summary>
        [Parameter] public string? ToggleFor { get; set; }

        protected string[] SubscribedToggleTargetIds { get; set; }

        protected EventCallback<UIMouseEventArgs>? ToggleForClickHandler { get; set; }

        protected IEventBus EventBus { get; }

        protected internal virtual void AddToggleHooks(ComponentBase component)
        {
            component.OnInitHook = OnInitHook;
            component.OnParametersSetHook = OnParametersSetHook;
            component.OnDisposedHook = OnDisposedHook;
        }

        private void OnInitHook(ComponentBase component)
        {
            InitToggleFor(ToggleFor?.SplitOnCommaOrSpace());
            InitClickHandler(component);
        }

        private void OnParametersSetHook(ComponentBase component)
        {
            var newTargetIds = ToggleFor?.SplitOnCommaOrSpace() ?? Array.Empty<string>();
            if (!SubscribedToggleTargetIds.SequenceEqual(newTargetIds))
            {
                DisposeToggleFor(SubscribedToggleTargetIds);
                InitToggleFor(newTargetIds);
            }
        }

        private void OnDisposedHook(ComponentBase component)
        {
            DisposeToggleFor(SubscribedToggleTargetIds);
        }

        private void InitToggleFor(string[]? targetIds)
        {
            if (targetIds is null || targetIds.Length == 0) return;

            foreach (var id in targetIds)
            {
                EventBus.Subscribe<CollapseStateChangedEventType, Collapse>(new CollapseStateChangedEventType(id), CollapseStateChangedHandler);
            }

            SubscribedToggleTargetIds = targetIds;

            AddOverride(HtmlAttrs.ARIA_CONTROLS, string.Join(" ", targetIds));
            AddOverride(HtmlAttrs.ROLE, "button");
        }

        private void DisposeToggleFor(string[]? subscribedIds)
        {
            if (subscribedIds is null || subscribedIds.Length == 0) return;

            foreach (var id in subscribedIds)
            {
                EventBus.Unsubscribe<CollapseStateChangedEventType, Collapse>(new CollapseStateChangedEventType(id), CollapseStateChangedHandler);
            }

            RemoveOverride(HtmlAttrs.ARIA_CONTROLS);
            RemoveOverride(HtmlAttrs.ROLE);
        }

        private void InitClickHandler(ComponentBase component)
        {
            if (!(ToggleFor is null) && !ToggleForClickHandler.HasValue)
            {
                ToggleForClickHandler = EventCallback.Factory.Create<UIMouseEventArgs>(this, _ =>
                {
                    foreach (var targetId in SubscribedToggleTargetIds)
                        EventBus.PublishAsync(new CollapseTogglerTriggeredEvent(component, targetId));
                });
            }
        }

        private void CollapseStateChangedHandler(IEvent<CollapseStateChangedEventType, Collapse> collapseStateChangedEvent)
        {
            AddOverride(HtmlAttrs.ARIA_EXPANDED, collapseStateChangedEvent.Source.Expanded.ToLowerCaseString());
            Invoke(StateHasChanged);
        }
    }
}
