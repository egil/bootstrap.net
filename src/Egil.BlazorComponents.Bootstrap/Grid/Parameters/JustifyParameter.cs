using System.Collections.Generic;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{
    public abstract class HorizontalAlignmentParameter : Parameter
    {
        protected const string OptionPrefix = "justify-content";

        public static implicit operator HorizontalAlignmentParameter(JustifyOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator HorizontalAlignmentParameter(AlignmentOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator HorizontalAlignmentParameter(OptionSet<IAlignmentOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static implicit operator HorizontalAlignmentParameter(OptionSet<IJustifyOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly HorizontalAlignmentParameter None = new NoneParameter();

        class OptionParameter : HorizontalAlignmentParameter
        {
            private readonly IOption option;

            public OptionParameter(IOption option)
            {
                this.option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
            }
        }

        class OptionSetParameter : HorizontalAlignmentParameter
        {
            private readonly IOptionSet<IOption> set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                this.set = set;
            }

            public override int Count => set.Count;

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in set)
                {
                    yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
                }
            }
        }

        class NoneParameter : HorizontalAlignmentParameter
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
