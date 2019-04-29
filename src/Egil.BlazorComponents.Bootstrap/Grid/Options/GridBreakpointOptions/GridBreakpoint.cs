using System.Diagnostics;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("GridBreakpoint: {Value}")]

    public class GridBreakpoint : OptionPair<Breakpoint, GridNumber>, IGridBreakpoint, ISpanOption, IOrderOption, IOffsetOption
    {
        public GridBreakpoint(Breakpoint leftOption, GridNumber rightOption) : base(leftOption, rightOption)
        {
        }

        public static OptionSet<IGridBreakpoint> operator |(GridBreakpoint option1, int gridNumber)
        {
            return new OptionSet<IGridBreakpoint>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<IGridBreakpoint> operator |(int gridNumber, GridBreakpoint option1)
        {
            return new OptionSet<IGridBreakpoint>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<IGridBreakpoint> operator |(GridBreakpoint option1, IGridBreakpoint option2)
        {
            return new OptionSet<IGridBreakpoint>() { option1, option2 };
        }

        public static OptionSet<IOrderOption> operator |(GridBreakpoint option1, IOrderOption option2)
        {
            return new OptionSet<IOrderOption>() { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(GridBreakpoint option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<IGridBreakpoint> operator |(OptionSet<IGridBreakpoint> set, GridBreakpoint option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, GridBreakpoint option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, GridBreakpoint option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOffsetOption> operator |(OptionSet<IOffsetOption> set, GridBreakpoint option)
        {
            set.Add(option);
            return set;
        }
    }
}