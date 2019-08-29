using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities
{
    public static class UrlHashUtility
    {
        public static string GenerateRelativeUrlWithHash(this string href, string currentRelativeUrl)
        {
            var existingHash = currentRelativeUrl.IndexOf('#');
            if (existingHash >= 0)
            {
                currentRelativeUrl = currentRelativeUrl.Substring(0, existingHash);
            }
            return $"{currentRelativeUrl}{href}";
        }
    }
}
