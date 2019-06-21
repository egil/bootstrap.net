using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Utilities.Sizings
{
    public class WidthSizePrefix : ICssClassPrefix
    {
        private const string WidthPrefix = "w";
        public string Prefix { get; } = WidthPrefix;
    }
}
