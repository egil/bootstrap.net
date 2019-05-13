using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Options.SpacingOptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public abstract class SpacingParameter<TParamPrefix> : Parameter
        where TParamPrefix : IParameterPrefix, new()
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
            if (!option.Number.IsValidSpacingNumber())
            {
                throw new ArgumentOutOfRangeException(nameof(option), "The specified breakpoint and size is not valid. Size must be between -5 and 5.");
            }
            return new OptionParameter(option);
        }

        public static implicit operator SpacingParameter<TParamPrefix>(OptionSet<ISpacingOption> set)
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
            private readonly string optionValue;

            public OptionParameter(IOption option)
            {
                this.optionValue = ToSpacingValue(option);
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return optionValue;
            }
        }

        class OptionSetParameter : SpacingParameter<TParamPrefix>
        {
            private readonly IOptionSet<IOption> set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                this.set = set;
            }

            public override int Count => set.Count;

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in set)
                {
                    yield return ToSpacingValue(option);
                }
            }
        }

        class NoneParameter : SpacingParameter<TParamPrefix>
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator() { yield break; }
        }
    }

    public sealed class MarginSpacing : IParameterPrefix
    {
        public string Prefix => "m";
    }

    public sealed class PaddingSpacing : IParameterPrefix
    {
        public string Prefix => "p";
    }
}
