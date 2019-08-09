using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
    public class ColorParameter<TParamPrefix> : CssClassProviderBase, ICssClassProvider
        where TParamPrefix : ICssClassPrefix, new()
    {
        private readonly ColorOption? _option;
        public TParamPrefix ColorPrefix { get; } = new TParamPrefix();

        public override int Count { get; }

        private ColorParameter(ColorOption? option)
        {
            _option = option;
            Count = option is null ? 0 : 1;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            if (Count == 1) yield return CombineWith(ColorPrefix, _option);
            yield break;
        }

        public static implicit operator ColorParameter<TParamPrefix>(ColorOption option)
        {
            return new ColorParameter<TParamPrefix>(option);
        }

        public static readonly ColorParameter<TParamPrefix> None = new ColorParameter<TParamPrefix>(null);
    }
}
