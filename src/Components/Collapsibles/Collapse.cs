using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.Context;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles.Events;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    public sealed class Collapse : ParentComponentBase
    {
        private const string CollapsedCssClass = "collapse";
        private ElementReference _domElement;
        private string? _subscribedId;

        private readonly Action<IEvent<CollapseTogglerTriggeredEventType, ComponentBase>> _togglerTriggeredHandler;

        [Inject] private IJSRuntime? JSRuntime { get; set; }

        [Inject] private IEventBus? EventBus { get; set; }

        [CssClassToggleParameter("show")] private bool Showing { get; set; }

        [Parameter] public bool Expanded { get; set; }

        [Parameter] public string? AriaLabelledBy { get; set; }

        public Collapse()
        {
            DefaultCssClass = CollapsedCssClass;
            DomElementCapture = (elm) => _domElement = elm;
            _togglerTriggeredHandler = _ => InvokeAsync(Toggle);
        }

        public void Toggle()
        {
            Expanded = !Expanded;
            InvokeAsync(StateHasChanged);
        }

        public void Show()
        {
            if (Expanded) return;
            Toggle();
        }

        public void Hide()
        {
            if (!Expanded) return;
            Toggle();
        }

        protected override Task OnCompomnentInitAsync()
        {
            Showing = Expanded;

            if (Id is null)
                return Task.CompletedTask;
            else
            {
                SubscribeToTogglerEvents();
                return EventBus!.PublishAsync(new CollapseStateChangedEvent(this)); // BANG: injected via setters at this point
            }
        }

        protected override void OnCompomnentParametersSet()
        {
            if (_subscribedId != Id)
            {
                UnsubscribeToTogglerEvents();
                SubscribeToTogglerEvents();
            }

            AddOverride(HtmlAttrs.ARIA_LABELLEDBY, AriaLabelledBy);
        }

        protected override async Task OnCompomnentAfterRenderAsync()
        {
            if (Expanded != Showing)
            {
                var _ = EventBus!.PublishAsync(new CollapseStateChangedEvent(this)); // BANG: injected via setters at this point
            }

            if (Expanded && !Showing)
            {
                await _domElement.Show(JSRuntime);
                Showing = true;
            }
            else if (!Expanded && Showing)
            {
                await _domElement.Hide(JSRuntime);
                Showing = false;
            }
        }

        protected override void OnCompomnentDispose()
        {
            UnsubscribeToTogglerEvents();
        }

        private void SubscribeToTogglerEvents()
        {
            if (Id is null) return;

            _subscribedId = Id;
            EventBus!.Subscribe(new CollapseTogglerTriggeredEventType(_subscribedId), _togglerTriggeredHandler);
        }

        private void UnsubscribeToTogglerEvents()
        {
            if (_subscribedId is null) return;

            EventBus!.Unsubscribe(new CollapseTogglerTriggeredEventType(_subscribedId), _togglerTriggeredHandler);
        }

    }
}
