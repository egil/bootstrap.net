using System;
using Egil.RazorComponents.Bootstrap.Services;
using Egil.RazorComponents.Testing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace Egil.RazorComponents.Bootstrap.Components
{

    public class BootstrapComponentFixture : RazorComponentFixture
    {
        public override ComponentBuilder<TComponent> Component<TComponent>()
        {
            return base.Component<TComponent>().WithServices(services =>
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
            });
        }

        public override ComponentBuilder<TComponent, TItem> Component<TComponent, TItem>()
        {
            return base.Component<TComponent, TItem>().WithServices(services =>
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
            });
        }
    }
}
