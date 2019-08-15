using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;

namespace Egil.RazorComponents.Bootstrap.Components.Breadcrumbs
{
    public sealed class Breadcrumb : ParentComponentBase
    {
        private const string BreadcrumbCssClass = "breadcrumb";
        private const string DefaultAriaLabel = "breadcrumb";
        private const string BreadcrumbItemCssClass = "breadcrumb-item";

        private string? SeparatorOverrideCss { get; set; }

        private CssClassValueProvider? CssScope { get; set; }

        [Parameter]
        public string? Separator { get; set; }

        protected override void OnCompomnentParametersSet()
        {
            if (Separator is null) return;
            var isSeparatorDataUrl = Separator.StartsWith("url(data:");
            CssScope = new CssClassValueProvider("breadcrumb-" + Guid.NewGuid().ToString());

            // TODO BUG: The new separator is not visible at first render - flashes 
            SeparatorOverrideCss = string.Concat(
                ".", CssScope.Value,
                " .breadcrumb-item+.breadcrumb-item::before{content:",
                isSeparatorDataUrl ? "" : "\"",
                Separator,
                isSeparatorDataUrl ? "" : "\"",
                ";}");
        }
        
        protected override void ApplyChildHooks(ComponentBase component)
        {
            if (component is A a)
            {
                a.CustomRenderFragment = BreadcrumpItemRenderFragment;
                a.Match = NavLinkMatch.All;

                void BreadcrumpItemRenderFragment(RenderTreeBuilder builder)
                {
                    builder.OpenElement(HtmlTags.LI);
                    builder.AddClassAttribute(BreadcrumbItemCssClass.CombineCssClassWith(a.CssClassValue));
                    if (a.Active)
                    {
                        builder.AddMultipleAttributes(a.AdditionalAttributes);
                        builder.AddContent(a.ChildContent);
                    }
                    else
                    {
                        builder.AddContent(a.DefaultRenderFragment);
                    }
                    builder.CloseElement();
                }
            }
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.AddContent(OverrideSeparatorRenderFragment);

            builder.OpenElement(HtmlTags.NAV);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(OverriddenAttributes);

            if (!AdditionalAttributes.ContainsKey(HtmlAttrs.ARIA_LABEL) && !OverriddenAttributes.ContainsKey(HtmlAttrs.ARIA_LABEL))
                builder.AddAriaLabelAttribute(DefaultAriaLabel);

            builder.OpenElement(HtmlTags.OL);
            builder.AddClassAttribute(BreadcrumbCssClass);
            builder.AddContent(ChildContent);
            builder.CloseElement();
            builder.AddElementReferenceCapture(DomElementCapture);
            builder.CloseElement();
        }

        private void OverrideSeparatorRenderFragment(RenderTreeBuilder builder)
        {
            if (SeparatorOverrideCss is null) return;
            builder.OpenElement(HtmlTags.STYLE);
            builder.AddContent(SeparatorOverrideCss);
            builder.CloseElement();
        }
    }
}
