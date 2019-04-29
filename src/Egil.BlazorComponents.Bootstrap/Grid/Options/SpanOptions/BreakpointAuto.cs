using System.Diagnostics;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("Auto: {Value}")]
    public class BreakpointAuto : OptionPair<Breakpoint, AutoOption>, ISpanOption
    {
        public BreakpointAuto(Breakpoint leftOption, AutoOption rightOption) : base(leftOption, rightOption)
        {
        }

        public static OptionSet<ISpanOption> operator |(BreakpointAuto option1, int gridNumber)
        {
            return new OptionSet<ISpanOption>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<ISpanOption> operator |(int gridNumber, BreakpointAuto option1)
        {
            return new OptionSet<ISpanOption>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<ISpanOption> operator |(BreakpointAuto option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, BreakpointAuto option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IGridBreakpoint> set, BreakpointAuto option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}