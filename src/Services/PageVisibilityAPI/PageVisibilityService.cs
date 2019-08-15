using System;
using System.Threading;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    public enum PageVisibilityAPIServiceStatus
    {
        Initialized = 0,
        Subscribing,
        Subscribed
    }

    public class PageVisibilityAPIService : IPageVisibilityAPI, IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IComponentContext _componentContext;
        private readonly Func<PageVisibilityAPIService, DotNetObjectRef<PageVisibilityAPIService>> _dotNetObjectRefFactory;
        private EventHandler<PageVisibilityChangedEventArgs>? _onPageVisibleChanged;
        private CancellationTokenSource? _cancellationTokenSource;
        private bool _disposed = false;

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

        public PageVisibilityAPIServiceStatus Status { get; private set; }

        public bool IsPageVisible { get; private set; } = true;

        public PageVisibilityAPIService(IJSRuntime jsRuntime, IComponentContext componentContext) : this(jsRuntime, componentContext, null)
        { }

        internal PageVisibilityAPIService(IJSRuntime jSRuntime, IComponentContext componentContext, Func<PageVisibilityAPIService, DotNetObjectRef<PageVisibilityAPIService>>? dotNetObjectRefFactory)
        {
            _jsRuntime = jSRuntime;
            _componentContext = componentContext;
            _dotNetObjectRefFactory = dotNetObjectRefFactory ?? CreateDotNetObjectRef;
            Status = PageVisibilityAPIServiceStatus.Initialized;
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
            if (Status != PageVisibilityAPIServiceStatus.Initialized) return;

            Status = PageVisibilityAPIServiceStatus.Subscribing;

            if (_cancellationTokenSource is null || _cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }

            var isConnected = await _componentContext.IsConnectedAsync(_cancellationTokenSource.Token);
            if (isConnected && !_cancellationTokenSource.IsCancellationRequested)
            {
                await _jsRuntime.InvokeAsync<object>("window.bootstrapDotNet.services.pageVisibilityApiInterop.subscribe",
                    new object[] { _dotNetObjectRefFactory(this), nameof(SetVisibilityState) },
                    _cancellationTokenSource.Token);

                Status = PageVisibilityAPIServiceStatus.Subscribed;
            }
            else
            {
                Status = PageVisibilityAPIServiceStatus.Initialized;
            }
        }

        private async void UnsubscribeFromAPI()
        {
            if (Status == PageVisibilityAPIServiceStatus.Subscribing && _cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }
            else if (_componentContext.IsConnected)
            {
                await _jsRuntime.InvokeAsync<object>("window.bootstrapDotNet.services.pageVisibilityApiInterop.unsubscribe");
            }

            Status = PageVisibilityAPIServiceStatus.Initialized;
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

                    _cancellationTokenSource?.Dispose();
                }
                _disposed = true;
                _cancellationTokenSource = null;
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
