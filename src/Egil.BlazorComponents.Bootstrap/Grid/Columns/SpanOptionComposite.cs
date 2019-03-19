using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Egil.BlazorComponents.Bootstrap.Grid.Columns
{
    [DebuggerDisplay("ColSpanOptionComposite: {CssClass}")]
    internal class SpanOptionComposite : SpanOptionBase
    {
        private readonly HashSet<SpanOption> options;

        public SpanOptionComposite(SpanOption option, SpanOption anotherOption)
        {
            options = new HashSet<SpanOption>() { option, anotherOption };
        }

        public override string CssClass => string.Join(" ", options.Select(x => x.CssClass));

        public static SpanOptionBase operator |(SpanOptionComposite composite, SpanOption anotherOption)
        {
            composite.options.Add(anotherOption);
            return composite;
        }
    }
}
