using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap
{
    public class BootstrapContext
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

        public void RegisterRule<TComponent, TSubComponent1>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
        {
            Action<IComponent> wrappedAction = (IComponent component) => rule((TComponent)component);
            _rules[typeof(TComponent)] = wrappedAction;
            _rules[typeof(TSubComponent1)] = wrappedAction;
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
        {
            Action<IComponent> wrappedAction = (IComponent component) => rule((TComponent)component);
            _rules[typeof(TComponent)] = wrappedAction;
            _rules[typeof(TSubComponent1)] = wrappedAction;
            _rules[typeof(TSubComponent2)] = wrappedAction;
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
        {
            Action<IComponent> wrappedAction = (IComponent component) => rule((TComponent)component);
            _rules[typeof(TComponent)] = wrappedAction;
            _rules[typeof(TSubComponent1)] = wrappedAction;
            _rules[typeof(TSubComponent2)] = wrappedAction;
            _rules[typeof(TSubComponent3)] = wrappedAction;
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
        {
            Action<IComponent> wrappedAction = (IComponent component) => rule((TComponent)component);
            _rules[typeof(TComponent)] = wrappedAction;
            _rules[typeof(TSubComponent1)] = wrappedAction;
            _rules[typeof(TSubComponent2)] = wrappedAction;
            _rules[typeof(TSubComponent3)] = wrappedAction;
            _rules[typeof(TSubComponent4)] = wrappedAction;
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
        {
            Action<IComponent> wrappedAction = (IComponent component) => rule((TComponent)component);
            _rules[typeof(TComponent)] = wrappedAction;
            _rules[typeof(TSubComponent1)] = wrappedAction;
            _rules[typeof(TSubComponent2)] = wrappedAction;
            _rules[typeof(TSubComponent3)] = wrappedAction;
            _rules[typeof(TSubComponent4)] = wrappedAction;
            _rules[typeof(TSubComponent5)] = wrappedAction;
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5, TSubComponent6>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
            where TSubComponent6 : TComponent
        {
            Action<IComponent> wrappedAction = (IComponent component) => rule((TComponent)component);
            _rules[typeof(TComponent)] = wrappedAction;
            _rules[typeof(TSubComponent1)] = wrappedAction;
            _rules[typeof(TSubComponent2)] = wrappedAction;
            _rules[typeof(TSubComponent3)] = wrappedAction;
            _rules[typeof(TSubComponent4)] = wrappedAction;
            _rules[typeof(TSubComponent5)] = wrappedAction;
            _rules[typeof(TSubComponent6)] = wrappedAction;
        }
    }
}
