using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap
{
    internal class BootstrapContext
    {
        private readonly Dictionary<Type, Action<IComponent>> _rules = new Dictionary<Type, Action<IComponent>>();

        public void UpdateChild<TComponent>(TComponent childComponent) where TComponent : class, IComponent
        {
            var type = childComponent.GetType();
            if (_rules.TryGetValue(type, out Action<IComponent> applyAction))
            {
                applyAction(childComponent);
            }
        }

        public void RegisterRule<TComponent>(Action<TComponent> rule) where TComponent : class, IComponent
        {
            _rules[typeof(TComponent)] = (IComponent component) => rule((TComponent)component);
        }
    }
}
