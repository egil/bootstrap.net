using System;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels
{
    public sealed class CarouselItem : BootstrapParentComponentBase
    {
        private const string CarouselItemCssClass = "carousel-item";
        private const string CarouselItemActiveCssClass = "active";
        private const string CarouselImageCssClass = "d-block w-100";
        private const string CarouselCaptionCssClass = "carousel-caption";

        [CssClassToggleParameter(CarouselItemActiveCssClass)] internal bool Active { get; set; }
        public bool AnimationActive { get; set; }

        public CarouselItem()
        {
            DefaultCssClass = CarouselItemCssClass;
        }

        public void UpdateCssClass(string value)
        {
            Class = value;
            StateHasChanged();
        }

        protected override void OnRegisterChildRules()
        {
            Rules.RegisterOnInitRule<Img>(img =>
            {
                img.DefaultCssClass = CarouselImageCssClass;
            });
            Rules.RegisterOnInitRule<Svg>(svg =>
            {
                svg.DefaultCssClass = CarouselImageCssClass;
            });
            Rules.RegisterOnInitRule<Caption>(caption =>
            {
                caption.DefaultCssClass = CarouselCaptionCssClass;
                caption.DefaultElementName = HtmlTags.DIV;
            });
        }
    }
}
