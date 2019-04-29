using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointFirst : OptionPair<Breakpoint, FirstOption>, IOrderOption
    {
        public BreakpointFirst(Breakpoint leftOption, FirstOption rightOption) : base(leftOption, rightOption)
        {
        }

        public static OptionSet<IOrderOption> operator |(BreakpointFirst option1, IOrderOption option2)
        {
            var set = new OptionSet<IOrderOption>() { option1, option2 };
            return set;
        }

        public static OptionSet<IOrderOption> operator |(OptionSet<IOrderOption> set, BreakpointFirst option)
        {
            set.Add(option);
            return set;
        }
    }
}