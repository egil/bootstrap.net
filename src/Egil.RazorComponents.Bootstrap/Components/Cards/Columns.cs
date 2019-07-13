using Egil.RazorComponents.Bootstrap.Base;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    // TODO: Does the "Columns" name clash with "Column"? 
    public class Columns : ParentComponentBase
    {
        private const string DefaultCardCssClass = "card-columns";

        public Columns()
        {
            DefaultCssClass = DefaultCardCssClass;
        }
    }
}
