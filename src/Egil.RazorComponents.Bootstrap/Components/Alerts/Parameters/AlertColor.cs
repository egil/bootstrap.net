using Egil.RazorComponents.Bootstrap.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters
{
    public class AlertColor : ICssClassPrefix
    {
        private const string AlertPrefix = "alert";

        public string Prefix { get; } = AlertPrefix;
    }
}
