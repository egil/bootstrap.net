using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public abstract class BootstrapComponentBase : ComponentBase
    {
        private static readonly Type CssClassProviderType = typeof(ICssClassProvider);
        private static readonly Type BoolType = typeof(bool);
        private static readonly char CssClassSplitChar = ' ';

        private static readonly IReadOnlyDictionary<string, object> EmptyDictionary = new Dictionary<string, object>(0);

        [Parameter(CaptureUnmatchedValues = true)]
        protected internal IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; } = EmptyDictionary;

        [Parameter] public string? Class { get; set; }

        protected internal string DefaultElementName { get; set; } = HtmlTags.DIV;
        protected internal string DefaultCssClass { get; set; } = string.Empty;

        protected internal virtual string CssClassValue => BuildCssClassValue();

        private string BuildCssClassValue()
        {
            var builder = new StringBuilder();

            AppendCssClass(DefaultCssClass);

            var sourceType = GetType();
            var properties = sourceType.GetInstanceProperties();
            foreach (var cssProp in properties.IsAssignableFrom(CssClassProviderType).GetValues<ICssClassProvider>(this))
            {
                foreach (var cssClass in cssProp)
                {
                    AppendCssClass(cssClass);
                }
            }

            foreach (var prop in properties)
            {
                if (prop.DeclaringType == sourceType && prop.PropertyType == BoolType)
                {
                    var cssToggle = prop.GetCustomAttribute<CssClassToggleParameterAttribute>();
                    if (cssToggle is null) continue;
                    var toggledOn = prop.GetValue<bool>(this);
                    var cssClass = cssToggle.GetValue(toggledOn);
                    AppendCssClass(cssClass);
                }
            }

            AppendCssClass(Class);

            RemoveTrailingSpace();

            return builder.ToString();

            void AppendCssClass(string? cssClass)
            {
                if (string.IsNullOrWhiteSpace(cssClass)) return;

                builder.Append(cssClass);
                builder.Append(CssClassSplitChar);
            }

            void RemoveTrailingSpace()
            {
                if (builder.Length > 0)
                    builder.Remove(builder.Length - 1, 1);
            }
        }

        protected internal static string CombineCssClasses(string cssClass, string otherCssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass) && string.IsNullOrWhiteSpace(otherCssClass)) return string.Empty;
            if (!string.IsNullOrWhiteSpace(cssClass) && string.IsNullOrWhiteSpace(otherCssClass)) return cssClass;
            if (string.IsNullOrWhiteSpace(cssClass) && !string.IsNullOrWhiteSpace(otherCssClass)) return otherCssClass;
            return string.Concat(cssClass, CssClassSplitChar, otherCssClass);
        }
    }
}
