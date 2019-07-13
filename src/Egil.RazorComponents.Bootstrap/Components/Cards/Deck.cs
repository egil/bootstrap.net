using Egil.RazorComponents.Bootstrap.Base;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public class Deck : ParentComponentBase
    {
        private const string DefaultCardCssClass = "card-deck";

        public Deck()
        {
            DefaultCssClass = DefaultCardCssClass;
        }
    }
}
