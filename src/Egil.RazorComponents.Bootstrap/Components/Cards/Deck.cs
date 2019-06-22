using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Cards
{
    public class Deck : BootstrapParentComponentBase
    {
        private const string DefaultCardCssClass = "card-deck";

        public Deck()
        {
            DefaultCssClass = DefaultCardCssClass;
        }
    }
}
