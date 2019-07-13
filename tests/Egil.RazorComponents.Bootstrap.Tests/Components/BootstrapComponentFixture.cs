using System;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components
{

    public class BootstrapComponentFixture
    {

        protected ComponentBuilder<TComponent> Component<TComponent>() where TComponent : Microsoft.AspNetCore.Components.ComponentBase
        {
            return new ComponentBuilder<TComponent>();
        }

        protected ComponentBuilder<TComponent, TItem> Component<TComponent, TItem>() where TComponent : Microsoft.AspNetCore.Components.ComponentBase
        {
            return new ComponentBuilder<TComponent, TItem>().WithItems(new TItem[0]);
        }

        protected FragmentBuilder Fragment(Type componentType)
        {
            return new FragmentBuilder(componentType);
        }

        protected FragmentBuilder<TComponent> Fragment<TComponent>() where TComponent : Microsoft.AspNetCore.Components.ComponentBase
        {
            return new FragmentBuilder<TComponent>();
        }
    }
}
