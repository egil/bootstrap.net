using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
    public class ColorParameter<TParamPrefix> : CssClassProviderBase, ICssClassProvider
        where TParamPrefix : ICssClassPrefix, new()
    {
        private static readonly TParamPrefix ColorPrefix = new TParamPrefix();

        private readonly string? _value;

        private ColorParameter() { }

        private ColorParameter(ColorOption option)
        {
            _value = string.Concat(ColorPrefix.Prefix, Option.OptionSeparator, option.Value);
        }

        public override int Count { get; } = 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return _value!;
        }

        public static implicit operator ColorParameter<TParamPrefix>(ColorOption option)
        {
            return new ColorParameter<TParamPrefix>(option);
        }

        public static readonly ColorParameter<TParamPrefix> None = new NoneParameter();

        class NoneParameter : ColorParameter<TParamPrefix>
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
