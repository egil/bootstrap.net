using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Layout;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public sealed class Card : BootstrapParentComponentBase
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
        private int _childComponentCount;

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

        protected override void OnChildInit(BootstrapContextAwareComponentBase component)
        {
            _childComponentCount += 1;
        }

        protected override void OnRegisterChildRules()
        {
            void ImageRule(BootstrapContextAwareComponentBase component)
            {
                component.DefaultCssClass = ImageOverlayed 
                    ? ImgCssClass 
                    : _childComponentCount == 1 ? ImgTopCssClass : ImgBottomCssClass;
            }

            Rules.RegisterOnInitRule<Svg>(ImageRule);
            Rules.RegisterOnInitRule<Img>(ImageRule);

            Rules.RegisterOnInitRule<Header>(header =>
            {
                header.DefaultElementName = HtmlTags.DIV;
                header.DefaultCssClass = HeaderCssClass;
                header.Rules.RegisterRule<Nav>(nav => nav.DefaultCssClass = nav.Pills ? HeaderNavPillsCssCass : HeaderNavCssCass);
            });

            Rules.RegisterOnInitRule<Footer>(footer => { footer.DefaultElementName = HtmlTags.DIV; footer.DefaultCssClass = FooterCssClass; });
            Rules.RegisterOnInitRule<Heading, H1, H2, H3, H4, H5, H6>(heading => { if (_childComponentCount == 1) heading.DefaultCssClass = HeaderCssClass; });

            Rules.RegisterOnInitRule<Content>(content =>
            {
                int headingsSeen = 0;

                content.Rules.RegisterOnInitRule<Heading, H1, H2, H3, H4, H5, H6>(x =>
                {
                    headingsSeen++;
                    x.DefaultCssClass = headingsSeen == 1 ? TitleCssClass : SubTitleCssClass;
                });

                content.Rules.RegisterOnInitRule<A>(x => x.DefaultCssClass = LinkCssClass);
                content.Rules.RegisterOnInitRule<P>(x => x.DefaultCssClass = TextCssClass);
            });

            Rules.RegisterRule<Content>(content => content.DefaultCssClass = ImageOverlayed ? OverlayImgCssClass : BodyCssClass);

            Rules.RegisterOnInitRule<Row>(row =>
            {
                row.NoGutters = true;
                row.Rules.CopyRulesFrom(Rules);
                row.Rules.RegisterOnInitRule<Svg>(x => x.DefaultCssClass = ImgCssClass);
                row.Rules.RegisterOnInitRule<Img>(x => x.DefaultCssClass = ImgCssClass);
            });
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }
}