using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components
{

    public class BootstrapComponentFixture
    {
        private readonly Func<string, string> _encoder = (t) => HtmlEncoder.Default.Encode(t);
        private readonly IDispatcher _dispatcher = Renderer.CreateDefaultDispatcher();

        public Mock<IUriHelper> UriHelperMock { get; set; } = new Mock<IUriHelper>();

        protected HtmlRenderer GetHtmlRenderer(IServiceProvider serviceProvider) =>
            new HtmlRenderer(serviceProvider, _encoder, _dispatcher);

        protected static RenderFragment CreateFragment<TComponent>(params object[] componentInput)
            where TComponent : class, IComponent
        {
            return builder =>
            {
                builder.OpenComponent<TComponent>(0);

                if (componentInput.Length > 0)
                {
                    if (componentInput[0] is RenderFragment)
                    {
                        builder.AddAttribute(1, RenderTreeBuilder.ChildContent, CreateFragment(componentInput.Select(x => (RenderFragment)x).ToArray()));
                    }
                    else
                    {
                        for (int i = 0; i < componentInput.Length; i++)
                        {
                            dynamic tuple = componentInput[i];
                            builder.AddAttribute(i + 1, tuple.Item1, tuple.Item2);
                        }
                    }
                }

                builder.CloseComponent();
            };
        }

        protected static RenderFragment CreateFragment(Type componentType, params RenderFragment[] childContents)
        {
            return builder =>
            {
                builder.OpenComponent(0, componentType);

                if (childContents.Length > 0)
                {
                    builder.AddAttribute(1, RenderTreeBuilder.ChildContent, CreateFragment(childContents));
                }

                builder.CloseComponent();
            };
        }

        private static RenderFragment CreateFragment(RenderFragment[] childContents)
        {
            return builder =>
            {
                for (int i = 0; i < childContents.Length; i++)
                {
                    builder.AddContent(i, childContents[i]);
                }
            };
        }

        protected ComponentRenderedText RenderComponent<TComponent>()
            where TComponent : class, IComponent
        {
            return RenderComponent<TComponent>(ParameterCollection.Empty);
        }

        protected ComponentRenderedText RenderComponent<TComponent>(params (string ParameterName, object value)[] paramValues)
            where TComponent : class, IComponent
        {
            var parameters = paramValues.ToDictionary(x => x.ParameterName, x => x.value);
            return RenderComponent<TComponent>(ParameterCollection.FromDictionary(parameters));
        }

        protected ComponentRenderedText RenderComponent<TComponent>(string childMarkupContent)
            where TComponent : class, IComponent
        {
            return RenderComponent<TComponent>(b => b.AddMarkupContent(0, childMarkupContent));
        }

        protected ComponentRenderedText RenderComponent<TComponent>(params RenderFragment[] childContents)
            where TComponent : class, IComponent
        {
            var parameterCollection = ParameterCollection.FromDictionary(new Dictionary<string, object>
            {
                [RenderTreeBuilder.ChildContent] = CreateFragment(childContents)
            });
            return RenderComponent<TComponent>(parameterCollection);
        }

        protected ComponentRenderedText RenderComponent<TComponent>(RenderFragment childContent, params (string ParameterName, object value)[] paramValues)
            where TComponent : class, IComponent
        {
            var parameters = paramValues.ToDictionary(x => x.ParameterName, x => x.value);
            parameters.Add(RenderTreeBuilder.ChildContent, childContent);

            return RenderComponent<TComponent>(ParameterCollection.FromDictionary(parameters));
        }

        protected ComponentRenderedText RenderComponent<TComponent>(RenderFragment[] childContents, params (string ParameterName, object value)[] paramValues)
            where TComponent : class, IComponent
        {
            var parameters = paramValues.ToDictionary(x => x.ParameterName, x => x.value);
            parameters.Add(RenderTreeBuilder.ChildContent, CreateFragment(childContents));
            
            return RenderComponent<TComponent>(ParameterCollection.FromDictionary(parameters));
        }

        protected ComponentRenderedText RenderComponent<TComponent>(ParameterCollection parameterCollection)
            where TComponent : class, IComponent
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<TComponent>()
                .AddSingleton<IUriHelper>(x => UriHelperMock.Object)
                .BuildServiceProvider();

            using var htmlRenderer = GetHtmlRenderer(serviceProvider);
            return GetResult(_dispatcher.InvokeAsync(() => htmlRenderer.RenderComponentAsync<TComponent>(parameterCollection)));
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
