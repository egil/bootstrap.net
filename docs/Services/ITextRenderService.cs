using System.Linq;
using System.Text;
using Egil.RazorComponents.Bootstrap.Documentation.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Documentation.Services
{
    public interface ITextRenderService
    {
        string Render(RenderFragment renderFragment);
    }
}
