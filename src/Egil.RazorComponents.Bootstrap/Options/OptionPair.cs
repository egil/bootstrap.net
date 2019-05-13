using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options.AlignmentOptions
{
    [DebuggerDisplay("OptionPair: {Value}")]
    public class OptionPair<TLeftOption, TRightOption>
        where TLeftOption : IOption
        where TRightOption : IOption
    {
        private readonly TLeftOption leftOption;
        private readonly TRightOption rightOption;

        public OptionPair(TLeftOption leftOption, TRightOption rightOption)
        {
            this.leftOption = leftOption;
            this.rightOption = rightOption;
        }

        public string Value => string.Concat(leftOption.Value, Option.OptionSeparator, rightOption.Value);
    }
}
