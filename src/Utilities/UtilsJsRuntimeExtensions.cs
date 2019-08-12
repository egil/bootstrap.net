using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Utilities
{
    internal static class UtilsJsRuntimeExtensions
    {
        internal static Task PreventDefault(this ElementRef element, IJSRuntime? jsRuntime, string eventType)
        {
            if (jsRuntime is null) throw new ArgumentNullException(nameof(jsRuntime));
            return jsRuntime.InvokeAsync<object>("bootstrapDotNet.utils.preventDefault", element, eventType);
        }

        internal static Task AllowDefault(this ElementRef element, IJSRuntime? jsRuntime, string eventType)
        {
            if (jsRuntime is null) throw new ArgumentNullException(nameof(jsRuntime));
            return jsRuntime.InvokeAsync<object>("bootstrapDotNet.utils.allowDefault", element, eventType);
        }
    }

}
