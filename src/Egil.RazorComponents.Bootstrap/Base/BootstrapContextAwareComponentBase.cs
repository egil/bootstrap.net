using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public abstract class BootstrapParentAwareComponentBase : BootstrapComponentBase
    {
        private BootstrapRuleRegistry? _rules;

        protected internal BootstrapRuleRegistry Rules => _rules ?? (_rules = new BootstrapRuleRegistry());

        private void UpdateChildOnInit(BootstrapParentAwareComponentBase component)
        {
            if (_rules is null) return;
            _rules.UpdateChildOnInit(component);
        }

        private void UpdateChild(BootstrapParentAwareComponentBase component)
        {
            if (_rules is null) return;
            _rules.UpdateChild(component);
        }

        /// <summary>
        /// Returns true if this component has a context with rules
        /// that apply to nested components.
        /// </summary>
        protected internal bool HasChildRules => _rules != null;

        /// <summary>
        /// Gets or sets the parent Bootstrap.NET component through a casacading parameter.
        /// </summary>
        [CascadingParameter] protected internal BootstrapParentAwareComponentBase? Parent { get; set; }

        /// <summary>
        /// Gets or sets whether to ignore the parent Bootstrap.NET components child context 
        /// and any rules it might have that applies to this component.
        /// </summary>
        [Parameter] public virtual bool IgnoreParentContext { get; set; }

        /// <summary>
        /// Used to register child component rules in this components child context.
        /// NOTE: child component rules should only be registered at this point.
        /// </summary>
        protected virtual void OnRegisterChildRules() { }

        /// <summary>
        /// Each Bootstrap.NET child component will call this method, passing in
        /// themselves as the argument. This happens during the child's <see cref="OnInit"/>
        /// phase.
        /// </summary>
        /// <param name="component"></param>
        protected virtual void OnChildInit(BootstrapParentAwareComponentBase component) { }

        /// <summary>
        /// This method is called after the components normal <see cref="OnInit"/> has run,
        /// and the component has received any modification from its parent, if any.
        /// </summary>
        protected virtual void OnBootstrapInit() { }

        /// <summary>
        /// This method is called after the components normal <see cref="OnParametersSet"/> has run,
        /// and the component has received any modification from its parent, if any.
        /// </summary>
        protected virtual void OnBootstrapParametersSet() { }

        protected sealed override void OnInit()
        {
            OnRegisterChildRules();

            if (!IgnoreParentContext && Parent != null)
            {
                Parent.OnChildInit(this);
                Parent.UpdateChildOnInit(this);
            }

            OnBootstrapInit();
        }

        protected sealed override void OnParametersSet()
        {
            if (!IgnoreParentContext && Parent != null)
            {
                Parent.UpdateChild(this);
            }
            OnBootstrapParametersSet();
        }

        /// <summary>
        /// Gets or sets a custom render fragment that will be used by the default
        /// <see cref="BuildRenderTree(RenderTreeBuilder)"/> method, instead of the 
        /// <see cref="DefaultRenderFragment(RenderTreeBuilder)"/> method.
        /// <para>
        /// This is used primarly by parents to provide an alternative rendering for a
        /// component if they have the need to customize it to that level.
        /// </para>
        /// </summary>
        internal RenderFragment? CustomRenderFragment { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var renderFragment = CustomRenderFragment ?? DefaultRenderFragment;
            renderFragment(builder);
        }

        /// <summary>
        /// This method is called by the <see cref="BuildRenderTree(RenderTreeBuilder)" /> method
        /// to render the component, if a custom render fragment is not specified.
        /// </summary>
        /// <param name="builder"></param>
        protected internal virtual void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementName);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);            
            builder.CloseElement();
        }
    }
}
