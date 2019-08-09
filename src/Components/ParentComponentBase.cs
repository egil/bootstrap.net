using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public abstract class ParentComponentBase : ComponentBase
    {
        private RenderFragment? _childContent;

        [Parameter]
        public virtual RenderFragment ChildContent
        {
            get => BuildChildContentRenderTree;
            protected set => _childContent = value;
        }

        internal ChildHooksInjector? CustomChildHooksInjector { get; set; }

        protected virtual void ApplyChildHooks(ComponentBase component) { }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementTag);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(OverriddenAttributes);
            builder.AddContent(ChildContent);
            builder.AddElementReferenceCapture(DomElementCapture);
            builder.CloseElement();
        }

        private void BuildChildContentRenderTree(RenderTreeBuilder builder)
        {
            if (_childContent is null) return;
            builder.OpenComponent<CascadingValue<ChildHooksInjector>>();
            builder.AddAttribute("Value", (ChildHooksInjector)ApplyChildHooksInternal);
            builder.AddAttribute("IsFixed", true);
            builder.AddChildContentFragment(_childContent);
            builder.CloseComponent();
        }

        private void ApplyChildHooksInternal(ComponentBase component)
        {
            ApplyChildHooks(component);
            if (this is IChildTrackingParentComponent trackingParent)
            {
                trackingParent.ApplyChildHooks(component);
            }
            if (!(CustomChildHooksInjector is null))
            {
                CustomChildHooksInjector(component);
            }
        }
    }
}
