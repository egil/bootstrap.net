using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components
{
    public class BootstrapComponentBaseTests
    {
        private readonly Func<string, string> _encoder = (t) => HtmlEncoder.Default.Encode(t);
        private readonly IDispatcher _dispatcher = Renderer.CreateDefaultDispatcher();

        protected HtmlRenderer GetHtmlRenderer(IServiceProvider serviceProvider) => new HtmlRenderer(serviceProvider, _encoder, _dispatcher);

        protected static readonly RenderFragment EmptyRenderFragment = builder => { };

        protected ComponentRenderedText RenderComponent<TComponent>() where TComponent : class, IComponent
        {
            return RenderComponent<TComponent>(ParameterCollection.Empty);
        }

        protected ComponentRenderedText RenderComponent<TComponent>(RenderFragment childContent) where TComponent : class, IComponent
        {
            var parameterCollection = ParameterCollection.FromDictionary(new Dictionary<string, object>
            {
                [RenderTreeBuilder.ChildContent] = childContent
            });
            return RenderComponent<TComponent>(parameterCollection);
        }

        protected ComponentRenderedText RenderComponent<TComponent>(ParameterCollection parameterCollection) where TComponent : class, IComponent
        {
            var serviceProvider = new ServiceCollection().AddSingleton<TComponent>().BuildServiceProvider();
            var htmlRenderer = GetHtmlRenderer(serviceProvider);
            return GetResult(_dispatcher.InvokeAsync(() => htmlRenderer.RenderComponentAsync<TComponent>(parameterCollection))); ;
        }

        protected ComponentRenderedText GetResult(Task<ComponentRenderedText> task)
        {
            Assert.True(task.IsCompleted);
            if (task.IsCompletedSuccessfully)
            {
                return task.Result;
            }
            else
            {
                ExceptionDispatchInfo.Capture(task.Exception).Throw();
                throw new InvalidOperationException("We will never hit this line");
            }
        }
    }
}
