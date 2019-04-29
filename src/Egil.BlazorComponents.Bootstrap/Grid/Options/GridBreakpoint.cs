using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class GridBreakpoint : OptionPair<Breakpoint, GridNumber>,  IGridBreakpoint, ISpanOption, IOrderOption, IOffsetOption
    {
        public GridBreakpoint(Breakpoint leftOption, GridNumber rightOption) : base(leftOption, rightOption)
        {
        }

        public static OptionSet<IGridBreakpoint> operator |(GridBreakpoint option1, IGridBreakpoint option2)
        {
            return new OptionSet<IGridBreakpoint>() { option1, option2 };
        }

        public static OptionSet<IOrderOption> operator |(GridBreakpoint option1, IOrderOption option2)
        {
            return new OptionSet<IOrderOption>() { option1, option2 };
        }
    }
}