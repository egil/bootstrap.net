using Egil.BlazorComponents.Bootstrap.Grid.Options;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{

    public abstract class OrderParameter : Parameter
    {
        protected const string OptionPrefix = "order";

        public static readonly OrderParameter None = new NoneOrderParameter();

        public static implicit operator OrderParameter(int number)
        {
            return new OrderOptionParameter(new GridNumber(number));
        }

        public static implicit operator OrderParameter(FirstOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(LastOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(GridBreakpoint option)
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

        public static implicit operator OrderParameter(OptionSet<IGridBreakpoint> set)
        {
            return new OrderOptionSetParameter(set);
        }

        class OrderOptionParameter : OrderParameter
        {
            private readonly IOrderOption option;

            public OrderOptionParameter(IOrderOption option)
            {
                this.option = option;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, OptionSeparator, option.Value);
            }
        }

        class OrderOptionSetParameter : OrderParameter
        {
            private readonly IOptionSet<IOption> set;

            public OrderOptionSetParameter(IOptionSet<IOption> set)
            {
                this.set = set;
            }
            
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
            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
