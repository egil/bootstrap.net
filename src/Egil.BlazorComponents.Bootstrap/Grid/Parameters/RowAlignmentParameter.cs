using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.AlignmentOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{

    public abstract class RowAlignmentParameter : Parameter
    {
        protected const string OptionPrefix = "align-items";

        public static implicit operator RowAlignmentParameter(AlignmentOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator RowAlignmentParameter(OptionSet<IAlignmentOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly RowAlignmentParameter None = new NoneParameter();

        class OptionParameter : RowAlignmentParameter
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

        class OptionSetParameter : RowAlignmentParameter
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

        class NoneParameter : RowAlignmentParameter
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
