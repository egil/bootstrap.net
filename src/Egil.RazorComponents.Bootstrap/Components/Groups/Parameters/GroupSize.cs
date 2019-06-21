using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Groups.Parameters
{
    public class GroupSize : ICssClassPrefix
    {
        private const string BtnPrefix = "btn-group";
        public string Prefix { get; } = BtnPrefix;
    }
}
