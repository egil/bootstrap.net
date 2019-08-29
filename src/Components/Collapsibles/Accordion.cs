using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Cards;
using Egil.RazorComponents.Bootstrap.Components.Collapsibles.Events;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{

    public sealed class Accordion : ParentComponentBase, IChildTrackingParentComponent
    {
        private readonly List<AccordionItem> _items = new List<AccordionItem>();

        private int? _expandedIndex = 0;
        private HashSet<int>? _expandedIndexes;

        /// <summary>
        /// Gets or sets the index number of the expanded <see cref="Card"/> in the accordion.
        /// </summary>
        [Parameter]
        public int? ExpandedIndex
        {
            get => _expandedIndex;
            set
            {
                if (value.HasValue && value.Value < 0)
                {
                    throw new ArgumentException($"{nameof(ExpandedIndex)} must be a integer equal to or larger than zero (0)");
                }
                _expandedIndex = value;
            }
        }
        [Parameter] public EventCallback<int?> ExpandedIndexChanged { get; set; }


        /// <summary>
        /// Gets or sets whether the accordion allows for multiple cards 
        /// to be visible/expanded at the same time.
        /// </summary>
        [Parameter] public bool MultiExpand { get; set; }

        /// <summary>
        /// Gets or sets the index numbers of the expanded <see cref="Card"/>s in the accordion.
        /// </summary>
        [Parameter]
        public IReadOnlyCollection<int> ExpandedIndexes
        {
            get
            {
                if (_expandedIndexes is null)
                    return Array.Empty<int>();
                else
                    return _expandedIndexes;
            }
            set
            {
                if (value is null) return;

                _expandedIndexes = value.ToHashSet();

                int removed = _expandedIndexes.RemoveWhere(x => x < 0);
                if (removed > 0)
                {
                    throw new ArgumentException($"{nameof(ExpandedIndexes)} must only contain integers equal to or larger than zero (0)");
                }
            }
        }

        [Parameter] public EventCallback<IReadOnlyCollection<int>> ExpandedIndexesChanged { get; set; }

        /// <summary>
        /// Gets or sets whether the buttons in the headers of each <see cref="Card"/>
        /// is rendered as a link or as a button.
        /// </summary>
        [Parameter] public bool HeaderButtonAsLink { get; set; }

        public Accordion()
        {
            DefaultCssClass = "accordion";
        }

        private void CardToggled(AccordionItem toggledItem)
        {
            if (toggledItem.Collapse is null)
                throw new InvalidOperationException($"BUG: {nameof(AccordionItem.Collapse)} not set to a value.");

            if (!MultiExpand)
                ExclusiveCardToggle(toggledItem);
            else
                MultiCardToggle(toggledItem);
        }

        private void MultiCardToggle(AccordionItem toggledItem)
        {
            var expand = !toggledItem.Collapse.Expanded;
            var toggledItemIdx = _items.IndexOf(toggledItem);

            if (expand)
                _expandedIndexes?.Add(toggledItemIdx);
            else
                _expandedIndexes?.Remove(toggledItemIdx);

            ExpandedIndexesChanged.InvokeAsync(ExpandedIndexes);
        }

        private void ExclusiveCardToggle(AccordionItem toggledItem)
        {
            if (!toggledItem.Collapse.Expanded)
                ExpandedIndex = _items.IndexOf(toggledItem);
            else
                ExpandedIndex = null;

            ExpandedIndexChanged.InvokeAsync(ExpandedIndex);
        }

        protected override void OnCompomnentInit()
        {
            if (MultiExpand && _expandedIndexes is null)
            {
                _expandedIndexes = new HashSet<int>() { 0 };
            }
        }

        protected override void OnCompomnentParametersSet()
        {
            if (!MultiExpand && !(_expandedIndexes is null))
            {
                throw new ArgumentException($"Do not use {nameof(ExpandedIndexes)} when {nameof(MultiExpand)} is not set or false. Use {nameof(ExpandedIndex)} instead.");
            }
        }

        protected override void ApplyChildHooks(ComponentBase component)
        {
            if (component is Card card)
            {
                ApplyCardHooks(card);
            }
            else
            {
                throw new InvalidChildContentException($"Only {nameof(Card)} components are allowed inside an {nameof(Accordion)}.");
            }
        }

        private bool ShouldCardBeExpanded(AccordionItem item)
        {
            return MultiExpand
                ? ExpandedIndexes.Contains(_items.IndexOf(item))
                : _items.IndexOf(item) == ExpandedIndex;
        }

        private void ApplyCardHooks(Card card)
        {
            var cardId = $"card-{card.GetHashCode()}";
            var headerId = $"header-{cardId}";
            var collapseId = $"body-{cardId}";
            var item = new AccordionItem() { Card = card };
            _items.Add(item);

            card.CustomChildHooksInjector = (component) =>
            {
                switch (component)
                {
                    case Header header:
                        header.CustomRenderFragment = (builder) =>
                        {
                            builder.OpenElement(header.DefaultElementTag);
                            builder.AddIdAttribute(headerId);
                            builder.AddRoleAttribute("heading");
                            builder.AddClassAttribute(header.CssClassValue);
                            builder.AddMultipleAttributes(header.AdditionalAttributes);
                            builder.AddMultipleAttributes(header.OverriddenAttributes);

                            builder.OpenComponent<Button>();
                            builder.AddAttribute(HtmlAttrs.ARIA_CONTROLS, collapseId);
                            builder.AddAttribute(HtmlAttrs.ARIA_EXPANDED, ShouldCardBeExpanded(item).ToLowerCaseString());
                            builder.AddAttribute(nameof(Button.AsLink), HeaderButtonAsLink);
                            builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(card, _ => CardToggled(item)));
                            builder.AddChildContentFragment(header.ChildContent);
                            builder.CloseComponent();

                            builder.CloseElement();
                        };
                        break;
                    case Content content:
                        content.CustomRenderFragment = (builder) =>
                        {
                            builder.OpenComponent<Collapse>();
                            builder.AddChildContentFragment(content.DefaultRenderFragment);
                            builder.CloseComponent();
                        };
                        break;

                    case Collapse collapse:
                        item.Collapse = collapse;
                        collapse.Id = collapseId;
                        collapse.Expanded = ShouldCardBeExpanded(item);
                        collapse.AddOverride(HtmlAttrs.ARIA_LABELLEDBY, headerId);
                        collapse.OnParametersSetHook = _ =>
                        {
                            collapse.Expanded = ShouldCardBeExpanded(item);
                        };
                        break;

                    default: break;
                }
            };
        }

        void IChildTrackingParentComponent.AddChild(ComponentBase component)
        {
            if (!(component is Card))
                throw new InvalidChildContentException($"Only {nameof(Card)} components are allowed inside an {nameof(Accordion)}.");
        }

        void IChildTrackingParentComponent.RemoveChild(ComponentBase component)
        {
            if (component is Card card)
            {
                _items.RemoveAll(x => x.Card == card);
            }
            else
            {
                throw new InvalidChildContentException($"Only {nameof(Card)} components are allowed inside an {nameof(Accordion)}.");
            }
        }
    }
}
