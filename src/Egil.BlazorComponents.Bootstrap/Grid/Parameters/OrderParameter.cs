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
            return new OrderOptionParameter(new Number<IOrderOption>(number));
        }

        public static implicit operator OrderParameter(SharedOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(OrderOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(SharedOptionsSet set)
        {
            return new OrderOptionSetParameter<ISharedOption>(set);
        }

        public static implicit operator OrderParameter(OptionSet<IOrderOption> set)
        {
            return new OrderOptionSetParameter<IOrderOption>(set);
        }

        class OrderOptionParameter : OrderParameter
        {
            private readonly IOption<IOrderOption> option;

            public OrderOptionParameter(IOption<IOrderOption> option)
            {
                this.option = option;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, OptionSeparator, option.Value);
            }
        }

        class OrderOptionSetParameter<T> : OrderParameter where T : IOption<T>
        {
            private readonly BaseOptionSet<T> set;

            public OrderOptionSetParameter(BaseOptionSet<T> set)
            {
                this.set = set;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in set)
                {
                    yield return string.Concat(OptionPrefix, OptionSeparator, option.Value);
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
