using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public interface IExploseElementRef
    {
        protected internal ElementRef DomElement { get; }
    }
}