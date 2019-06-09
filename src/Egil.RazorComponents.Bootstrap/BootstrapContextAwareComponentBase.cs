using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap
{
    public abstract class BootstrapContextAwareComponentBase : BootstrapComponentBase
    {
        private BootstrapContext? _bootstrapContext;

        internal bool HasContext => !(_bootstrapContext is null);

        internal BootstrapContext ChildContext => _bootstrapContext ?? (_bootstrapContext = new BootstrapContext());

        [CascadingParameter]
        internal BootstrapContextAwareComponentBase? Parent { get; set; }

        protected virtual void RegisterChildContextRules() { }

        protected virtual void OnChildInit(IComponent component) { }

        protected virtual void OnBootstrapComponentInit() { }

        protected sealed override void OnInit()
        {
            RegisterChildContextRules();

            if (!(Parent is null) && Parent.HasContext)
            {
                Parent.OnChildInit(this);
                Parent.ChildContext.UpdateChild(this);
            }

            OnBootstrapComponentInit();
        }
    }
}
