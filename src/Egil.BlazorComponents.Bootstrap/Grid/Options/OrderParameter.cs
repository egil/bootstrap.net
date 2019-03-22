using Egil.BlazorComponents.Bootstrap.Grid.Options;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class OrderParameter : Parameter
    {
        private const string OptionPrefix = "order";

        public virtual string CssClass => OptionPrefix;

        public static implicit operator OrderParameter(int number)
        {
            return new OrderIndexParameter(number);
        }

        public static implicit operator OrderParameter(FirstOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(LastOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointWithNumber bpw)
        {
            return new OrderOptionParameter(bpw);
        }

        public static implicit operator OrderParameter(BreakpointWithFirstOption option)
        {
            return new OrderOptionParameter(option);
        }

        public static implicit operator OrderParameter(BreakpointWithLastOption option)
        {
            return new OrderOptionParameter(option);
        }

        class OrderIndexParameter : OrderParameter
        {
            private readonly int index;

            public OrderIndexParameter(int index)
            {
                ValidateGridNumberInRange(index);
                this.index = index;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, index);
        }

        class OrderOptionParameter : OrderParameter
        {
            private readonly Option option;

            public OrderOptionParameter(Option option)
            {
                this.option = option;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, option.CssClass);
        }
    }
}