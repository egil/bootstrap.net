using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Parameters;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Layout.Parameters
{
    public sealed class VerticalColumnAlignment : ICssClassParameterPrefix
    {
        public string Prefix => "align-self";
    }

    public sealed class VerticalRowAlignment : ICssClassParameterPrefix
    {
        public string Prefix => "align-items";
    }

    public abstract class AlignmentParameter<TParamPrefix> : CssClassParameterBase, ICssClassParameter
        where TParamPrefix : ICssClassParameterPrefix, new()
    {
        protected static readonly TParamPrefix SpacingType = new TParamPrefix();

        public static implicit operator AlignmentParameter<TParamPrefix>(AlignmentOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator AlignmentParameter<TParamPrefix>(BreakpointAlignmentOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator AlignmentParameter<TParamPrefix>(OptionSet<IAlignmentOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly AlignmentParameter<TParamPrefix> None = new NoneParameter();

        class OptionParameter : AlignmentParameter<TParamPrefix>
        {
            private readonly IOption _option;

            public OptionParameter(IOption option)
            {
                _option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(SpacingType.Prefix, Option.OptionSeparator, _option.Value);
            }
        }

        class OptionSetParameter : AlignmentParameter<TParamPrefix>
        {
            private readonly IOptionSet<IOption> _set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                _set = set;
            }

            public override int Count => _set.Count;

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in _set)
                {
                    yield return string.Concat(SpacingType.Prefix, Option.OptionSeparator, option.Value);
                }
            }
        }

        class NoneParameter : AlignmentParameter<TParamPrefix>
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
