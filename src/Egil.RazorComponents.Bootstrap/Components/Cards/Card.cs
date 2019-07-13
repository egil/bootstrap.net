using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Layout;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public sealed class Card : ParentComponentBase, IChildTrackingParentComponent
    {
        private const string DefaultCardCssClass = "card";
        private const string BodyCssClass = "card-body";
        private const string OverlayImgCssClass = "card-img-overlay";
        private const string TitleCssClass = "card-title";
        private const string SubTitleCssClass = "card-subtitle";
        private const string LinkCssClass = "card-link";
        private const string TextCssClass = "card-text";
        private const string ImgCssClass = "card-img";
        private const string ImgTopCssClass = "card-img-top";
        private const string ImgBottomCssClass = "card-img-bottom";
        private const string HeaderCssClass = "card-header";
        private const string FooterCssClass = "card-footer";
        private const string HeaderNavCssCass = "nav nav-tabs card-header-tabs";
        private const string HeaderNavPillsCssCass = "nav nav-pills card-header-pills";
        private int _childComponentCount = 0;
        private int _contentHeadingCount = 0;

        /// <summary>
        /// Gets or sets the width of the component using standard Bootstrap
        /// Widths: 25, 50, 70, 100.
        /// <see cref="https://bootstrap.egilhansen.com/docs/4.3/utilities/sizing/"/>
        /// </summary>
        [Parameter] public NumericSizeParameter<WidthSizePrefix> Width { get; set; } = NumericSizeParameter<WidthSizePrefix>.None;

        [Parameter] public bool ImageOverlayed { get; set; } = false;

        public Card()
        {
            DefaultCssClass = DefaultCardCssClass;
        }

        protected override void ApplyChildHooks(ComponentBase component)
        {
            switch (component)
            {
                case Img image: image.OnParametersSetHook = SetImageCssClass; break;
                case Svg image: image.OnParametersSetHook = SetImageCssClass; break;
                case Header header:
                    header.DefaultElementTag = HtmlTags.DIV;
                    header.DefaultCssClass = HeaderCssClass;
                    header.CustomChildHooksInjector = ApplyChildHooks;
                    break;
                case Nav nav:
                    nav.DefaultCssClass = nav.Pills ? HeaderNavPillsCssCass : HeaderNavCssCass;
                    break;
                case Footer footer:
                    footer.DefaultElementTag = HtmlTags.DIV;
                    footer.DefaultCssClass = FooterCssClass;
                    break;
                case Heading heading when _childComponentCount == 0:
                    heading.DefaultCssClass = HeaderCssClass;
                    break;
                case Content content:
                    content.OnParametersSetHook = _ => content.DefaultCssClass = ImageOverlayed ? OverlayImgCssClass : BodyCssClass;
                    content.CustomChildHooksInjector = CustomContentChildHooks;
                    break;
                case Row row:
                    row.NoGutters = true;
                    row.CustomChildHooksInjector = (c) => ((Column)c).CustomChildHooksInjector = CustomColumnChildHooks;
                    break;
                default: break;
            }
        }

        private void SetImageCssClass(ComponentBase component)
        {
            component.DefaultCssClass = ImageOverlayed
                ? ImgCssClass
                : _childComponentCount == 1
                    ? ImgTopCssClass
                    : ImgBottomCssClass;
        }

        private void CustomContentChildHooks(ComponentBase component)
        {
            switch (component)
            {
                case Heading heading:
                    _contentHeadingCount++;
                    heading.DefaultCssClass = _contentHeadingCount == 1 ? TitleCssClass : SubTitleCssClass;
                    break;
                case A a:
                    a.DefaultCssClass = LinkCssClass;
                    break;
                case P p:
                    p.DefaultCssClass = TextCssClass;
                    break;
                default: break;
            }
        }

        private void CustomColumnChildHooks(ComponentBase component)
        {
            switch (component)
            {
                case Img image:
                    image.DefaultCssClass = ImgCssClass;
                    break;
                case Svg image:
                    image.DefaultCssClass = ImgCssClass;
                    break;
                default:
                    ApplyChildHooks(component);
                    break;
            }
        }

        void IChildTrackingParentComponent.AddChild(ComponentBase component) => _childComponentCount++;

        void IChildTrackingParentComponent.RemoveChild(ComponentBase component) => _childComponentCount--;
    }
}