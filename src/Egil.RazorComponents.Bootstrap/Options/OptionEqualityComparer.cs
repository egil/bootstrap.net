using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Options
{
    public class OptionEqualityComparer<TOption> : IEqualityComparer<TOption>
        where TOption : IOption
    {
        public bool Equals(TOption option1, TOption option2) => option1.Value.Equals(option2.Value);

        public int GetHashCode(TOption option) => option.Value.GetHashCode();

        public static readonly OptionEqualityComparer<TOption> Instance = new OptionEqualityComparer<TOption>();
    }
}