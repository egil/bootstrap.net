using Egil.RazorComponents.Bootstrap.Services.EventBus;
using Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI;
using Microsoft.Extensions.DependencyInjection;

namespace Egil.RazorComponents.Bootstrap.Services
{
    public static class ComponentServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapServices(this IServiceCollection services)
        {
            services.AddScoped<IPageVisibilityAPI, PageVisibilityAPIService>();
            services.AddScoped<IEventBus, ComponentEventBus>();
            return services;
        }
    }
}
