using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Documentation.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Documentation.Components
{
    public class ExampleCode : ComponentBase
    {
        protected RenderFragment ExampleOutput => (builder) =>
        {
            builder.OpenComponent(0, File);
            builder.CloseComponent();
        };

        protected string? Id => File?.FullName?.Replace(".", "");
        protected string? ExampleSource { get; set; }

        [Inject] private IJSRuntime? JsRuntime { get; set; }
        [Inject] private IComponentContext? Context { get; set; }
        [Inject] private IExampleComponentRepository? ExampleRepo { get; set; }

        [Parameter]
        public Type? File { get; set; }

        [Parameter]
        public bool ShowSourcesOnly { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter] public string? CustomExcludePattern { get;set;}

        protected ElementReference ExampleElement { get; set; }

        internal bool ExampleTriggersAfterRender { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (File is null || File.FullName is null) throw new ArgumentNullException(nameof(File));
            var source = await ExampleRepo!.GetExampleAsync(File.FullName);
            source = RemoveExcludedExampleCode(source);
            source = RemoveExcludedExampleCode2(source);
            if(CustomExcludePattern != null)
            {
                source = source.Replace(CustomExcludePattern, "");
            }
            ExampleSource = source;
        }

        private static string RemoveExcludedExampleCode(string source)
        {
            const string startExcludeMarker = "/*example_exclude*/";
            const string endExcludeMarker = "/*end_example_exclude*/";
            var excludeStart = source.IndexOf(startExcludeMarker, StringComparison.OrdinalIgnoreCase);
            if (excludeStart >= 0)
            {
                var excludeEnd = source.IndexOf(endExcludeMarker, StringComparison.OrdinalIgnoreCase) + endExcludeMarker.Length + Environment.NewLine.Length + 4 /* tab */;
                var excludeLength = excludeEnd - excludeStart;
                return source.Remove(excludeStart, excludeLength);
            }
            return source;
        }

        private static string RemoveExcludedExampleCode2(string source)
        {
            const string startExcludeMarker = "@*example_exclude*@";
            const string endExcludeMarker = "@*end_example_exclude*@";
            int excludeStart;
            do
            {
                excludeStart = source.IndexOf(startExcludeMarker, StringComparison.OrdinalIgnoreCase);
                if (excludeStart >= 0)
                {
                    var excludeEnd = source.IndexOf(endExcludeMarker, StringComparison.OrdinalIgnoreCase) + endExcludeMarker.Length + Environment.NewLine.Length;
                    var excludeLength = excludeEnd - excludeStart;
                    source = source.Remove(excludeStart, excludeLength);
                }
            } while (excludeStart >= 0);
            return source;
        }

        protected override Task OnAfterRenderAsync()
        {
            if (ExampleTriggersAfterRender) return Task.CompletedTask;
            return ExampleOnAfterRenderAsync();
        }

        internal Task ExampleOnAfterRenderAsync()
        {
            var isConnected = Context?.IsConnected ?? false;
            if (!isConnected)
                return Task.CompletedTask;
            else
                return JsRuntime!.InvokeAsync<object>("bootstrapDotNetDocs.example.setOutputHtml", ExampleElement);
        }
    }
}
