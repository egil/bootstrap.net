using System;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    public class PageVisibilityAPIService : IPageVisibilityAPI, IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Func<PageVisibilityAPIService, DotNetObjectRef<PageVisibilityAPIService>> _dotNetObjectRefFactory;
        private EventHandler<PageVisibilityChangedEventArgs>? _onPageVisibleChanged;
        private bool _disposed = false;
        private bool _subscribed = false;
        private bool _subscribing = false;
        private bool _unsubscribing = false;

        /// <summary>
        /// An event that fires when the pages visibilty has changed.
        /// </summary>
        public event EventHandler<PageVisibilityChangedEventArgs> OnPageVisibilityChanged
        {
            add
            {
                EnsureSubscribedToAPI();
                _onPageVisibleChanged += value;
            }
            remove
            {
                _onPageVisibleChanged -= value;
                if (_onPageVisibleChanged is null)
                {
                    UnsubscribeFromAPI();
                }
            }
        }

        public bool IsPageVisible { get; private set; } = true;

        public PageVisibilityAPIService(IJSRuntime jsRuntime) : this(jsRuntime, null)
        { }

        internal PageVisibilityAPIService(IJSRuntime jSRuntime, Func<PageVisibilityAPIService, DotNetObjectRef<PageVisibilityAPIService>>? dotNetObjectRefFactory)
        {
            _jsRuntime = jSRuntime;
            _dotNetObjectRefFactory = dotNetObjectRefFactory ?? CreateDotNetObjectRef;
        }

        [JSInvokable]
        public void SetVisibilityState(bool isVisible)
        {
            if (IsPageVisible != isVisible)
            {
                IsPageVisible = isVisible;
                _onPageVisibleChanged?.Invoke(this, new PageVisibilityChangedEventArgs(isVisible));
            }
        }

        private async void EnsureSubscribedToAPI()
        {
            if (_subscribing) return;

            try
            {
                _subscribing = true;

                if (!_subscribed)
                {
                    await _jsRuntime.InvokeAsync<object>("window.bootstrapDotNet.services.pageVisibilityApiInterop.subscribe", _dotNetObjectRefFactory(this), nameof(SetVisibilityState));
                    _subscribed = true;
                }
            }
            catch (InvalidOperationException e)
            {
                throw new PageVisibilityApiInteropException("Unable to setup Page Visibility API Adapter and/or subscribe to it. " +
                    "JsRuntime might not be available yet. Make sure you are subscribing during OnAfterRender.", e);
            }
            finally
            {
                _subscribing = false;
            }
        }

        private async void UnsubscribeFromAPI()
        {
            if (_unsubscribing) return;
            try
            {
                _unsubscribing = true;
                
                await _jsRuntime.InvokeAsync<object>("window.bootstrapDotNet.services.pageVisibilityApiInterop.unsubscribe");
                
                _subscribed = false; 
                _subscribing = false; 
            }
            catch (InvalidOperationException e)
            {
                throw new PageVisibilityApiInteropException("Unable to unsubscribe from Page Visibility API Adapter. " +
                    "JsRuntime might not be available. Make sure you are unsubscribing during OnAfterRender.", e);
            }
            finally
            {
                _unsubscribing = false;
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _onPageVisibleChanged = null;
                    UnsubscribeFromAPI();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region Hack to fix https://github.com/aspnet/AspNetCore/issues/11159
        public static object CreateDotNetObjectRefSyncObj = new object();

        protected DotNetObjectRef<T> CreateDotNetObjectRef<T>(T value) where T : class
        {
            lock (CreateDotNetObjectRefSyncObj)
            {
                JSRuntime.SetCurrentJSRuntime(_jsRuntime);
                return DotNetObjectRef.Create(value);
            }
        }
        #endregion
    }
}
