using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Egil.RazorComponents.Bootstrap.Utilities;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    // TODO: Allow user to override/set Active param - update examples in docs
    public sealed class A : ParentComponentBase, IToggleForCollapse
    {
        private ElementReference _domElement;
        private bool _preventDefaultHandlerRegistered;

        #region IToggleForCollapse state

        string[] IToggleForCollapse.SubscribedToggleTargetIds { get; set; } = Array.Empty<string>();
        IEventBus IToggleForCollapse.EventBus => EventBus!;
        EventCallback<UIMouseEventArgs>? IToggleForCollapse.ToggleForClickHandler
        {
            get => ToggleForClickHandler; set => ToggleForClickHandler = value;
        }

        #endregion 

        private EventCallback<UIMouseEventArgs>? ToggleForClickHandler { get; set; }

        private string? MatchUrlAbsolute { get; set; }

        private ICssClassProvider ActiveCssClass { get; set; } = CssClassProviderBase.Empty;

        [Inject] private IComponentContext? ComponentContext { get; set; }
        [Inject] private IUriHelper? UriHelper { get; set; }
        [Inject] private IJSRuntime? JsRuntime { get; set; }
        [Inject] private IEventBus? EventBus { get; set; }

        /// <summary>
        /// Gets whether the link is currently pointing to the active browser URL.
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// Gets or sets the CSS class name applied to the NavLink when the 
        /// current route matches the NavLink href.
        /// </summary>
        [Parameter] public string? ActiveClass { get; set; } = ActiveCssClassProvider.DefaultActiveClass;

        /// <summary>
        /// Gets or sets a value representing the URL matching behavior.
        /// Default is <see cref="NavLinkMatch.All"/>.
        /// </summary>
        [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

        /// <summary>
        /// Gets or sets a custom URL, that should be used instead of the URL 
        /// in the href attribute, to match current route and determine if link
        /// is active or not.
        /// </summary>
        [Parameter] public string? MatchUrl { get; set; }

        /// <summary>
        /// Renders the link as a button using Bootstraps button style.
        /// </summary>
        [Parameter, CssClassToggleParameter("btn")] public bool AsButton { get; set; } = false;

        /// <summary>
        /// Sets the color of the button.
        /// </summary>
        [Parameter] public ColorParameter<ButtonColor>? Color { get; set; }

        /// <summary>
        /// Gets or sets whether to prevent default when a user clicks this link. 
        /// </summary>
        [Parameter] public bool PreventDefaultOnClick { get; set; }

        /// <summary>
        /// Gets or sets the IDs of the <see cref="Collapse"/> components that this 
        /// component should be used to toggle. One or more IDs can be specified 
        /// by separating them with a comma or space.
        /// </summary>
        [Parameter] public string? ToggleFor { get; set; }

        /// <summary>
        /// Gets or sets whether this link should be disabled or not. Default is false.
        /// Used together with <see cref="AsButton"/>.
        /// </summary>
        [Parameter, CssClassToggleParameter("disabled")] public bool Disabled { get; set; } = false;

        public A()
        {
            DefaultElementTag = HtmlTags.A;
            DomElementCapture = (elm) => _domElement = elm;
        }

        protected override void OnCompomnentInit()
        {
            UriHelper!.OnLocationChanged += OnLocationChanged;
            ((IToggleForCollapse)this).AddToggleHooks(this);
        }

        protected override void OnCompomnentParametersSet()
        {
            var newMatchUrl = CreateMatchUrlAbsolute();
            if (newMatchUrl != MatchUrlAbsolute)
            {
                MatchUrlAbsolute = newMatchUrl;
                Active = UriHelper!.CurrentUriMatches(MatchUrlAbsolute, Match);
                SetActiveCssClassProvider();
            }

            if (ToggleForClickHandler.HasValue)
            {
                AddOverride(HtmlEvents.CLICK, JoinEventCallbacks(HtmlEvents.CLICK, ToggleForClickHandler));
            }

            if (Disabled)
            {
                AddOverride(HtmlAttrs.TABINDEX, "-1");
                AddOverride(HtmlAttrs.ARIA_DISABLED, "true");
            }
            else
            {
                RemoveOverride(HtmlAttrs.TABINDEX);
                RemoveOverride(HtmlAttrs.ARIA_DISABLED);
            }

            if (Active && AsButton)
            {
                AddOverride(HtmlAttrs.ARIA_PRESSED, "true");
            }

            // HACK for href="#" urls being redirected to href="/#".
            if (AdditionalAttributes.TryGetValue("href", out string href) && href.StartsWith('#'))
            {
                var relativeUrl = UriHelper!.ToBaseRelativePath(UriHelper.GetBaseUri(), UriHelper.GetAbsoluteUri());
                AddOverride("href", href.GenerateRelativeUrlWithHash(relativeUrl));
            }
            else
            {
                RemoveOverride("href");
            }
        }

        protected override Task OnCompomnentAfterRenderAsync()
        {
            return ApplyPreventDefaultRule();
        }

        protected override void OnCompomnentDispose()
        {
            // To avoid leaking memory, it's important to detach any event handlers in Dispose()
            UriHelper!.OnLocationChanged -= OnLocationChanged;
        }

        private Task ApplyPreventDefaultRule()
        {
            if (!ComponentContext!.IsConnected) return Task.CompletedTask;
            if (!Disabled && !PreventDefaultOnClick && !_preventDefaultHandlerRegistered) return Task.CompletedTask;

            if (Disabled || PreventDefaultOnClick)
            {
                _preventDefaultHandlerRegistered = true;
                return _domElement.PreventDefault(JsRuntime, "click");
            }
            else
            {
                _preventDefaultHandlerRegistered = false;
                return _domElement.AllowDefault(JsRuntime, "click");
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
        {
            // We could just re-render always, but for this component we know the
            // only relevant state change is to the _isActive property.
            var isActiveNow = UriHelper!.CurrentUriMatches(MatchUrlAbsolute, Match);
            if (isActiveNow != Active)
            {
                Active = isActiveNow;
                SetActiveCssClassProvider();
                StateHasChanged();
            }
        }

        private string? CreateMatchUrlAbsolute()
        {
            if (!string.IsNullOrWhiteSpace(MatchUrl))
                return UriHelper!.ToAbsoluteUri(MatchUrl).AbsoluteUri;
            else if (AdditionalAttributes.TryGetValue(HtmlAttrs.HREF, out object? value) && value is string href && !string.IsNullOrWhiteSpace(href))
                return UriHelper!.ToAbsoluteUri(href).AbsoluteUri;
            else
                return null;
        }

        private void SetActiveCssClassProvider()
        {
            if (!Active || ActiveClass is null)
            {
                ActiveCssClass = CssClassProviderBase.Empty;
            }
            else if (ActiveCssClassProvider.IsDefault(ActiveClass))
            {
                ActiveCssClass = ActiveCssClassProvider.Default;
            }
            else
            {
                var currentActiveClass = ActiveCssClass.FirstOrDefault() ?? string.Empty;
                if (!ActiveClass.Equals(currentActiveClass, StringComparison.Ordinal))
                {
                    ActiveCssClass = new ActiveCssClassProvider(ActiveClass);
                }
            }
        }
    }
}