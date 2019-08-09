using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Components.Layout.Parameters
{
    public abstract class OrderParameter : CssClassProviderBase, ICssClassProvider
    {
        protected const string OptionPrefix = "order";

        public static implicit operator OrderParameter(int number)
        {
            return new OptionParameter(Number.ToOrderNumber(number));
        }

        public static implicit operator OrderParameter(FirstOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator OrderParameter(LastOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointWithNumber option)
        {
            option.Number.ValidateAsOrderNumber();
            return new OptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointFirst option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointLast option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator OrderParameter(OptionSet<IOrderOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static implicit operator OrderParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly OrderParameter None = new NoneParameter();

        class OptionParameter : OrderParameter
        {
            private readonly IOption _option;

            public OptionParameter(IOption option)
            {
                _option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, _option.Value);
            }
        }

        class OptionSetParameter : OrderParameter
        {
            private readonly IReadOnlyCollection<string> _set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                _set = set.Select(option =>
                    {
                        if (option is BreakpointWithNumber bwn) bwn.Number.ValidateAsOrderNumber();
                        if (option is Number n) n.ValidateAsOrderNumber();
                        return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
                    }).ToArray();
            }

            public override int Count => _set.Count;

            public override IEnumerator<string> GetEnumerator() => _set.GetEnumerator();
        }

        class NoneParameter : OrderParameter
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator() { yield break; }
        }
    }
}
