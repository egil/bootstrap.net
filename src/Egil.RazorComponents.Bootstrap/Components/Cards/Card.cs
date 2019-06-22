using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Common;
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

        protected override void OnRegisterChildContextRules()
        {
            void ImageRule(BootstrapContextAwareComponentBase component)
            {
                component.DefaultCssClass = ImageOverlayed 
                    ? ImgCssClass 
                    : _childComponentCount == 1 ? ImgTopCssClass : ImgBottomCssClass;
            }

            Context.RegisterOnInitRule<Svg>(ImageRule);
            Context.RegisterOnInitRule<Img>(ImageRule);

            Context.RegisterOnInitRule<Header>(header =>
            {
                header.DefaultElementName = HtmlTags.DIV;
                header.DefaultCssClass = HeaderCssClass;
                header.Context.RegisterRule<Nav>(nav => nav.DefaultCssClass = nav.Pills ? HeaderNavPillsCssCass : HeaderNavCssCass);
            });

            Context.RegisterOnInitRule<Footer>(footer => { footer.DefaultElementName = HtmlTags.DIV; footer.DefaultCssClass = FooterCssClass; });
            Context.RegisterOnInitRule<Heading, H1, H2, H3, H4, H5, H6>(heading => { if (_childComponentCount == 1) heading.DefaultCssClass = HeaderCssClass; });

            Context.RegisterOnInitRule<Content>(content =>
            {
                int headingsSeen = 0;

                content.Context.RegisterOnInitRule<Heading, H1, H2, H3, H4, H5, H6>(x =>
                {
                    headingsSeen++;
                    x.DefaultCssClass = headingsSeen == 1 ? TitleCssClass : SubTitleCssClass;
                });

                content.Context.RegisterOnInitRule<A>(x => x.DefaultCssClass = LinkCssClass);
                content.Context.RegisterOnInitRule<P>(x => x.DefaultCssClass = TextCssClass);
            });

            Context.RegisterRule<Content>(content => content.DefaultCssClass = ImageOverlayed ? OverlayImgCssClass : BodyCssClass);

            Context.RegisterOnInitRule<Row>(row =>
            {
                row.NoGutters = true;
                row.Context.CopyRulesFrom(Context);
                row.Context.RegisterOnInitRule<Svg>(x => x.DefaultCssClass = ImgCssClass);
                row.Context.RegisterOnInitRule<Img>(x => x.DefaultCssClass = ImgCssClass);
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