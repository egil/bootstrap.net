using System;
using Egil.RazorComponents.Bootstrap.Services;
using Egil.RazorComponents.Testing;

namespace Egil.RazorComponents.Bootstrap.Components
{

    public class BootstrapComponentFixture : RazorComponentFixture
    {
        public override ComponentBuilder<TComponent> Component<TComponent>()
        {
            return base.Component<TComponent>().WithServices(services => services.AddBootstrapServices());
        }

        public virtual ComponentBuilder<TComponent, TItem> Component<TComponent, TItem>() where TComponent : ComponentBase
        {
            return base.Component<TComponent, TItem>().WithServices(services => services.AddBootstrapServices());
        }
    }
}
