using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options
{
    [DebuggerDisplay("Last: {Value}")]
    public class BreakpointLast : OptionPair<Breakpoint, LastOption>, IOrderOption
    {
        public BreakpointLast(Breakpoint leftOption, LastOption rightOption) : base(leftOption, rightOption)
        {
        }

        public static OptionSet<IOrderOption> operator |(BreakpointLast option1, int gridNumber)
        {
            return new OptionSet<IOrderOption>() { option1, (Number)gridNumber };
        }

        public static OptionSet<IOrderOption> operator |(int gridNumber, BreakpointLast option1)
        {
            return new OptionSet<IOrderOption>() { option1, (Number)gridNumber };
        }

        public static OptionSet<IOrderOption> operator |(BreakpointLast option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, BreakpointLast option)
        {
            set.Add(option);
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IBreakpointWithNumber> set, BreakpointLast option)
        {
            OptionSet<IOrderOption> spanSet = new OptionSet<IOrderOption>(set);
            spanSet.Add(option);
            return spanSet;
        }
    }
}