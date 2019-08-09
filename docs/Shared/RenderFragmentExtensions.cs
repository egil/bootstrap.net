using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;

namespace Egil.RazorComponents.Bootstrap.Documentation.Shared
{
    public static class RenderFragmentExtensions
    {
        private static readonly IDispatcher Dispatcher = Renderer.CreateDefaultDispatcher();
        private static readonly Func<string, string> Encoder = (t) => HtmlEncoder.Default.Encode(t);
        public static readonly ServiceProvider EmptyServiceProvider = new ServiceCollection().BuildServiceProvider();

        public static ComponentRenderedText RenderAsText(this RenderFragment renderFragment, IServiceCollection? services = null)
        {
            var serviceProvider = services?.BuildServiceProvider() ?? EmptyServiceProvider;
            var paramCollection = ParameterCollection.FromDictionary(new Dictionary<string, object>() { { "ChildContent", renderFragment } });
            using var htmlRenderer = new HtmlRenderer(serviceProvider, Encoder, Dispatcher);
            return GetResult(Dispatcher.InvokeAsync(() => htmlRenderer.RenderComponentAsync<RenderFragmentWrapper>(paramCollection)));
        }

        private static ComponentRenderedText GetResult(Task<ComponentRenderedText> task)
        {
            if (!task.IsCompleted) throw new InvalidOperationException("This should not happen!");

            if (task.IsCompletedSuccessfully)
            {
                return task.Result;
            }
            else
            {
                ExceptionDispatchInfo.Capture(task.Exception!).Throw();
                throw new InvalidOperationException("We will never hit this line");
            }
        }

        class RenderFragmentWrapper : ComponentBase
        {
            [Parameter]
            public RenderFragment? ChildContent { get; set; }

            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                if (ChildContent is null) throw new ArgumentNullException(nameof(ChildContent));
                builder.AddContent(0, ChildContent);
            }
        }
    }
}
