using System;
using System.Collections;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Options;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    #pragma warning disable CA1710 // Identifiers should have correct suffix
    public abstract class CssClassProviderBase : ICssClassProvider
    {
        public abstract int Count { get; }

        public bool HasValues => Count > 0;

        public abstract IEnumerator<string> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static string CombineWith(string prefix, string option)
        {
            return string.Concat(prefix, Option.OptionSeparator, option);
        }

        public static string CombineWith(ICssClassPrefix prefix, IOption? otherOption)
        {
            if (prefix is null) throw new ArgumentNullException(nameof(prefix));
            if (otherOption is null) return string.Empty;
            return string.Concat(prefix.Prefix, Option.OptionSeparator, otherOption.Value);
        }

        public static string CombineWith(ICssClassPrefix prefix, int? otherOption)
        {
            if (prefix is null) throw new ArgumentNullException(nameof(prefix));
            if (otherOption is null) return string.Empty;
            return string.Concat(prefix.Prefix, Option.OptionSeparator, otherOption.Value);
        }


        public static readonly ICssClassProvider Empty = new EmptyCssClassProvider();
    }
}
