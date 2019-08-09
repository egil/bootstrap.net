using Microsoft.Extensions.DependencyInjection;

namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    public static class ComponentServiceCollectionExtensions
    {
        public static IServiceCollection AddPageVisibility(this IServiceCollection services)
        {
            services.AddScoped<IPageVisibilityAPI, PageVisibilityAPIService>();
            return services;
        }
    }
}
