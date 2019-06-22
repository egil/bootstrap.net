using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Components.Layout.Parameters
{
    public class SpanParameter : CssClassProviderBase, ICssClassProvider
    {
        private const string OptionPrefix = "col";

        protected string ToOptionValue(IOption option)
        {
            if (option is BreakpointWithNumber bwn) bwn.Number.ValidateAsSpanNumber();
            if (option is Number n) n.ValidateAsSpanNumber();

            return option is DefaultSizeOption
                ? OptionPrefix
                : string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
        }

        public override int Count => 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return OptionPrefix;
        }

        public static implicit operator SpanParameter(int number)
        {
            return new OptionParameter(Number.ToSpanNumber(number));
        }

        public static implicit operator SpanParameter(DefaultSizeOption _)
        {
            return Default;
        }

        public static implicit operator SpanParameter(SizeOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(ExtendedSizeOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(BreakpointWithNumber option)
        {
            option.Number.ValidateAsSpanNumber();
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(AutoOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(BreakpointAuto option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(OptionSet<ISpanOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static implicit operator SpanParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly SpanParameter Default = new SpanParameter();

        class OptionParameter : SpanParameter
        {
            private readonly string _option;

            public OptionParameter(IOption option)
            {
                _option = ToOptionValue(option);
            }

            public override IEnumerator<string> GetEnumerator()
            {
                yield return _option;
            }
        }

        class OptionSetParameter : SpanParameter
        {
            private readonly IReadOnlyCollection<string> _set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                _set = ToOptionValueSet(set);
            }

            private List<string> ToOptionValueSet(IOptionSet<IOption> set)
            {
                var res = new List<string>(set.Count);
                var defaultAdded = false;
                var numberAdded = false;

                foreach (var option in set)
                {
                    if (option is DefaultSizeOption)
                    {
                        if (numberAdded) continue;
                        else defaultAdded = true;
                    }
                    else if (option is Number)
                    {
                        if (defaultAdded) res.Remove(OptionPrefix);
                        numberAdded = true;
                    }

                    res.Add(ToOptionValue(option));
                }

                return res;
            }

            public override int Count => _set.Count;

            public override IEnumerator<string> GetEnumerator() => _set.GetEnumerator();
        }
    }
}
