using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;

namespace Egil.RazorComponents.Bootstrap.Components.Html.Parameters
{
    public class ButtonSize : ICssClassPrefix
    {
        private const string BtnPrefix = "btn";
        public string Prefix { get; } = BtnPrefix;
    }
}
