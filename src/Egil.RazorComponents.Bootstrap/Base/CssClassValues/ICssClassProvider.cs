using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    public interface ICssClassProvider : IReadOnlyCollection<string>, IEnumerable<string>
    {
        bool HasValues { get; }
    }
}
