using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{

    public class BreakpointLast : OptionPair<Breakpoint, LastOption>, IOrderOption
    {
        public BreakpointLast(Breakpoint leftOption, LastOption rightOption) : base(leftOption, rightOption)
        {
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
    }
}