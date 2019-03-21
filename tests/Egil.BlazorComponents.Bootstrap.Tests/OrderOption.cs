namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public abstract class OrderOption : Option
    {
        protected static readonly string OptionPrefix = "order-";

        public static implicit operator OrderOption(int number)
        {
            return new OrderIndexOption(number);
        }

        public static implicit operator OrderOption(BreakpointWithSpan bpw)
        {
            return new OrderBreakpointOption(bpw);
        }

        public static implicit operator OrderOption(FirstOption option)
        {
            return new OrderOptionOption(option);
        }

        public static implicit operator OrderOption(LastOption option)
        {
            return new OrderOptionOption(option);
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
                ValidateGridNumber(index);
                this.index = index;
            }

            public override string CssClass => OptionPrefix + index;
        }

        class OrderBreakpointOption : OrderOption
        {
            private readonly BreakpointWithSpan breakpoint;

            public OrderBreakpointOption(BreakpointWithSpan breakpoint)
            {
                this.breakpoint = breakpoint;
            }

            public override string CssClass => OptionPrefix + breakpoint.CssClass;
        }

        class OrderOptionOption : OrderOption
        {
            private readonly Option option;

            public OrderOptionOption(Option option)
            {
                this.option = option;
            }

            public override string CssClass => OptionPrefix + option.CssClass;
        }
    }

}