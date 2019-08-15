using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class ComponentContextExtensions
    {
        public static async Task<bool> IsConnectedAsync(this IComponentContext componentContext, CancellationToken token = default)
        {
            while (!componentContext.IsConnected && !token.IsCancellationRequested)
            {
                await Task.Delay(50, token);
            }
            return componentContext.IsConnected;
        }
    }
}
