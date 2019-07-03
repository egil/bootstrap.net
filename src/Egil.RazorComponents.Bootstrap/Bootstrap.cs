using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.Context;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap
{

    public class Bootstrap : ComponentBase
    {
        private readonly IBootstrapContext _bootstrapContext = new BootstrapContext();

        [Parameter] public RenderFragment? ChildContent { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<IBootstrapContext>>();
            builder.AddAttribute("Value", _bootstrapContext);
            builder.AddAttribute("IsFixed", true);
            builder.AddAttribute(RenderTreeBuilder.ChildContent, ChildContent!);
            builder.CloseComponent();
        }
    }
}
