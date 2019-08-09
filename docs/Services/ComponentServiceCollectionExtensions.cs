using Egil.RazorComponents.Bootstrap.Documentation.Components;
using Egil.RazorComponents.Bootstrap.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Egil.RazorComponents.Bootstrap.Documentation.Services
{
    public static class ComponentServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapDocsServices(this IServiceCollection services)
        {
            services.AddBootstrapServices();
            services.AddSingleton<IExampleComponentRepository>(_ => new AssemblyEmbeddedExampleComponentRepository(typeof(App).Assembly));            
            return services;
        }
    }
}
