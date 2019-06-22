using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public abstract class BootstrapParentComponentBase : BootstrapContextAwareComponentBase
    {
        private RenderFragment? _childContent;

        protected bool AlwaysCascadeToChildren { get; set; } = false;

        [Parameter]
        protected internal RenderFragment ChildContent
        {
            get => BuildChildContentRenderTree;
            set => _childContent = value;
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

            if (HasContext || AlwaysCascadeToChildren)
            {
                builder.OpenComponent<CascadingValue<BootstrapContextAwareComponentBase>>(0);
                builder.AddAttribute(0, "Value", this);
                builder.AddAttribute(0, "IsFixed", true);
                builder.AddAttribute(1, RenderTreeBuilder.ChildContent, _childContent);
                builder.CloseComponent();
            }
            else
            {
                builder.AddContent(0, _childContent);
            }
        }
    }
}
