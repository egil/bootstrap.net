using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.DependencyInjection;

namespace Egil.RazorComponents.Bootstrap.Documentation.Services
{
    public class TextRenderService : ITextRenderService
    {
        private static readonly IDispatcher Dispatcher = Renderer.CreateDefaultDispatcher();
        private static readonly Func<string, string> Encoder = (t) => HtmlEncoder.Default.Encode(t);
        private readonly IServiceProvider _serviceProvider;

        public TextRenderService()
        {
            var services = new ServiceCollection();

            _serviceProvider = services.BuildServiceProvider();
        }

        public string Render(RenderFragment renderFragment)
        {
            //var services = new ServiceCollection().AddBootstrapDocsServices();
            //services.AddTransient(_ => _serviceProvider.GetService<IUriHelper>());
            //services.AddTransient(_ => _serviceProvider.GetService<IJSRuntime>());
            //var res = renderFragment.RenderAsText(_serviceProvider);//services.BuildServiceProvider());
            //return string.Concat(res.Tokens);
            //var paramCollection = ParameterCollection.FromDictionary(
            //    new Dictionary<string, object>() { { RenderTreeBuilder.ChildContent, renderFragment } }
            //    );

            ParameterCollection paramCollection = ParameterCollection.FromDictionary(new Dictionary<string, object>() { { RenderTreeBuilder.ChildContent, renderFragment } });
            using var htmlRenderer = new HtmlRenderer(_serviceProvider, Encoder, Dispatcher);
            var renderTask = htmlRenderer.RenderComponentAsync<RenderFragmentWrapper>(paramCollection);
            var renderResultTask = Dispatcher.InvokeAsync(() => renderTask);
            return string.Concat(GetResult(renderResultTask).Tokens);
        }

        //private string RenderAsString()
        //{
        //    var result = string.Empty;
        //    try
        //    {
        //        ParameterCollection paramCollection = ParameterCollection.FromDictionary(new Dictionary<string, object>() { { "ChildContent", ChildContent } });
        //        using HtmlRenderer htmlRenderer = new HtmlRenderer(EmptyServiceProvider, Encoder, Dispatcher);
        //        RenderTreeBuilder builder = new RenderTreeBuilder(htmlRenderer);
        //        builder.AddContent(0, ChildContent);
        //        var frames = from f in builder.GetFrames().Array
        //                     where f.FrameType == RenderTreeFrameType.Markup || f.FrameType == RenderTreeFrameType.Text
        //                     select f.MarkupContent;
        //        result = string.Join("", frames.ToList());
        //    }
        //    catch
        //    {
        //        //ignored dont crash if can't get result
        //    }
        //    return result;
        //}

        private ComponentRenderedText GetResult(Task<ComponentRenderedText> task)
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
