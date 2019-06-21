using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class UriHelperExtensions
    {
        public static bool CurrentUriMatches(this IUriHelper uriHelper, string? otherUri, NavLinkMatch matchKind)
        {
            if (otherUri is null) return false;
            var otherUrlAbsolute = uriHelper.ToAbsoluteUri(otherUri).AbsoluteUri;
            var currentUriAbsolute = uriHelper.GetAbsoluteUri();

            if (EqualsHrefExactlyOrIfTrailingSlashAdded(currentUriAbsolute, otherUrlAbsolute))
            {
                return true;
            }

            if (matchKind == NavLinkMatch.Prefix && IsStrictlyPrefixWithSeparator(currentUriAbsolute, otherUrlAbsolute))
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
    }
}
