using System;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels
{
    public sealed class CarouselItem : ParentComponentBase
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

        internal void UpdateCssClass(string value)
        {
            Class = value;
            StateHasChanged();
        }

        protected override void ApplyChildHooks(ComponentBase component)
        {
            switch (component)
            {
                case Img img:
                    img.DefaultCssClass = CarouselImageCssClass; break;
                case Svg svg:
                    svg.DefaultCssClass = CarouselImageCssClass;
                    break;
                case Caption caption:
                    caption.DefaultCssClass = CarouselCaptionCssClass;
                    caption.DefaultElementTag = HtmlTags.DIV;
                    break;
                default: break;
            }
        }
    }
}
