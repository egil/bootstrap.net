using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public class BootstrapRuleRegistry
    {
        private readonly Dictionary<(Type childType, bool isOnInitRule), Action<IComponent>> _rules = new Dictionary<(Type childType, bool isOnInitRule), Action<IComponent>>();

        public bool TryGetRule<TComponent>(out Action<TComponent>? rule) where TComponent : class, IComponent
        {
            rule = default;
            if (_rules.TryGetValue((typeof(TComponent), false), out var tmp) && tmp is Action<TComponent> result)
            {
                rule = result;
                return true;
            }
            return false;
        }

        public bool TryGetOnInitRule<TComponent>(out Action<TComponent>? rule) where TComponent : class, IComponent
        {
            rule = default;
            if (_rules.TryGetValue((typeof(TComponent), true), out var tmp) && tmp is Action<TComponent> result)
            {
                rule = result;
                return true;
            }
            return false;
        }

        public void UpdateChildOnInit<TComponent>(TComponent childComponent) where TComponent : class, IComponent
        {
            var type = childComponent.GetType();
            if (_rules.TryGetValue((type, true), out Action<IComponent> applyAction))
                applyAction(childComponent);
        }

        public void UpdateChild<TComponent>(TComponent childComponent) where TComponent : class, IComponent
        {
            var type = childComponent.GetType();
            if (_rules.TryGetValue((type, false), out Action<IComponent> applyAction))
                applyAction(childComponent);
        }

        public void CopyRulesFrom(BootstrapRuleRegistry otherContext)
        {
            foreach (var typeRulePair in otherContext._rules)
            {
                _rules.Add(typeRulePair.Key, typeRulePair.Value);
            }
        }

        public void RegisterOnInitRule<TComponent>(Action<TComponent> rule) where TComponent : class, IComponent
        {
            RegisterRule<TComponent>(rule, true);
        }

        public void RegisterOnInitRule<TComponent, TSubComponent1>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1>(rule, true);
        }

        public void RegisterOnInitRule<TComponent, TSubComponent1, TSubComponent2>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2>(rule, true);
        }

        public void RegisterOnInitRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3>(rule, true);
        }

        public void RegisterOnInitRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4>(rule, true);
        }

        public void RegisterOnInitRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5>(rule, true);
        }

        public void RegisterOnInitRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5, TSubComponent6>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
            where TSubComponent6 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5, TSubComponent6>(rule, true);
        }

        public void RegisterRule<TComponent>(Action<TComponent> rule) where TComponent : class, IComponent
        {
            RegisterRule<TComponent>(rule, false);
        }

        public void RegisterRule<TComponent, TSubComponent1>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1>(rule, false);
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2>(rule, false);
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3>(rule, false);
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4>(rule, false);
        }

        public void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5>(Action<TComponent> rule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
        {
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5>(rule, false);
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
            RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5, TSubComponent6>(rule, false);
        }

        private void RegisterRule<TComponent>(Action<TComponent> rule, bool onInitRule) where TComponent : class, IComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
        }

        private void RegisterRule<TComponent, TSubComponent1>(Action<TComponent> rule, bool onInitRule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent1), onInitRule)] = wrappedAction;
        }

        private void RegisterRule<TComponent, TSubComponent1, TSubComponent2>(Action<TComponent> rule, bool onInitRule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent1), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent2), onInitRule)] = wrappedAction;
        }

        private void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3>(Action<TComponent> rule, bool onInitRule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent1), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent2), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent3), onInitRule)] = wrappedAction;
        }

        private void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4>(Action<TComponent> rule, bool onInitRule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent1), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent2), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent3), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent4), onInitRule)] = wrappedAction;
        }

        private void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5>(Action<TComponent> rule, bool onInitRule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent1), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent2), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent3), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent4), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent5), onInitRule)] = wrappedAction;
        }

        private void RegisterRule<TComponent, TSubComponent1, TSubComponent2, TSubComponent3, TSubComponent4, TSubComponent5, TSubComponent6>(Action<TComponent> rule, bool onInitRule)
            where TComponent : class, IComponent
            where TSubComponent1 : TComponent
            where TSubComponent2 : TComponent
            where TSubComponent3 : TComponent
            where TSubComponent4 : TComponent
            where TSubComponent5 : TComponent
            where TSubComponent6 : TComponent
        {
            void wrappedAction(IComponent component) => rule((TComponent)component);
            _rules[(typeof(TComponent), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent1), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent2), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent3), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent4), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent5), onInitRule)] = wrappedAction;
            _rules[(typeof(TSubComponent6), onInitRule)] = wrappedAction;
        }
    }
}
