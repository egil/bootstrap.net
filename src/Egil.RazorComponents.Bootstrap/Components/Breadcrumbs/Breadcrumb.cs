using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Routing;

namespace Egil.RazorComponents.Bootstrap.Components.Breadcrumbs
{
    public sealed class Breadcrumb : BootstrapParentComponentBase
    {
        private const string BreadcrumbCssClass = "breadcrumb";
        private const string DefaultAriaLabel = "breadcrumb";
        private const string BreadcrumbItemCssClass = "breadcrumb-item";

        private string? SeparatorOverrideCss { get; set; }

        private CssClassValueProvider? CssScope { get; set; }

        [Parameter]
        public string? Separator { get; set; }

        protected override void OnBootstrapParametersSet()
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

        protected override void OnRegisterChildContextRules()
        {
            Context.RegisterOnInitRule<A>(a =>
            {
                a.CustomRenderFragment = BreadcrumpItemRenderFragment;
                a.Match = NavLinkMatch.All;

                void BreadcrumpItemRenderFragment(RenderTreeBuilder builder)
                {
                    builder.OpenElement(HtmlTags.LI);
                    builder.AddClassAttribute(CombineCssClasses(BreadcrumbItemCssClass, a.CssClassValue));
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
            });
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.AddContent(OverrideSeparatorRenderFragment);

            builder.OpenElement(HtmlTags.NAV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            if (!AdditionalAttributes.ContainsKey(HtmlAttrs.ARIA_LABEL)) builder.AddAriaLabelAttribute(DefaultAriaLabel);

            builder.OpenElement(HtmlTags.OL);
            builder.AddClassAttribute(BreadcrumbCssClass);
            builder.AddContent(ChildContent);
            builder.CloseElement();

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
