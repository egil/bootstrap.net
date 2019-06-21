using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Badges.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Badges
{
    public sealed class Badge : BootstrapParentComponentBase
    {
        private const string BadgeCssClass = "badge";
        private const string PillShapedCssClass = "badge-pill";

        [Parameter]
        public ColorParameter<BadgeColor> Color { get; set; } = ColorParameter<BadgeColor>.None;

        [Parameter, CssClassToggleParameter(PillShapedCssClass)]
        public bool PillShaped { get; set; }

        public Badge()
        {
            DefaultCssClass = BadgeCssClass;
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            if (AdditionalAttributes.ContainsKey(HtmlAttrs.HREF))
                builder.OpenElement(HtmlTags.A);
            else
                builder.OpenElement(HtmlTags.SPAN);

            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }
}
