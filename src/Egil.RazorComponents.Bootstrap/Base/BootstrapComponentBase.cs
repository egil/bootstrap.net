using Egil.RazorComponents.Bootstrap.Base.Context;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public abstract class BootstrapComponentBase : ComponentBase, IBootstrapComponent
    {
        private static readonly Type CssClassProviderType = typeof(ICssClassProvider);
        private static readonly Type BoolType = typeof(bool);
        private static readonly char CssClassSplitChar = ' ';
        private static readonly IReadOnlyDictionary<string, object> EmptyDictionary = new Dictionary<string, object>(0);

        private bool _isFirstRender = true;

        [CascadingParameter] public IBootstrapContext? BootstrapContext { get; private set; }

        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; private set; } = EmptyDictionary;

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

        protected override void OnAfterRender()
        {
            base.OnAfterRender();
            if (_isFirstRender)
            {
                _isFirstRender = false;
                OnAfterFirstRender();
            }
        }

        protected virtual void OnAfterFirstRender() { }

        internal void TriggerRender()
        {
            StateHasChanged();
        }
    }
}
