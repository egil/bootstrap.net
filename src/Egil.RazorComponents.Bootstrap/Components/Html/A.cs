using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class A : BootstrapHtmlElementComponentBase, IDisposable
    {
        private string? MatchUrlAbsolute { get; set; }

        private ICssClassProvider ActiveCssClass { get; set; } = CssClassProviderBase.Empty;

        [Inject] private IUriHelper? UriHelper { get; set; }

        public bool Active { get; private set; }

        /// <summary>
        /// Gets or sets the CSS class name applied to the NavLink when the 
        /// current route matches the NavLink href.
        /// </summary>
        [Parameter] public string? ActiveClass { get; set; } = ActiveCssClassProvider.DefaultActiveClass;

        /// <summary>
        /// Gets or sets a value representing the URL matching behavior.
        /// </summary>
        [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;

        /// <summary>
        /// Gets or sets a custom URL, that should be used instead of the URL 
        /// in the href attribute, to match current route and determine if link
        /// is active or not.
        /// </summary>
        [Parameter] public string? MatchUrl { get; set; }

        [Parameter, CssClassToggleParameter("btn")] public bool AsButton { get; set; } = false;

        /// <summary>
        /// Sets the color of the button.
        /// </summary>
        [Parameter]
        public ColorParameter<ButtonColor> Color { get; set; } = ColorParameter<ButtonColor>.None;

        public A()
        {
            DefaultElementName = HtmlTags.A;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // To avoid leaking memory, it's important to detach any event handlers in Dispose()
            UriHelper!.OnLocationChanged -= OnLocationChanged;
        }

        protected override void OnBootstrapInit()
        {
            // We'll consider re-rendering on each location change
            UriHelper!.OnLocationChanged += OnLocationChanged;
        }

        protected override void OnBootstrapParametersSet()
        {
            var newMatchUrl = CreateMatchUrlAbsolute();
            if (newMatchUrl != MatchUrlAbsolute)
            {
                MatchUrlAbsolute = newMatchUrl;
                Active = UriHelper!.CurrentUriMatches(MatchUrlAbsolute, Match);
                SetActiveCssClassProvider();
            }
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
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
            else if (AdditionalAttributes.TryGetValue(HtmlAttrs.HREF, out string href) && !string.IsNullOrWhiteSpace(href))
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