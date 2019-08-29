using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Documentation.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Documentation.Components
{
    public abstract class ExampleCode : ComponentBase
    {
        private Type? _file;

        protected RenderFragment ExampleOutput => (builder) =>
        {
            builder.OpenComponent(0, File);
            builder.CloseComponent();
        };

        protected string? Id => File?.FullName?.Replace(".", "", StringComparison.OrdinalIgnoreCase);
        protected string? ExampleSource { get; set; }

        [Inject] private IJSRuntime? JsRuntime { get; set; }
        [Inject] private IComponentContext? Context { get; set; }
        [Inject] private IExampleComponentRepository? ExampleRepo { get; set; }

        [Parameter]
        public Type File
        {
            get => _file!;
            set
            {
                if (value is null || value.FullName is null)
                    throw new ArgumentNullException(nameof(File));

                _file = value;
            }
        }

        [Parameter]
        public bool ShowSourcesOnly { get; set; }

        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter] public string? CustomExcludePattern { get; set; }

        [Parameter] public bool TrimLoremLipsumText { get; set; }

        protected ElementReference ExampleElement { get; set; }

        internal bool ExampleTriggersAfterRender { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var source = await ExampleRepo!.GetExampleAsync(File.FullName!);
            source = RemoveCsharpExcludedExampleCode(source);
            source = RemoveRazorExcludedExampleCode(source);
            if (CustomExcludePattern != null)
            {
                source = source.Replace(CustomExcludePattern, "", StringComparison.OrdinalIgnoreCase);
            }
            if(TrimLoremLipsumText)
            {
                source = Regex.Replace(source, 
                    @"Lorem ipsum dolor sit amet[,| |a-z|\.]*", 
                    "Lorem ipsum dolor sit amet, consectetur adipiscing ...", 
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            ExampleSource = source;
        }

        private static string RemoveCsharpExcludedExampleCode(string source)
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

        private static string RemoveRazorExcludedExampleCode(string source)
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
                return JsRuntime!.InvokeAsync<object>("bootstrapDotNetDocs.example.setOutputHtml", ExampleElement, TrimLoremLipsumText);
        }
    }
}
