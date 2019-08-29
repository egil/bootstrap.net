using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.PointerEvents;
using Egil.RazorComponents.Bootstrap.Components.Carousels.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI;
using Egil.RazorComponents.Bootstrap.Utilities.Animations.Timers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Carousels
{
    public sealed class Carousel<TItem> : ComponentBase, IChildTrackingParentComponent
    {
        private const string DefaultCarouselCssClass = "carousel";
        private const string CarouselInnerCssClass = "carousel-inner";
        private const string DefaultPreviousControlSrText = "Previous";
        private const string DefaultNextControlSrText = "Next";
        private static readonly Type CarouselStaticType = typeof(CarouselStatic);

        private readonly AnimationTimer _changeItemTimer;
        private HorizontalSwipePointerEventDetector? _swipeDetector;
        private readonly List<CarouselItem> _carouselItems = new List<CarouselItem>();

        private bool ChangingItems { get; set; }
        private bool StaticContentMode { get; } = typeof(TItem) == CarouselStaticType;

        [Inject] private IPageVisibilityAPI? PageVisibilityAPI { get; set; }

        /// <summary>
        /// Gets the total number of items in the carousel.
        /// </summary>
        public int Count => StaticContentMode ? _carouselItems.Count : Items?.Count ?? 0;

        /// <summary>
        /// Gets whether the carousel is currently automatically cycling.
        /// </summary>
        public bool Cycling => _changeItemTimer.IsRunning;

        [Parameter] public RenderFragment<TItem>? ChildContent { get; set; }

        [Parameter] public IReadOnlyList<TItem>? Items { get; set; }

        /// <summary>
        /// Gets or sets the current active index of carousel.
        /// </summary>
        [Parameter] public ushort ActiveIndex { get; set; } = 0;

        [Parameter] public EventCallback<ushort> ActiveIndexChanged { get; set; }

        /// <summary>
        /// Gets or sets the item change animation. 
        /// <list type="bullet">
        ///   <listheader>
        ///     <term>slide</term>
        ///     <description>*Default: Uses a 0.6 second slide animation between items.</description>
        ///   </listheader>
        ///   <item>
        ///     <term>fade</term>
        ///     <description>A 0.6s fading animation between items.</description>
        ///   </item>
        ///   <item>
        ///     <term>none</term>
        ///     <description>An instant change between items.</description>
        ///   </item>
        /// </list>
        /// </summary>
        [Parameter] public AnimationParameter Animation { get; set; } = AnimationParameter.Slide;

        /// <summary>
        /// Gets or sets whether the carousel will automatically cycle to the next item after the specified <see cref="Interval"/>.
        /// </summary>
        [Parameter] public bool Autoplay { get; set; } = true;

        /// <summary>
        /// Gets or sets the amount of time to delay between automatically cycling an item.
        /// Default is five seconds.
        /// </summary>
        [Parameter] public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Gets or sets whether the carousel should cycle continuously or stop <see cref="Autoplay"/> at the last item.
        /// </summary>
        [Parameter] public bool Wrap { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the carousel should continue <see cref="Autoplay"/> after the user manually cycles the an item.
        /// </summary>
        [Parameter] public bool Ride { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the carousel pauses <see cref="Autoplay"/> when the user hovers a pointer over the carousel.
        /// </summary>
        [Parameter] public bool PauseOnHover { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the carousel should react to keyboard events.
        /// Default is true.
        /// </summary>
        [Parameter] public bool EnableKeyboard { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the carousel should support left/right swipe interactions on touchscreen devices.
        /// Default is true.
        /// </summary>
        [Parameter] public bool EnableTouch { get; set; } = true;

        /// <summary>
        /// Gets or sets whether Next/Previous controls should be rendered.
        /// </summary>
        [Parameter] public bool ShowControls { get; set; } = false;

        /// <summary>
        /// Gets or sets whether indicators should be rendered.
        /// </summary>
        [Parameter] public bool ShowIndicators { get; set; } = false;

        /// <summary>
        /// Gets or sets the screen-reader only text for the Previous control.
        /// </summary>
        [Parameter] public string PreviousControlSrText { get; set; } = DefaultPreviousControlSrText;

        /// <summary>
        /// Gets or sets the screen-reader only text for the Next control.
        /// </summary>
        [Parameter] public string NextControlSrText { get; set; } = DefaultNextControlSrText;

        public Carousel()
        {
            DefaultCssClass = DefaultCarouselCssClass;
            _changeItemTimer = new AnimationTimer(() => InvokeAsync(AutoplayNext));
        }

        /// <summary>
        /// Starts the automatic cycling of items in the carousel based on the specified <see cref="Interval"/>.
        /// </summary>
        public void Cycle()
        {
            if (Count > 1 && !_changeItemTimer.IsRunning)
            {
                _changeItemTimer.Start();
            }
        }

        /// <summary>
        /// Resumes the automatic cycling of the carousel that has been paused (<see cref="Pause"/>).
        /// </summary>
        public void Resume()
        {
            if (_changeItemTimer.IsPaused)
                _changeItemTimer.Resume();
        }

        /// <summary>
        /// Pauses the automatic cycling of the carousel.
        /// </summary>
        public void Pause()
        {
            if (_changeItemTimer.IsRunning)
                _changeItemTimer.Pause();
        }

        /// <summary>
        /// Stops the automatic cycling of the carousel.
        /// </summary>
        public void Stop()
        {
            if (_changeItemTimer.IsRunning)
                _changeItemTimer.Stop();
        }

        /// <summary>
        /// Changes the active index to another item. Zero based indexing is used.
        /// </summary>
        /// <param name="index">A zero based index</param>
        /// <returns></returns>
        public Task GoTo(ushort index)
        {
            if (index >= Count) throw new ArgumentOutOfRangeException(nameof(index), "Index cannot be higher than the number of items in the carousel.");
            return ChangeActiveItem(ActiveIndex, index);
        }

        /// <summary>
        /// Changes the active index to another item. Zero based indexing is used.
        /// </summary>
        /// <param name="index">A zero based index</param>
        /// <returns></returns>
        public Task GoTo(object index)
        {
            return GoTo(ushort.Parse(index?.ToString() ?? string.Empty, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Sets the previous item as the active in the carousel.
        /// </summary>
        /// <returns></returns>
        public Task Previous()
        {
            if (Count < 2) return Task.CompletedTask;
            return ChangeActiveItem(ActiveIndex, (ActiveIndex - 1 + Count) % Count);
        }

        /// <summary>
        /// Sets the next item as the active in the carousel.
        /// </summary>
        /// <returns></returns>
        public Task Next()
        {
            if (Count < 2) return Task.CompletedTask;
            return ChangeActiveItem(ActiveIndex, (ActiveIndex + 1 + Count) % Count);
        }

        private Task UserNext()
        {
            Autoplay = Ride;
            return Next();
        }

        private Task UserPrevious()
        {
            Autoplay = Ride;
            return Previous();
        }

        private Task UserGoTo(ushort index)
        {
            Autoplay = Ride;
            return GoTo(index);
        }

        private Task AutoplayNext()
        {
            if (!Wrap && ActiveIndex + 1 == Count)
            {
                _changeItemTimer.Stop();
                return Task.CompletedTask;
            }
            return Next();
        }

        protected override void OnCompomnentAfterFirstRender()
        {
            if (!(PageVisibilityAPI is null))
                PageVisibilityAPI.OnPageVisibilityChanged += PageVisibilityAPI_OnPageVisibilityChanged;
        }

        protected override void OnCompomnentParametersSet()
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

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);

            AddPauseOnMouseHover(builder);
            AddKeyboardNavigation(builder);
            AddTouchNavigation(builder);

            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(OverriddenAttributes);

            builder.AddContent(IndicatorsRenderFragment);
            builder.AddContent(CarouselInnerRenderFragment);
            builder.AddContent(ControlsRenderFragment);

            builder.AddElementReferenceCapture(DomElementCapture);
            builder.CloseElement();
        }

        private void AddPauseOnMouseHover(RenderTreeBuilder builder)
        {
            if (Count > 1 && PauseOnHover && (Autoplay || Cycling))
            {
                builder.AddEventListener(HtmlEvents.MOUSEENTER, EventCallback.Factory.Create<UIMouseEventArgs>(this, Pause));
                builder.AddEventListener(HtmlEvents.MOUSELEAVE, EventCallback.Factory.Create<UIMouseEventArgs>(this, Resume));
            }
        }

        private void AddKeyboardNavigation(RenderTreeBuilder builder)
        {
            if (!EnableKeyboard || Count < 2) return;

            // To make the KEYDOWN event fire on a DIV element, it must be focusable. Setting
            // tabindex to -1 makes it focusable, without making it possible to tab to it.
            // See https://developer.mozilla.org/en-US/docs/Web/HTML/Global_attributes/tabindex for details.
            builder.AddTabIndex(-1);

            builder.AddEventListener(HtmlEvents.KEYDOWN, EventCallback.Factory.Create<UIKeyboardEventArgs>(this, NavigationHandler));

            Task NavigationHandler(UIKeyboardEventArgs e) => e.Code switch
            {
                "ArrowLeft" => UserPrevious(),
                "ArrowRight" => UserNext(),
                _ => Task.CompletedTask
            };
        }

        private void AddTouchNavigation(RenderTreeBuilder builder)
        {
            if (!EnableTouch || Count < 2) return;

            if (_swipeDetector is null)
            {
                _swipeDetector = new HorizontalSwipePointerEventDetector(() => Previous(), () => Next());
            }

            builder.AddEventListeners(_swipeDetector);
        }

        private void CarouselInnerRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CarouselInnerCssClass);

            builder.OpenComponent<CascadingValue<ChildHooksInjector>>();
            builder.AddAttribute("Value", (ChildHooksInjector)ApplyChildHooksInternal);
            builder.AddAttribute("IsFixed", true);
            builder.AddChildContentFragment(ItemsRenderFragment);
            builder.CloseComponent();

            builder.CloseElement();
        }

        internal ChildHooksInjector? CustomChildHooksInjector { get; set; }

        private void ApplyChildHooksInternal(ComponentBase component)
        {
            if (this is IChildTrackingParentComponent trackingParent)
            {
                trackingParent.ApplyChildHooks(component);
            }
            if (!(CustomChildHooksInjector is null))
            {
                CustomChildHooksInjector(component);
            }
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
                        builder.AddChildContentFragment(ChildContent(item));
                    }
                    else if (item is string imageUrl)
                    {
                        builder.AddChildContentFragment(ImageRenderTemplate(imageUrl));
                    }
                    else if (item is ValueTuple<string, string> tuple)
                    {
                        builder.AddChildContentFragment(ImageRenderTemplate(tuple.Item1, tuple.Item2));
                    }

                    builder.CloseComponent();
                }
            }
        }

        private void ControlsRenderFragment(RenderTreeBuilder builder)
        {
            if (!ShowControls || Count < 2) return;

            builder.AddLine();
            builder.OpenElement(HtmlTags.A);
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, UserPrevious));
            builder.AddClassAttribute("carousel-control-prev");
            builder.AddRoleAttribute("button");
            builder.AddMarkupContent(@"<span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>");
            builder.AddMarkupContent($@"<span class=""sr-only"">{PreviousControlSrText}</span>");
            builder.CloseElement();

            builder.AddLine();
            builder.OpenElement(HtmlTags.A);
            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(this, UserNext));
            builder.AddClassAttribute("carousel-control-next");
            builder.AddRoleAttribute("button");
            builder.AddMarkupContent(@"<span class=""carousel-control-next-icon"" aria-hidden=""true""></span>");
            builder.AddMarkupContent($@"<span class=""sr-only"">{NextControlSrText}</span>");
            builder.CloseElement();
        }

        private void IndicatorsRenderFragment(RenderTreeBuilder builder)
        {
            if (!ShowIndicators || Count < 2) return;

            builder.OpenElement(HtmlTags.OL);
            builder.AddClassAttribute("carousel-indicators");
            for (ushort i = 0; i < Count; i++)
            {
                var itemIndex = i;
                builder.OpenElement(HtmlTags.LI);
                builder.AddEventListener(HtmlEvents.CLICK,
                    EventCallback.Factory.Create<UIMouseEventArgs>(this, () => UserGoTo(itemIndex))
                );
                if (ActiveIndex == itemIndex) builder.AddClassAttribute("active");
                builder.CloseElement();
            }
            builder.CloseElement();
        }

        private static RenderFragment ImageRenderTemplate(string imageUrl, string? altText = null)
        {
            return builder =>
            {
                builder.OpenComponent<Img>();
                builder.AddAttribute(HtmlAttrs.SRC, imageUrl);
                if (!(altText is null)) builder.AddAttribute(HtmlAttrs.ALT, altText);
                builder.CloseComponent();
            };
        }

        private Task ChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            if (oldActiveIndex == newActiveIndex || ChangingItems) return Task.CompletedTask;

            ChangingItems = true;
            if (!_changeItemTimer.IsStopped) _changeItemTimer.Stop();

            var changeTask = Animation == AnimationParameter.None
                ? InstantChangeActiveItem(oldActiveIndex, newActiveIndex)
                : AnimateChangeActiveItem(oldActiveIndex, newActiveIndex);

            ChangingItems = false;
            if (Autoplay) _changeItemTimer.Start();

            return changeTask;
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
            await ActiveIndexChanged.InvokeAsync(ActiveIndex).ConfigureAwait(true);
            StateHasChanged();
            await Task.Delay(1).ConfigureAwait(true);

            // Step 2
            activeElm.UpdateCssClass(directionalClassName);
            nextElm.UpdateCssClass($"{directionalClassName} {orderClassName}");
            await Task.Delay(600).ConfigureAwait(true);

            // Step 3
            activeElm.Active = false;
            nextElm.Active = true;

            activeElm.UpdateCssClass(string.Empty);
            nextElm.UpdateCssClass(string.Empty);
        }

        private Task InstantChangeActiveItem(int oldActiveIndex, int newActiveIndex)
        {
            _carouselItems[oldActiveIndex].Active = false;
            _carouselItems[newActiveIndex].Active = true;

            ActiveIndex = (ushort)newActiveIndex;
            StateHasChanged();
            return ActiveIndexChanged.InvokeAsync(ActiveIndex);
        }

        private void PageVisibilityAPI_OnPageVisibilityChanged(object? sender, PageVisibilityChangedEventArgs e)
        {
            if (e.IsPageVisible && !Cycling) Resume();
            else if (Cycling) Pause();
        }

        protected override void OnCompomnentDispose()
        {
            if (!(PageVisibilityAPI is null))
                PageVisibilityAPI.OnPageVisibilityChanged -= PageVisibilityAPI_OnPageVisibilityChanged;

            _changeItemTimer.Dispose();
        }

        void IChildTrackingParentComponent.AddChild(ComponentBase component)
        {
            if (component is CarouselItem item)
            {
                item.Active = ActiveIndex == _carouselItems.Count;
                _carouselItems.Add(item);
                if (StaticContentMode && Autoplay)
                {
                    Cycle();
                    StateHasChanged();
                }
            }
            else
            {
                ThrowInvalidChildContentException();
            }
        }

        void IChildTrackingParentComponent.RemoveChild(ComponentBase component)
        {
            if (component is CarouselItem item)
            {
                _carouselItems.Remove(item);
            }
            else
            {
                ThrowInvalidChildContentException();
            }
        }

        private void ThrowInvalidChildContentException()
        {
            throw new InvalidChildContentException($"When using a {nameof(Carousel<TItem>)} in static content mode, " +
                $"where no {nameof(Items)} is provided and {nameof(TItem)} is set to {nameof(CarouselStatic)}, " +
                $"all immediate children of the Carousel component must be a {nameof(CarouselItem)} component.");
        }
    }
}