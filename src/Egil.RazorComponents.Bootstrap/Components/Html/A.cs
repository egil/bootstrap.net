using Egil.RazorComponents.Bootstrap.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Components.Html
{
    public sealed class A : BootstrapParentComponentBase, IDisposable
    {
        private const string DefaultActiveClass = "active";

        private string? MatchUrlAbsolute { get; set; }

        public bool IsActive { get; private set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; private set; }

        [Parameter]
        public string? Href { get; set; }

        /// <summary>
        /// Gets or sets the CSS class name applied to the NavLink when the 
        /// current route matches the NavLink href.
        /// </summary>
        [Parameter]
        public string? ActiveClass { get; set; } = DefaultActiveClass;

        /// <summary>
        /// Gets or sets a value representing the URL matching behavior.
        /// </summary>
        [Parameter]
        public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;

        /// <summary>
        /// Gets or sets a custom URL, that should be used instead of the URL 
        /// in the href attribute, to match current route and determine if link
        /// is active or not.
        /// </summary>
        [Parameter]
        public string? MatchUrl { get; set; }

        [Inject] private IUriHelper? UriHelper { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            // We'll consider re-rendering on each location change
            UriHelper!.OnLocationChanged += OnLocationChanged;
        }

        protected override void OnParametersSet()
        {
            MatchUrlAbsolute = CreateMatchUrlAbsolute();
            IsActive = ShouldMatch(UriHelper!.GetAbsoluteUri());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // To avoid leaking memory, it's important to detach any event handlers in Dispose()
            UriHelper!.OnLocationChanged -= OnLocationChanged;
        }

        internal RenderFragment? CustomRenderFragment { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var renderFragment = CustomRenderFragment ?? DefaultRenderFragment;
            renderFragment(builder);
        }

        internal void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.A);
            builder.AddClassAttribute(CombineWithSpace(CssClassValue, IsActive ? ActiveClass : null));
            builder.AddAttribute("href", Href);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(4, ChildContent);
            builder.CloseElement();
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            // We could just re-render always, but for this component we know the
            // only relevant state change is to the _isActive property.
            var shouldBeActiveNow = ShouldMatch(args.Location);
            if (shouldBeActiveNow != IsActive)
            {
                IsActive = shouldBeActiveNow;
                StateHasChanged();
            }
        }

        private string? CreateMatchUrlAbsolute()
        {
            if (!string.IsNullOrWhiteSpace(MatchUrl))
                return UriHelper!.ToAbsoluteUri(MatchUrl).AbsoluteUri;
            else if (!string.IsNullOrWhiteSpace(Href))
                return UriHelper!.ToAbsoluteUri(Href).AbsoluteUri;
            else
                return null;
        }

        private bool ShouldMatch(string currentUriAbsolute)
        {
            if (MatchUrlAbsolute is null) return false;

            if (EqualsHrefExactlyOrIfTrailingSlashAdded(currentUriAbsolute, MatchUrlAbsolute))
            {
                return true;
            }

            if (Match == NavLinkMatch.Prefix
                && IsStrictlyPrefixWithSeparator(currentUriAbsolute, MatchUrlAbsolute))
            {
                return true;
            }

            return false;
        }

        private static bool EqualsHrefExactlyOrIfTrailingSlashAdded(string currentUriAbsolute, string matchUrlAbsolute)
        {
            if (string.Equals(currentUriAbsolute, matchUrlAbsolute, StringComparison.Ordinal))
            {
                return true;
            }

            if (currentUriAbsolute.Length == matchUrlAbsolute.Length - 1)
            {
                // Special case: highlight links to http://host/path/ even if you're
                // at http://host/path (with no trailing slash)
                //
                // This is because the router accepts an absolute URI value of "same
                // as base URI but without trailing slash" as equivalent to "base URI",
                // which in turn is because it's common for servers to return the same page
                // for http://host/vdir as they do for host://host/vdir/ as it's no
                // good to display a blank page in that case.
                if (matchUrlAbsolute[matchUrlAbsolute.Length - 1] == '/'
                    && matchUrlAbsolute.StartsWith(currentUriAbsolute, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsStrictlyPrefixWithSeparator(string value, string prefix)
        {
            var prefixLength = prefix.Length;
            if (value.Length > prefixLength)
            {
                return value.StartsWith(prefix, StringComparison.Ordinal)
                    && (
                        // Only match when there's a separator character either at the end of the
                        // prefix or right after it.
                        // Example: "/abc" is treated as a prefix of "/abc/def" but not "/abcdef"
                        // Example: "/abc/" is treated as a prefix of "/abc/def" but not "/abcdef"
                        prefixLength == 0
                        || !char.IsLetterOrDigit(prefix[prefixLength - 1])
                        || !char.IsLetterOrDigit(value[prefixLength])
                    );
            }
            else
            {
                return false;
            }
        }

        private static string CombineWithSpace(string? str1, string? str2)
           => str1 == null && str2 != null ? str2
            : str1 != null && str2 == null ? str1
            : $"{str1} {str2}";
    }
}