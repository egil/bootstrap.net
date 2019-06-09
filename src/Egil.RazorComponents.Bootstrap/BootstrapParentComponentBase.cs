using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap
{
    public abstract class BootstrapParentComponentBase : BootstrapContextAwareComponentBase
    {
        private RenderFragment? _childContent;

        [Parameter]
        protected RenderFragment ChildContent
        {
            get => BuildChildContentRenderTree;
            set => _childContent = value;
        }

        private void BuildChildContentRenderTree(RenderTreeBuilder builder)
        {
            if (_childContent is null) return;
            
            if(Parent is null && !HasContext)
            {
                builder.AddContent(0, _childContent);
            }
            else
            {
                builder.OpenComponent<CascadingValue<BootstrapContextAwareComponentBase>>(0);
                builder.AddAttribute(0, "Value", HasContext ? this : null);
                builder.AddAttribute(0, "IsFixed", true);
                builder.AddAttribute(1, RenderTreeBuilder.ChildContent, _childContent);
                builder.CloseComponent();
            }
        }
    }
}
