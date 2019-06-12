using Egil.RazorComponents.Bootstrap.Helpers;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap
{
    public abstract class BootstrapComponentBase : ComponentBase
    {
        private static readonly char CssClassSplitChar = ' ';

        [Parameter]
        public string? Id { get; set; }

        [Parameter]
        public string? Class { get; set; }

        internal string? DefaultCssClass { get; set; }

        protected string CssClassValue => BuildCssClassValue();

        private IEnumerable<ICssClassProvider> GetCssClassParameters()
        {
            var properties = GetType().GetPropertiesAssignableFrom<ICssClassProvider>();
            return properties.GetValues<ICssClassProvider>(this);
        }

        private string BuildCssClassValue()
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(DefaultCssClass)) AppendCssClass(DefaultCssClass);

            foreach (var cssProp in GetCssClassParameters())
            {
                foreach (var cssClass in cssProp)
                {
                    AppendCssClass(cssClass);
                }
            }

            if (!string.IsNullOrWhiteSpace(Class)) AppendCssClass(Class);

            RemoveTrailingSpace();

            return builder.ToString();

            void AppendCssClass(string cssClass)
            {
                builder.Append(cssClass);
                builder.Append(CssClassSplitChar);
            }

            void RemoveTrailingSpace()
            {
                if (builder.Length > 0)
                    builder.Remove(builder.Length - 1, 1);
            }
        }
    }
}
