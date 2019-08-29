using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    #pragma warning disable CA1710 // Identifiers should have correct suffix
    public interface ICssClassProvider : IReadOnlyCollection<string>, IEnumerable<string>
    {
        bool HasValues { get; }
    }
}
