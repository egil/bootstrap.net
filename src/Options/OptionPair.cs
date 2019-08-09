using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.AlignmentOptions
{
    [DebuggerDisplay("OptionPair: {Value}")]
    public class OptionPair<TLeftOption, TRightOption> : Option
        where TLeftOption : IOption
        where TRightOption : IOption
    {
        public OptionPair(TLeftOption leftOption, TRightOption rightOption)
        {
            Value = Option.CombineWith(leftOption, rightOption);
        }

        public override string Value { get; }
    }
}
