using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public abstract class BootstrapParentComponentBase : BootstrapParentAwareComponentBase
    {
        private RenderFragment? _childContent;

        protected bool AlwaysCascadeToChildren { get; set; } = false;

        [Parameter]
        public virtual RenderFragment ChildContent
        {
            get => BuildChildContentRenderTree;
            private set => _childContent = value;
        }

        /// <summary>
        /// This method is called by the <see cref="BuildRenderTree(RenderTreeBuilder)" /> method
        /// to render the component, if a custom render fragment is not specified.
        /// </summary>
        /// <param name="builder"></param>
        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementName);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }

        private void BuildChildContentRenderTree(RenderTreeBuilder builder)
        {
            if (_childContent is null) return;

            if (HasChildRules || AlwaysCascadeToChildren)
            {
                WrapRenderTreeInBootstrapParentAwareCascade(builder, _childContent);
            }
            else
            {
                builder.AddContent(_childContent);
            }
        }

        protected void WrapRenderTreeInBootstrapParentAwareCascade(RenderTreeBuilder builder, RenderFragment childContent)
        {
            builder.OpenComponent<CascadingValue<BootstrapParentAwareComponentBase>>();
            builder.AddAttribute("Value", this);
            builder.AddAttribute("IsFixed", true);
            builder.AddChildContentFragment(childContent);
            builder.CloseComponent();
        }
    }
}
