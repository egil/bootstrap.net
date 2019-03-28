using Egil.BlazorComponents.Bootstrap.Grid.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static implicit operator OrderParameter(First option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(Last option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointNumber bpw)
        {
            return new OrderOptionParameter(bpw);
        }

        public static implicit operator OrderParameter(BreakpointFirst option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointLast option)
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

        class OrderOptionSetParameter<T> : OrderParameter
            where T : IOption<T>
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
