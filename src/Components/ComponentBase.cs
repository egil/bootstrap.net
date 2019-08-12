using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public abstract class ComponentBase : Microsoft.AspNetCore.Components.ComponentBase, IComponent
    {
        private static readonly Dictionary<string, object> EmptyDictionary = new Dictionary<string, object>(0);

        private bool _disposed = false;
        private bool _isFirstRender = true;
        private ChildHooksInjector _parentHooks = Noop;
        private Action<ComponentBase> _onInitHook = Noop;
        private Func<ComponentBase, Task> _onInitHookAsync = NoopAsync;
        private Action<ComponentBase> _onParametersSetHook = Noop;
        private Func<ComponentBase, Task> _onParametersSetHookAsync = NoopAsync;
        private Action<ComponentBase> _onAfterFirstRenderHook = Noop;
        private Func<ComponentBase, Task> _onAfterFirstRenderHookAsync = NoopAsync;
        private Action<ComponentBase> _onAfterRenderHook = Noop;
        private Func<ComponentBase, Task> _onAfterRenderHookAsync = NoopAsync;
        private Action<ComponentBase> _onDisposedHook = Noop;
        private Dictionary<string, object>? _overriddenAttributes;

        protected internal string DefaultCssClass { get; set; } = string.Empty;

        protected internal string DefaultElementTag { get; set; } = "div";

        protected internal IReadOnlyDictionary<string, object> OverriddenAttributes => _overriddenAttributes ?? EmptyDictionary;

        [Parameter(CaptureUnmatchedValues = true)]
        protected internal IReadOnlyDictionary<string, object> AdditionalAttributes { get; private set; } = EmptyDictionary;

        [CascadingParameter] internal ChildHooksInjector ParentHooksInjector { get => _parentHooks; private set => _parentHooks = value ?? Noop; }

        [Parameter] public virtual string? Id { get; set; }

        [Parameter] public virtual string? Class { get; set; }

        [Parameter] public virtual bool DisableParentOverrides { get; set; }

        protected override bool ShouldRender() => !_disposed && base.ShouldRender();

        protected internal void AddOverride<TUIEvent>(string key, EventCallback<TUIEvent>? eventCallback)
        {
            if (eventCallback is null) return;
            if (_overriddenAttributes is null) _overriddenAttributes = new Dictionary<string, object>();
            _overriddenAttributes[key] = eventCallback;
        }

        protected internal void AddOverride(string key, object? value)
        {
            if (value is null) return;
            if (_overriddenAttributes is null) _overriddenAttributes = new Dictionary<string, object>();
            _overriddenAttributes[key] = value;
        }

        protected internal void RemoveOverride<TUIEvent>(string key, EventCallback<TUIEvent> eventCallback)
        {
            if (_overriddenAttributes is null) return;
            _overriddenAttributes.Remove(key);
        }

        protected internal void RemoveOverride(string key)
        {
            if (_overriddenAttributes is null) return;
            _overriddenAttributes.Remove(key);
        }

        /// <summary>
        /// Creates a new event callback that will call any user provided callback 
        /// (in <see cref="AdditionalAttributes"/>) and the callbacks provided to this method.
        /// </summary>
        protected EventCallback<T>? JoinEventCallbacks<T>(string eventName, params EventCallback<T>?[] eventCallbacks)
        {
            if ((AdditionalAttributes.ContainsKey(eventName) && eventCallbacks.Length > 0) || eventCallbacks.Length > 1)
                return EventCallback.Factory.Create<T>(this, CallbackGroup);
            else if (eventCallbacks.Length == 1)
                return eventCallbacks[0];
            else
                return null;

            Task CallbackGroup(T e)
            {
                var tasks = new List<Task>();

                if (AdditionalAttributes.TryGetValue<string, EventCallback<T>>(eventName, out var userCallback))
                    tasks.Add(userCallback.InvokeAsync(e));

                foreach (var callback in eventCallbacks)
                {
                    if (callback.HasValue)
                    {
                        tasks.Add(callback.Value.InvokeAsync(e));
                    }
                }

                return Task.WhenAll(tasks);
            }
        }

        protected internal RenderFragment? CustomRenderFragment { get; set; }
        protected internal Action<ElementRef>? DomElementCapture { get; set; }

        protected internal string CssClassValue => this.BuildCssClass();

        #region Blazor life-cycle methods
        protected sealed override void OnInit()
        {
            base.OnInit();
            if (!DisableParentOverrides) ParentHooksInjector(this);
            OnCompomnentInit();
            OnInitHook(this);
        }
        protected sealed async override Task OnInitAsync()
        {
            await base.OnInitAsync();
            await OnCompomnentInitAsync();
            await OnInitHookAsync(this);
        }

        protected sealed override void OnParametersSet()
        {
            base.OnParametersSet();
            OnCompomnentParametersSet();
            OnParametersSetHook(this);
        }

        protected sealed override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await OnCompomnentParametersSetAsync();
            await OnParametersSetHookAsync(this);
        }

        protected sealed override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var renderFragment = CustomRenderFragment ?? DefaultRenderFragment;
            renderFragment(builder);
        }

        protected sealed override void OnAfterRender()
        {
            base.OnAfterRender();
            if (_isFirstRender)
            {
                OnCompomnentAfterFirstRender();
                OnAfterFirstRenderHook(this);
            }
            OnCompomnentAfterRender();
            OnAfterFirstRenderHook(this);
        }

        protected sealed override async Task OnAfterRenderAsync()
        {
            await base.OnAfterRenderAsync();

            if (_isFirstRender)
            {
                _isFirstRender = false;
                await OnCompomnentAfterFirstRenderAsync();
                await OnAfterFirstRenderHookAsync(this);
            }

            await OnCompomnentAfterRenderAsync();
            await OnAfterRenderHookAsync(this);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                OnCompomnentDispose();
                OnDisposedHook(this);
            }
        }
        #endregion

        #region Sub component life-cycle methods
        protected virtual void OnCompomnentInit() { }
        protected virtual Task OnCompomnentInitAsync() => Task.CompletedTask;
        protected virtual void OnCompomnentParametersSet() { }
        protected virtual Task OnCompomnentParametersSetAsync() => Task.CompletedTask;
        protected internal virtual void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementTag);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(_overriddenAttributes);
            builder.AddElementReferenceCapture(DomElementCapture);
            builder.CloseElement();
        }
        protected virtual void OnCompomnentAfterFirstRender() { }
        protected virtual Task OnCompomnentAfterFirstRenderAsync() => Task.CompletedTask;
        protected virtual void OnCompomnentAfterRender() { }
        protected virtual Task OnCompomnentAfterRenderAsync() => Task.CompletedTask;
        protected virtual void OnCompomnentDispose() { }
        #endregion

        #region Life-cycle hooks
        // NOTE: Each hook uses the Action<T> += operator to 
        //       chain hooks in the order they are added.
        internal Action<ComponentBase> OnInitHook
        {
            private get => _onInitHook; set => _onInitHook += value;
        }
        internal Func<ComponentBase, Task> OnInitHookAsync
        {
            private get => _onInitHookAsync; set => _onInitHookAsync += value;
        }
        internal Action<ComponentBase> OnParametersSetHook
        {
            private get => _onParametersSetHook; set => _onParametersSetHook += value;
        }
        internal Func<ComponentBase, Task> OnParametersSetHookAsync
        {
            private get => _onParametersSetHookAsync; set => _onParametersSetHookAsync += value;
        }
        internal Action<ComponentBase> OnAfterFirstRenderHook
        {
            private get => _onAfterFirstRenderHook; set => _onAfterFirstRenderHook += value;
        }
        internal Func<ComponentBase, Task> OnAfterFirstRenderHookAsync
        {
            private get => _onAfterFirstRenderHookAsync; set => _onAfterFirstRenderHookAsync += value;
        }
        internal Action<ComponentBase> OnAfterRenderHook
        {
            private get => _onAfterRenderHook; set => _onAfterRenderHook += value;
        }
        internal Func<ComponentBase, Task> OnAfterRenderHookAsync
        {
            private get => _onAfterRenderHookAsync; set => _onAfterRenderHookAsync += value;
        }
        internal Action<ComponentBase> OnDisposedHook
        {
            private get => _onDisposedHook; set => _onDisposedHook += value;
        }
        protected internal static void Noop(ComponentBase _) { }
        protected internal static Task NoopAsync(ComponentBase _) => Task.CompletedTask;

        #endregion

        #region IComponent explicit implementation
        void IComponent.StateHasChanged() => StateHasChanged();
        Task IComponent.Invoke(Action workItem) => Invoke(workItem);
        Task IComponent.InvokeAsync(Func<Task> workItem) => InvokeAsync(workItem);
        void IComponent.AddOverride<TUIEvent>(string key, EventCallback<TUIEvent> eventCallback) => AddOverride(key, eventCallback);
        void IComponent.AddOverride(string key, object value) => AddOverride(key, value);
        void IComponent.RemoveOverride<TUIEvent>(string key, EventCallback<TUIEvent> eventCallback) => RemoveOverride(key, eventCallback);
        void IComponent.RemoveOverride(string key) => RemoveOverride(key);
        #endregion
    }
}
