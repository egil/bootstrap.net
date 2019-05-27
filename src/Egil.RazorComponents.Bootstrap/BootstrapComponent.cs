using Egil.RazorComponents.Bootstrap.Parameters;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap
{
    public class BootstrapComponent : ComponentBase
    {
        private static readonly char[] CssClassSplitChar = new[] { ' ' };

        protected string CssClassValue { get; private set; } = string.Empty;

        protected virtual IEnumerable<string> StaticCssClasses { get; } = Enumerable.Empty<string>();

        protected virtual IEnumerable<IParameter> CssClassSources { get; } = Enumerable.Empty<IParameter>();

        [Parameter]
        string AdditionalCssClasses { get; set; } = string.Empty;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            UpdateComponentCssClasses();
        }

        protected void UpdateComponentCssClasses()
        {
            var acc = AdditionalCssClasses.Split(CssClassSplitChar, StringSplitOptions.RemoveEmptyEntries);

            var classes = StaticCssClasses.Concat(CssClassSources.SelectMany(x => x))
                .Concat(acc)
                .Distinct();

            CssClassValue = string.Join(" ", classes);
        }
    }
}