using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Components.Badges.Parameters
{
    public class BadgeColor : ICssClassPrefix
    {
        private const string BadgePrefix = "badge";

        public string Prefix { get; } = BadgePrefix;
    }
}
