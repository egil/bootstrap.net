using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    internal static class CollapseJsRuntimeExtensions
    {
        private const string ShowFunctionName = "bootstrapDotNet.components.collapse.show";
        private const string HideFunctionName = "bootstrapDotNet.components.collapse.hide";

        internal static Task<int> Show(this ElementReference element, IJSRuntime? jsRuntime)
        {
            if (jsRuntime is null) throw new ArgumentNullException(nameof(jsRuntime));
            return jsRuntime.InvokeAsync<int>(ShowFunctionName, element);
        }

        internal static Task<int> Hide(this ElementReference element, IJSRuntime? jsRuntime)
        {
            if (jsRuntime is null) throw new ArgumentNullException(nameof(jsRuntime));
            return jsRuntime.InvokeAsync<int>(HideFunctionName, element);
        }
    }

}
