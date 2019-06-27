using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Carousels.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Animations.Timers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels
{
    public class Carousel<TItem> : BootstrapContextAwareComponentBase, IDisposable
    {
        private const string DefaultCarouselCssClass = "carousel";
        private const string CarouselInnerCssClass = "carousel-inner";
        private const string DefaultPreviousControlSrText = "Previous";
        private const string DefaultNextControlSrText = "Next";
        private static readonly Type CarouselStaticType = typeof(CarouselStatic);

        private readonly AnimationTimer _changeItemTimer;
        private readonly List<CarouselItem> _carouselItems = new List<CarouselItem>();

        private bool ChangingItems { get; set; }
        private int ItemCount => StaticContentMode ? _carouselItems.Count : Items?.Count ?? 0;
        private bool StaticContentMode { get; } = typeof(TItem) == CarouselStaticType;

        [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }
        [Parameter] public IReadOnlyList<TItem>? Items { get; set; }
        [Parameter] public ushort ActiveIndex { get; set; } = 0;
        [Parameter] public EventCallback<ushort> ActiveIndexChanged { get; set; }
        [Parameter] public AnimationParameter Animation { get; set; } = AnimationParameter.Slide;
        [Parameter] public bool Autoplay { get; set; } = true;
        [Parameter] public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(5);
        [Parameter] public bool PauseOnHover { get; set; } = true;
        [Parameter] public bool KeyboardNavigateable { get; set; } = true;
        [Parameter] public bool ShowControls { get; set; } = false;
        [Parameter] public bool ShowIndicators { get; set; } = false;
        [Parameter] public string PreviousControlSrText { get; set; } = DefaultPreviousControlSrText;
        [Parameter] public string NextControlSrText { get; set; } = DefaultNextControlSrText;

        public Carousel()
        {
            DefaultCssClass = DefaultCarouselCssClass;
            _changeItemTimer = new AnimationTimer(async () => await InvokeAsync(Next));
        }

        public void Cycle()
        {
            if (ItemCount > 0 && !_changeItemTimer.IsRunning)
            {
                _changeItemTimer.Start();
            }
        }

        public void Resume()
        {
            if (_changeItemTimer.IsPaused)
                _changeItemTimer.Resume();
        }

        public void Pause()
        {
            if (_changeItemTimer.IsRunning)
                _changeItemTimer.Pause();
        }

        public void Stop()
        {
            if (_changeItemTimer.IsRunning)
                _changeItemTimer.Stop();
        }

        public async Task Previous()
        {
            if (ItemCount == 0) return;
            await ChangeActiveItem(ActiveIndex, (ActiveIndex - 1 + ItemCount) % ItemCount);
        }

        public async Task Next()
        {
            if (ItemCount == 0) return;
            await ChangeActiveItem(ActiveIndex, (ActiveIndex + 1 + ItemCount) % ItemCount);
        }

        protected override void OnBootstrapParametersSet()
        {
            if (Interval > TimeSpan.Zero)
            {
                _changeItemTimer.DueTime = Interval;
                _changeItemTimer.Interval = Interval;
            }
            if (Autoplay)
            {
                Cycle();
            }
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

            AddPauseOnMouseHover(builder);
            AddKeyboardNavigation(builder);

            builder.AddClassAttribute(CssClassValue);
            builder.AddContent(IndicatorsRenderFragment);
            builder.AddContent(CarouselInnerRenderFragment);
            builder.AddContent(ControlsRenderFragment);
            builder.CloseElement();
        }

        private void AddKeyboardNavigation(RenderTreeBuilder builder)
        {
            if (KeyboardNavigateable)
            {
                // To make the KEYDOWN event fire on a DIV element, it must be focusable. Setting
                // tabindex to -1 makes it focusable, without making it possible to tab to it.
                // See https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/tabindex for details.
                builder.AddTabIndex(-1);

                builder.AddEventListener(HtmlEvents.KEYDOWN, EventCallback.Factory.Create<UIKeyboardEventArgs>(this, async (UIKeyboardEventArgs e) =>
                {
                    switch (e.Code)
                    {
                        case "ArrowLeft": await Previous(); break;
                        case "ArrowRight": await Next(); break;
                        default: break;
                    }
                }));
            }
        }

        private void AddPauseOnMouseHover(RenderTreeBuilder builder)
        {
            if (PauseOnHover && Autoplay || _changeItemTimer.IsRunning)
            {
                builder.AddEventListener(HtmlEvents.MOUSEENTER, EventCallback.Factory.Create<UIMouseEventArgs>(this, Pause));
                builder.AddEventListener(HtmlEvents.MOUSELEAVE, EventCallback.Factory.Create<UIMouseEventArgs>(this, Resume));
            }
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

        private async Task ChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            if (oldActiveIndex == newActiveIndex || ChangingItems) return;

            ChangingItems = true;
            if (!_changeItemTimer.IsStopped) _changeItemTimer.Stop();

            if (Animation == AnimationParameter.None)
                await InstantChangeActiveItem(oldActiveIndex, newActiveIndex);
            else
                await AnimateChangeActiveItem(oldActiveIndex, newActiveIndex);

            ChangingItems = false;
            if (Autoplay) _changeItemTimer.Start();
        }

        private async Task AnimateChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            // This is complex due to the css transition rules used by bootstrap.
            // Here are the steps involved to make it:
            //
            // 1. nextElm gets the orderClassName class set (rendered to DOM).
            //    If the orderClassName class isn't rendered to the DOM a 'tick' before step 2, the 
            //    animation will not work correctly.
            //    If the delay is too long, there will be a gab between the two images as they slide in/out.
            // 
            //    Step 1 updates ActiveIndex, and calls StateHasChanged() to ensure that indicators are updated
            //    immediately.
            //
            // 2. immediately after, both nextElm and activeElm has directionalClassName class set (rendered to DOM)
            //    and the animation/transition in CSS should be in sync (in/out following each other)
            //
            // 3. after 600ms the animation/transition is done in CSS and class can be reset and active set

            var nextElm = _carouselItems[newActiveIndex];
            var activeElm = _carouselItems[oldActiveIndex];

            var directionalClassName = oldActiveIndex < newActiveIndex ? "carousel-item-left" : "carousel-item-right";
            var orderClassName = oldActiveIndex < newActiveIndex ? "carousel-item-next" : "carousel-item-prev";

            // Step 1
            nextElm.Class = orderClassName;
            ActiveIndex = (ushort)newActiveIndex;
            await ActiveIndexChanged.InvokeAsync(ActiveIndex);
            StateHasChanged();

            await Task.Delay(1);
            
            // Step 2
            activeElm.UpdateCssClass(directionalClassName);
            nextElm.UpdateCssClass($"{directionalClassName} {orderClassName}");

            await Task.Delay(600);

            // Step 3
            activeElm.Active = false;
            nextElm.Active = true;

            activeElm.UpdateCssClass(string.Empty);
            nextElm.UpdateCssClass(string.Empty);
        }

        private async Task InstantChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            _carouselItems[oldActiveIndex].Active = false;
            _carouselItems[newActiveIndex].Active = true;

            ActiveIndex = (ushort)newActiveIndex;
            await ActiveIndexChanged.InvokeAsync(ActiveIndex);
            StateHasChanged();
        }

        public void Dispose()
        {
            _changeItemTimer.Dispose();
        }
    }
}
