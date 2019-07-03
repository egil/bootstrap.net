using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class A : BootstrapHtmlElementComponentBase, IToggleForCollapse, IDisposable
    {
        private ElementRef _domElement;
        private bool _preventDefaultHandlerRegistered;

        private string? MatchUrlAbsolute { get; set; }

        private ICssClassProvider ActiveCssClass { get; set; } = CssClassProviderBase.Empty;

        [Inject] private IUriHelper? UriHelper { get; set; }

        [Inject] private IJSRuntime? JsRuntime { get; set; }

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

        /// <summary>
        /// Renders the link as a button using Bootstraps button style.
        /// </summary>
        [Parameter, CssClassToggleParameter("btn")] public bool AsButton { get; set; } = false;

        /// <summary>
        /// Sets the color of the button.
        /// </summary>
        [Parameter] public ColorParameter<ButtonColor> Color { get; set; } = ColorParameter<ButtonColor>.None;

        [Parameter] public bool PreventDefaultOnClick { get; set; }

        #region IToggleForCollapse

        private bool _isToggleTargetExpanded;
        private string? _ariaControls;
        public event EventHandler OnToggled;

        [Parameter] public string? ToggleFor { get; set; }

        void IToggleForCollapse.SetExpandedState(bool isExpanded)
        {
            _isToggleTargetExpanded = isExpanded;
            StateHasChanged();
        }

        #endregion

        public A()
        {
            DefaultElementName = HtmlTags.A;
        }

        protected override void OnBootstrapInit()
        {
            // We'll consider re-rendering on each location change
            UriHelper!.OnLocationChanged += OnLocationChanged;
                        
            IToggleForCollapse.Connect(this);
            _ariaControls = string.Join(' ', ToggleFor?.SplitOnComma() ?? Array.Empty<string>());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // To avoid leaking memory, it's important to detach any event handlers in Dispose()
            UriHelper!.OnLocationChanged -= OnLocationChanged;
            IToggleForCollapse.Disconnect(this);
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementName);
            
            if (!(OnToggled is null))
            {
                builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, (e) => OnToggled?.Invoke(this, EventArgs.Empty)));
            }
            
            if (!(ToggleFor is null))
            {
                builder.AddAttribute(HtmlAttrs.ARIA_EXPANDED, _isToggleTargetExpanded.ToLowerCaseString());
                builder.AddAttribute(HtmlAttrs.ARIA_CONTROLS, _ariaControls);
                builder.AddRoleAttribute("button");
            }

            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.AddElementReferenceCapture(elm => _domElement = elm);
            builder.CloseElement();
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

        protected override async Task OnAfterRenderAsync()
        {
            await base.OnAfterRenderAsync();

            await ApplyPreventDefaultRule();
        }

        private async Task ApplyPreventDefaultRule()
        {
            if (!PreventDefaultOnClick && !_preventDefaultHandlerRegistered) return;

            if (PreventDefaultOnClick)
            {
                await _domElement.PreventDefault(JsRuntime, "click");
                _preventDefaultHandlerRegistered = true;
            }
            else
            {
                await _domElement.AllowDefault(JsRuntime, "click");
                _preventDefaultHandlerRegistered = false;
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
            else if (AdditionalAttributes.TryGetValue(HtmlAttrs.HREF, out object value) && value is string href && !string.IsNullOrWhiteSpace(href))
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