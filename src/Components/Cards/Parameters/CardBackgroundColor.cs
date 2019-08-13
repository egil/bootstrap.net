using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Cards.Parameters
{
    public class CardBackgroundColor : ICssClassPrefix
    {
        private const string CardPrefix = "bg";

        public string Prefix { get; } = CardPrefix;
    }
}
