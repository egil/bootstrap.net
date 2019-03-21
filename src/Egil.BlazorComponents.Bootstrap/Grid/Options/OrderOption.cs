using Egil.BlazorComponents.Bootstrap.Grid.Options;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class OrderOption : Option
    {
        private const string OptionPrefix = "order";

        public override string CssClass => OptionPrefix;

        public static implicit operator OrderOption(int number)
        {
            return new OrderIndexOption(number);
        }

        public static implicit operator OrderOption(FirstOption option)
        {
            return new OrderOptionOption(option);
        }

        public static implicit operator OrderOption(LastOption option)
        {
            return new OrderOptionOption(option);
        }

        public static implicit operator OrderOption(BreakpointWithNumber bpw)
        {
            return new OrderOptionOption(bpw);
        }

        public static implicit operator OrderOption(BreakpointWithFirstOption option)
        {
            return new OrderOptionOption(option);
        }

        public static implicit operator OrderOption(BreakpointWithLastOption option)
        {
            return new OrderOptionOption(option);
        }

        class OrderIndexOption : OrderOption
        {
            private readonly int index;

            public OrderIndexOption(int index)
            {
                ValidateGridNumberInRange(index);
                this.index = index;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, index);
        }

        class OrderOptionOption : OrderOption
        {
            private readonly Option option;

            public OrderOptionOption(Option option)
            {
                this.option = option;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, option.CssClass);
        }
    }
}