using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Carousels.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels
{
    public class Carousel<TItem> : BootstrapContextAwareComponentBase
    {
        private const string DefaultCarouselCssClass = "carousel";
        private const string CarouselInnerCssClass = "carousel-inner";
        private const string DefaultPreviousControlSrText = "Previous";
        private const string DefaultNextControlSrText = "Next";
        private static readonly Type CarouselStaticType = typeof(CarouselStatic);

        private readonly List<CarouselItem> _carouselItems = new List<CarouselItem>();

        private bool ChangingItems { get; set; }
        private int ItemCount => StaticContentMode ? _carouselItems.Count : Items?.Count ?? 0;
        private bool StaticContentMode { get; } = typeof(TItem) == CarouselStaticType;

        [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }
        [Parameter] public IReadOnlyList<TItem>? Items { get; set; }
        [Parameter] public ushort ActiveIndex { get; set; } = 0;
        [Parameter] public EventCallback<ushort> ActiveIndexChanged { get; set; }
        [Parameter] public AnimationParameter Animation { get; set; } = AnimationParameter.Slide;
        [Parameter] public bool ShowControls { get; set; } = false;
        [Parameter] public bool ShowIndicators { get; set; } = false;
        [Parameter] public string PreviousControlSrText { get; set; } = DefaultPreviousControlSrText;
        [Parameter] public string NextControlSrText { get; set; } = DefaultNextControlSrText;

        public Carousel()
        {
            DefaultCssClass = DefaultCarouselCssClass;
        }

        public void Previous() => ChangeActiveItem(ActiveIndex, (ActiveIndex - 1 + ItemCount) % ItemCount);

        public void Next() => ChangeActiveItem(ActiveIndex, (ActiveIndex + 1 + ItemCount) % ItemCount);

        public void Goto(ushort index)
        {
            if (index >= ItemCount) throw new ArgumentOutOfRangeException(nameof(index), index, $"Specify a zero-based index, that is not higher than the total number of items in the Carousel.");
            ChangeActiveItem(ActiveIndex, index);
        }

        protected override void OnChildInit(BootstrapContextAwareComponentBase component)
        {
            if (component is CarouselItem item)
            {
                item.Active = ActiveIndex == _carouselItems.Count;
                _carouselItems.Add(item);
                if (StaticContentMode) StateHasChanged();
            }
            else
            {
                throw new InvalidChildContentException($"When using a {nameof(Carousel<TItem>)} in static content mode, " +
                    $"where no {nameof(Items)} is provided and {nameof(TItem)} is set to {nameof(CarouselStatic)}, " +
                    $"all immediate children of the Carousel component must be a {nameof(CarouselItem)} component.");
            }
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddContent(IndicatorsRenderFragment);
            builder.AddContent(CarouselInnerRenderFragment);
            builder.AddContent(ControlsRenderFragment);
            builder.CloseElement();
        }

        private void CarouselInnerRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CarouselInnerCssClass);

            builder.OpenComponent<CascadingValue<BootstrapContextAwareComponentBase>>();
            builder.AddAttribute("Value", this);
            builder.AddAttribute("IsFixed", true);
            builder.AddAttribute(RenderTreeBuilder.ChildContent, (RenderFragment)ItemsRenderFragment);
            builder.CloseComponent();

            builder.CloseElement();
        }

        private void ItemsRenderFragment(RenderTreeBuilder builder)
        {
            //if (Items is null || Items.Count == 0) return;
            if (StaticContentMode && !(ChildContent is null))
            {
                RenderFragment fragment = ChildContent(default!);
                builder.AddContent(fragment);
            }
            else
            {
                for (int index = 0; index < Items?.Count; index++)
                {
                    var item = Items[index];

                    builder.OpenComponent<CarouselItem>();

                    if (!(ChildContent is null))
                    {
                        builder.AddAttribute(RenderTreeBuilder.ChildContent, ChildContent(item));
                    }
                    else if (item is string imageUrl)
                    {
                        builder.AddAttribute(RenderTreeBuilder.ChildContent, ImageRenderTemplate(imageUrl));
                    }
                    else if (item is ValueTuple<string, string> tuple)
                    {
                        builder.AddAttribute(RenderTreeBuilder.ChildContent, ImageRenderTemplate(tuple.Item1, tuple.Item2));
                    }

                    builder.CloseComponent();
                }
            }
        }

        private void ControlsRenderFragment(RenderTreeBuilder builder)
        {
            if (!ShowControls) return;

            builder.OpenElement(HtmlTags.A);
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Previous));
            builder.AddClassAttribute("carousel-control-prev");
            builder.AddRoleAttribute("button");
            builder.AddMarkupContent(@"<span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>");
            builder.AddMarkupContent($@"<span class=""sr-only"">{PreviousControlSrText}</span>");
            builder.CloseElement();

            builder.OpenElement(HtmlTags.A);
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, Next));
            builder.AddClassAttribute("carousel-control-next");
            builder.AddRoleAttribute("button");
            builder.AddMarkupContent(@"<span class=""carousel-control-next-icon"" aria-hidden=""true""></span>");
            builder.AddMarkupContent($@"<span class=""sr-only"">{NextControlSrText}</span>");
            builder.CloseElement();
        }

        private void IndicatorsRenderFragment(RenderTreeBuilder builder)
        {
            if (!ShowIndicators) return;

            builder.OpenElement(HtmlTags.OL);
            builder.AddClassAttribute("carousel-indicators");
            for (int i = 0; i < ItemCount; i++)
            {
                var itemIndex = i;
                builder.OpenElement(HtmlTags.LI);
                builder.AddEventListener(HtmlEvents.CLICK,
                    EventCallback.Factory.Create<UIMouseEventArgs>(this,
                        () => ChangeActiveItem(ActiveIndex, itemIndex)
                    )
                );
                if (ActiveIndex == itemIndex) builder.AddClassAttribute("active");
                builder.CloseElement();
            }
            builder.CloseElement();
        }

        private RenderFragment ImageRenderTemplate(string imageUrl, string? altText = null)
        {
            return builder =>
            {
                builder.OpenComponent<Img>();
                builder.AddAttribute(HtmlAttrs.SRC, imageUrl);
                if (!(altText is null)) builder.AddAttribute(HtmlAttrs.ALT, altText);
                builder.CloseComponent();
            };
        }

        private async void ChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            if (oldActiveIndex == newActiveIndex) return;

            //if (isCycling)
            //{
            //    this.pause()
            //}
            if (Animation == AnimationParameter.None)
            {
                await InstantChangeActiveItem(oldActiveIndex, newActiveIndex);
            }
            else
            {
                await AnimateChangeActiveItem(oldActiveIndex, newActiveIndex);
            }
            //if (isCycling)
            //{
            //    this.cycle()
            //}
        }

        private async Task AnimateChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            if (ChangingItems) return;
            ChangingItems = true;

            var nextElm = _carouselItems[newActiveIndex];
            var activeElm = _carouselItems[oldActiveIndex];

            var directionalClassName = oldActiveIndex < newActiveIndex ? "carousel-item-left" : "carousel-item-right";
            var orderClassName = oldActiveIndex < newActiveIndex ? "carousel-item-next" : "carousel-item-prev";

            nextElm.Class = orderClassName;
            ActiveIndex = (ushort)newActiveIndex;
            await ActiveIndexChanged.InvokeAsync(ActiveIndex);

            await Task.Yield();

            activeElm.UpdateCssClass(directionalClassName);
            nextElm.UpdateCssClass($"{directionalClassName} {orderClassName}");

            await Task.Delay(600);

            activeElm.Active = false;
            nextElm.Active = true;

            activeElm.UpdateCssClass(string.Empty);
            nextElm.UpdateCssClass(string.Empty);

            ChangingItems = false;
        }

        private async Task InstantChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            _carouselItems[oldActiveIndex].Active = false;
            _carouselItems[newActiveIndex].Active = true;

            ActiveIndex = (ushort)newActiveIndex;

            await ActiveIndexChanged.InvokeAsync(ActiveIndex);
        }
    }
}
