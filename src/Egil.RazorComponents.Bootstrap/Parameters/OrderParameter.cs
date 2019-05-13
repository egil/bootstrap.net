using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public abstract class OrderParameter : ParameterBase, IParameter
    {
        protected const string OptionPrefix = "order";

        public static implicit operator OrderParameter(int number)
        {
            return new OrderOptionParameter(Number.ToGridNumber(number));
        }

        public static implicit operator OrderParameter(FirstOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(LastOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointWithNumber option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointFirst option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointLast option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(OptionSet<IOrderOption> set)
        {
            return new OrderOptionSetParameter(set);
        }

        public static implicit operator OrderParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new OrderOptionSetParameter(set);
        }

        public static readonly OrderParameter None = new NoneOrderParameter();

        class OrderOptionParameter : OrderParameter
        {
            private readonly IOption option;

            public OrderOptionParameter(IOption option)
            {
                this.option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
            }
        }

        class OrderOptionSetParameter : OrderParameter
        {
            private readonly IOptionSet<IOption> set;

            public OrderOptionSetParameter(IOptionSet<IOption> set)
            {
                this.set = set;
            }

            public override int Count => set.Count;

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in set)
                {
                    yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
                }
            }
        }

        class NoneOrderParameter : OrderParameter
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator() { yield break; }
        }
    }
}
