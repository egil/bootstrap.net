using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Services;
using Egil.RazorComponents.Testing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public class BootstrapRazorComponentTest : RazorComponentTest
    {
        protected override void AddServices(IServiceCollection services)
        {
            services.AddSingleton(x => Mock.Of<IJSRuntime>());
            services.AddSingleton(x => Mock.Of<IUriHelper>());
            services.AddSingleton(x =>
           {
               var mock = new Mock<IComponentContext>();
               mock.SetupGet(x => x.IsConnected).Returns(true);
               return mock.Object;
           });
            services.AddBootstrapServices();
        }
    }
}
