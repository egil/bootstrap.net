using System;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    #pragma warning disable CA1710 // Identifiers should have correct suffix
    public class ActiveCssClassProvider : CssClassProviderBase
    {
        public const string DefaultActiveClass = "active";

        public string ActiveClass { get; }

        public ActiveCssClassProvider(string activeClass = DefaultActiveClass)
        {
            ActiveClass = activeClass;
        }

        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return ActiveClass;
        }

        public static bool IsDefault(string? activeClass) => !(activeClass is null) && DefaultActiveClass.Equals(activeClass, StringComparison.Ordinal);

        public static readonly ActiveCssClassProvider Default = new ActiveCssClassProvider();
    }
}
