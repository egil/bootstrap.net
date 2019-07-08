using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Cards;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    public sealed class Accordion : BootstrapParentComponentBase
    {

        private readonly Dictionary<Card, AccordionCardState> _items = new Dictionary<Card, AccordionCardState>();
        private readonly ISet<int> _expandedIndexes = new SortedSet<int>() { 0 };

        /// <summary>
        /// Gets or sets the index numbers of the expanded <see cref="Card"/>'s in the
        /// accordion. Specify a single or multiple numbers (zero based index) separated
        /// by a comma or space.
        /// </summary>
        [Parameter]
        public string ExpandedIndex
        {
            get => string.Join(',', _expandedIndexes);
            set
            {
                _expandedIndexes.Clear();
                foreach (var item in value.SplitOnCommaOrSpace())
                {
                    if (int.TryParse(item, out var index))
                    {
                        _expandedIndexes.Add(index);
                    }
                    else
                    {
                        throw new ArgumentException("ExpandedIndex must be a list of integers, separated by a comma (,)");
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the accordion allows for multiple cards 
        /// to be visible/expanded at the same time.
        /// </summary>
        [Parameter] public bool MultiExpand { get; set; }

        /// <summary>
        /// Gets or sets whether the buttons in the headers of each <see cref="Card"/>
        /// is rendered as a link or as a button.
        /// </summary>
        [Parameter] public bool HeaderButtonAsLink { get; set; }

        public Accordion()
        {
            DefaultCssClass = "accordion";
        }

        protected override void OnChildInit(BootstrapParentAwareComponentBase component)
        {
            if (component is Card card)
            {
                _items[card] = new AccordionCardState(_items.Count) { Expanded = _expandedIndexes.Contains(_items.Count) };
            }
            else
            {
                throw new InvalidChildContentException($"Only {nameof(Card)} components are allowed inside an {nameof(Accordion)}.");
            }
        }

        protected override void OnRegisterChildRules()
        {
            Rules.RegisterOnInitRule<Card>(RegisterCardChildRules);
        }

        private void CardToggled(Card toggledCard)
        {
            if (MultiExpand)
            {
                var state = _items[toggledCard];
                state.Toggle();

                if (state.Expanded)
                    _expandedIndexes.Add(state.Index);
                else
                    _expandedIndexes.Remove(state.Index);
            }
            else
            {
                foreach (var (card, state) in _items)
                {
                    if (card == toggledCard)
                        state.Toggle();
                    else
                        state.Hide();

                    if (state.Expanded)
                        _expandedIndexes.Add(state.Index);
                    else
                        _expandedIndexes.Remove(state.Index);
                }
            }
        }

        private void RegisterCardChildRules(Card card)
        {
            var cardId = $"card-{card.GetHashCode()}";
            var headerId = $"header-{cardId}";
            var collapseId = $"body-{cardId}";

            card.Rules.TryGetOnInitRule<Header>(out var existingHeaderRule);
            card.Rules.RegisterOnInitRule<Header>(header =>
            {
                if (!(existingHeaderRule is null)) existingHeaderRule(header);

                header.CustomRenderFragment = (builder) =>
                {
                    builder.OpenElement(header.DefaultElementName);
                    builder.AddClassAttribute(header.CssClassValue);
                    builder.AddMultipleAttributes(header.AdditionalAttributes);

                    builder.OpenComponent<Button>();
                    builder.AddAttribute(HtmlAttrs.ARIA_CONTROLS, collapseId);
                    builder.AddAttribute(HtmlAttrs.ARIA_EXPANDED, _items[card].Expanded.ToLowerCaseString());
                    builder.AddAttribute("AsLink", HeaderButtonAsLink);
                    builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(card, _ => CardToggled(card)));

                    builder.AddAttribute(RenderTreeBuilder.ChildContent, header.ChildContent);
                    builder.CloseComponent();

                    builder.CloseElement();
                };
            });

            card.Rules.RegisterRule<Header>(header =>
            {
                header.AdditionalAttributes[HtmlAttrs.ID] = headerId;
                header.AdditionalAttributes[HtmlAttrs.ROLE] = "heading";
            });

            card.Rules.TryGetOnInitRule<Content>(out var existingContentRule);
            card.Rules.RegisterOnInitRule<Content>(content =>
            {
                if (!(existingContentRule is null)) existingContentRule(content);

                content.CustomRenderFragment = (builder) =>
                {
                    builder.OpenComponent<Collapse>();
                    builder.AddAttribute(RenderTreeBuilder.ChildContent, (RenderFragment)content.DefaultRenderFragment);
                    builder.CloseComponent();
                };
            });

            card.Rules.RegisterOnInitRule<Collapse>(collapse =>
            {
                var item = _items[card];

                collapse.Id = collapseId;
                collapse.AriaLabelledBy = headerId;
                collapse.Expanded = item.Expanded;

                _items[card].Collapse = collapse;
            });
        }
    }
}
