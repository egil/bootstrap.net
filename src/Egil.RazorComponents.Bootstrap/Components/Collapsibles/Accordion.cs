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
        private const string CardHeaderCssClass = "card-header";

        private readonly Dictionary<Card, (Button? Button, Collapse? Collapse)> _items = new Dictionary<Card, (Button?, Collapse?)>();

        [Parameter] public bool MultiExpand { get; set; }

        [Parameter] public bool HeaderButtonAsLink { get; set; }

        public Accordion()
        {
            DefaultCssClass = "accordion";
        }

        protected override void OnChildInit(BootstrapContextAwareComponentBase component)
        {
            if (component is Card card)
            {

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
                _items[toggledCard].Collapse?.Toggle();
            }
            else
            {
                foreach (var (card, (_, collapse)) in _items)
                {
                    if (collapse is null) continue;

                    if (card == toggledCard)
                        collapse.Show();
                    else
                        collapse.Hide();
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
                    builder.AddAttribute("AsLink", HeaderButtonAsLink);
                    builder.AddEventListener(HtmlEvents.CLICK, EventCallback.Factory.Create<UIMouseEventArgs>(card, _ => CardToggled(card)));

                    builder.AddAttribute(HtmlAttrs.ARIA_CONTROLS, collapseId);
                    builder.AddAttribute(RenderTreeBuilder.ChildContent, header.ChildContent);
                    builder.CloseComponent();

                    builder.CloseElement();
                };

                header.Rules.RegisterOnInitRule<Button>(btn =>
                {
                    if (_items.TryGetValue(card, out var item))
                    {
                        item.Button = btn;
                    }
                    else
                    {
                        _items[card] = (btn, null);
                    }
                });
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
                collapse.Id = collapseId;
                collapse.AriaLabelledBy = headerId;

                if (_items.TryGetValue(card, out var item))
                {
                    item.Collapse = collapse;
                }
                else
                {
                    _items[card] = (null, collapse);
                }
            });
        }
    }
}
