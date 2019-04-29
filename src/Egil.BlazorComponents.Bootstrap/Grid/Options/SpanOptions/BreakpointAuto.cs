using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class BreakpointAuto : OptionPair<Breakpoint, AutoOption>, ISpanOption
    {
        public BreakpointAuto(Breakpoint leftOption, AutoOption rightOption) : base(leftOption, rightOption)
        {
        }
        
        public static OptionSet<ISpanOption> operator |(OptionSet<ISpanOption> set, BreakpointAuto option)
        {
            set.Add(option);
            return set;
        }
    }
}