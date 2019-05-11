using System.Diagnostics;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("LastOption: {Value}")]
    public class LastOption : IOrderOption
    {
        private const string OptionText = "last";
        public string Value => OptionText;

        public static BreakpointLast operator -(Breakpoint breakpoint, LastOption option)
        {
            return new BreakpointLast(breakpoint, option);
        }

        public static OptionSet<IOrderOption> operator |(LastOption option1, int gridNumber)
        {
            return new OptionSet<IOrderOption>() { option1, (Number)gridNumber };
        }

        public static OptionSet<IOrderOption> operator |(int gridNumber, LastOption option1)
        {
            return new OptionSet<IOrderOption>() { option1, (Number)gridNumber };
        }

        public static OptionSet<IOrderOption> operator |(LastOption option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }
        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, LastOption option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IBreakpointWithNumber> set, LastOption option)
        {
            OptionSet<IOrderOption> spanSet = new OptionSet<IOrderOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}