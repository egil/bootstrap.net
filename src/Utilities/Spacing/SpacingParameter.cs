using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Utilities.Spacing
{
    public abstract class SpacingParameter<TParamPrefix> : CssClassProviderBase, ICssClassProvider
        where TParamPrefix : ICssClassPrefix, new()
    {
        private static readonly TParamPrefix SpacingType = new TParamPrefix();

        public static implicit operator SpacingParameter<TParamPrefix>(int size)
        {
            return new OptionParameter(Number.ToSpacingNumber(size));
        }

        public static implicit operator SpacingParameter<TParamPrefix>(SpacingOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpacingParameter<TParamPrefix>(AutoOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpacingParameter<TParamPrefix>(BreakpointAuto option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpacingParameter<TParamPrefix>(BreakpointWithNumber option)
        {
            option.Number.ValidateAsSpacingNumber();
            return new OptionParameter(option);
        }

        public static implicit operator SpacingParameter<TParamPrefix>(OptionSet<ISpacingOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static implicit operator SpacingParameter<TParamPrefix>(OptionSet<IBreakpointWithNumber> set)
        {
            return new OptionSetParameter(set);
        }

        private static string ToSpacingValue(IOption option)
        {
            return option is SpacingOption
                ? string.Concat(SpacingType.Prefix, option.Value)
                : string.Concat(SpacingType.Prefix, Option.OptionSeparator, option.Value);
        }

        public static readonly SpacingParameter<TParamPrefix> None = new NoneParameter();

        class OptionParameter : SpacingParameter<TParamPrefix>
        {
            private readonly string _optionValue;

            public OptionParameter(IOption option)
            {
                _optionValue = ToSpacingValue(option);
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return _optionValue;
            }
        }

        class OptionSetParameter : SpacingParameter<TParamPrefix>
        {
            private readonly IReadOnlyCollection<string> _set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                this._set = set.Select(option =>
                {
                    if (option is BreakpointWithNumber bwn) bwn.Number.ValidateAsSpacingNumber();
                    if (option is Number n) n.ValidateAsSpacingNumber();
                    return ToSpacingValue(option);
                }).ToArray();
            }

            public override int Count => _set.Count;

            public override IEnumerator<string> GetEnumerator() => _set.GetEnumerator();
        }

        class NoneParameter : SpacingParameter<TParamPrefix>
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator() { yield break; }
        }
    }
}
