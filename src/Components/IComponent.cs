using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public interface IComponent : IDisposable
    {
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
        protected internal void AddOverride<TUIEvent>(string key, EventCallback<TUIEvent> eventCallback);
        protected internal void AddOverride(string key, object value);
        protected internal void RemoveOverride(string key);
        
        void StateHasChanged();
        protected Task InvokeAsync(Action workItem);
        protected Task InvokeAsync(Func<Task> workItem);
    }
}
