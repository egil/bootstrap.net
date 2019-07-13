using System;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public interface IChildTrackingParentComponent
    {
        protected internal virtual void ApplyChildHooks(ComponentBase component)
        {
            component.OnInitHook = AddChild;
            component.OnDisposedHook = RemoveChild;
        }

        protected void AddChild(ComponentBase component);

        protected void RemoveChild(ComponentBase component);
    }
}
