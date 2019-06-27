using System;
using Egil.RazorComponents.Bootstrap.Base;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.BlazorTestServer.Pages
{
    public class Comp : BootstrapParentComponentBase
    {
        [Parameter]
        public int[] Span { get; set;} = Array.Empty<int>();
    }
}
