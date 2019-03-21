using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class AutoOption : Option
    {
        private const string OptionText = "auto";

        public override string CssClass => OptionText;
    }
}
