using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public interface IComponent : IDisposable
    {
        protected internal void AddOverride<TUIEvent>(string key, EventCallback<TUIEvent> eventCallback);
        protected internal void AddOverride(string key, object value);
        protected internal void RemoveOverride<TUIEvent>(string key, EventCallback<TUIEvent> eventCallback);
        protected internal void RemoveOverride(string key);
        
        void StateHasChanged();
        protected Task Invoke(Action workItem);
        protected Task InvokeAsync(Func<Task> workItem);
    }
}
