using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Extensions;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public static class ComponentCssClassExtensions
    {
        private static readonly Type CssClassProviderType = typeof(ICssClassProvider);
        private static readonly Type BoolType = typeof(bool);
        private static readonly char CssClassSplitChar = ' ';

        internal static string BuildCssClass(this ComponentBase component)
        {
            var builder = new StringBuilder();

            AppendCssClass(component.DefaultCssClass);

            var sourceType = component.GetType();
            var properties = sourceType.GetInstanceProperties().RemoveCssClassExcluded().ToArray();
            foreach (var cssProp in properties.IsAssignableFrom(CssClassProviderType).GetValues<ICssClassProvider>(component))
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
                    var toggledOn = prop.GetValue<bool>(component);
                    var cssClass = cssToggle.GetValue(toggledOn);
                    AppendCssClass(cssClass);
                }
            }

            AppendCssClass(component.Class);

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

        internal static string CombineCssClassWith(this string cssClass, string otherCssClass)
        {
            if (string.IsNullOrWhiteSpace(cssClass) && string.IsNullOrWhiteSpace(otherCssClass)) return string.Empty;
            if (!string.IsNullOrWhiteSpace(cssClass) && string.IsNullOrWhiteSpace(otherCssClass)) return cssClass;
            if (string.IsNullOrWhiteSpace(cssClass) && !string.IsNullOrWhiteSpace(otherCssClass)) return otherCssClass;
            return string.Concat(cssClass, CssClassSplitChar, otherCssClass);
        }
    }

}
