using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("FirstOption: {Value}")]
    public class FirstOption : IOrderOption
    {
        private const string OptionText = "first";
        public string Value => OptionText;

        public static BreakpointFirst operator -(Breakpoint breakpoint, FirstOption option)
        {
            return new BreakpointFirst(breakpoint, option);
        }

        public static OptionSet<IOrderOption> operator |(FirstOption option1, int gridNumber)
        {
            return new OptionSet<IOrderOption>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<IOrderOption> operator |(int gridNumber, FirstOption option1)
        {
            return new OptionSet<IOrderOption>() { option1, (GridNumber)gridNumber };
        }

        public static OptionSet<IOrderOption> operator |(FirstOption option1, IOrderOption option2)
        {
            return new OptionSet<IOrderOption>() { option1, option2 };
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, FirstOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IGridBreakpoint> set, FirstOption option)
        {
            OptionSet<IOrderOption> spanSet = new OptionSet<IOrderOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}