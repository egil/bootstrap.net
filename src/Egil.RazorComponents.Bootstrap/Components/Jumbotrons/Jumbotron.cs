using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Jumbotrons
{
    public sealed class Jumbotron : BootstrapParentComponentBase
    {
        [Parameter, CssClassToggleParameter("jumbotron-fluid")] public bool Fluid { get; set; }

        public Jumbotron()
        {
            DefaultCssClass = "jumbotron";
        }
    }
}
