using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("Auto: {Value}")]
    public class AutoOption : ISpanOption
    {
        private const string OptionText = "auto";

        public string Value => OptionText;

        public static BreakpointAuto operator -(Breakpoint breakpoint, AutoOption option)
        {
            return new BreakpointAuto(breakpoint, option);
        }

        public static OptionSet<ISpanOption> operator |(AutoOption option1, int gridNumber)
        {
            return new OptionSet<ISpanOption>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<ISpanOption> operator |(int gridNumber, AutoOption option1)
        {
            return new OptionSet<ISpanOption>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<ISpanOption> operator |(AutoOption option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption>() { option1, option2 };
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<IGridBreakpoint> set, AutoOption option)
        {
            OptionSet<ISpanOption> spanSet = new OptionSet<ISpanOption>(set);
            spanSet.Add(option);
            return spanSet;
        }

        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, AutoOption option)
        {
            set.Add(option);
            return set;
        }
    }
}