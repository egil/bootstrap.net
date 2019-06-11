using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap
{
    public abstract class BootstrapContextAwareComponentBase : BootstrapComponentBase
    {
        private BootstrapContext? _bootstrapContext;

        protected internal bool HasContext => !(_bootstrapContext is null);

        protected internal BootstrapContext ChildContext => _bootstrapContext ?? (_bootstrapContext = new BootstrapContext());

        [CascadingParameter]
        protected internal BootstrapContextAwareComponentBase? Parent { get; private set; }

        [Parameter]
        public bool IgnoreParentContext { get; private set; }

        protected virtual void RegisterChildContextRules() { }

        protected virtual void OnChildInit(IComponent component) { }

        protected virtual void OnBootstrapComponentInit() { }

        protected override void OnInit()
        {
            RegisterChildContextRules();

            if (!IgnoreParentContext && !(Parent is null) && Parent.HasContext)
            {
                Parent.OnChildInit(this);
                Parent.ChildContext.UpdateChild(this);
            }

            OnBootstrapComponentInit();
        }
    }
}
