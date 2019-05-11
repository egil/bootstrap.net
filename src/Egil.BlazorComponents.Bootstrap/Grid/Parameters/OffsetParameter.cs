using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{
    public abstract class OffsetParameter : Parameter
    {
        protected const string OptionPrefix = "offset";

        public static implicit operator OffsetParameter(int number)
        {
            return new OffsetOptionParameter(Number.ToGridNumber(number));
        }

        public static implicit operator OffsetParameter(BreakpointWithNumber option)
        {
            return new OffsetOptionParameter(option);
        }

        public static implicit operator OffsetParameter(OptionSet<IOffsetOption> set)
        {
            return new OffsetOptionSetParameter(set);
        }

        public static implicit operator OffsetParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new OffsetOptionSetParameter(set);
        }
        
        public static readonly OffsetParameter None = new NoneOffsetParameter();

        class OffsetOptionParameter : OffsetParameter
        {
            private readonly IOption option;

            public OffsetOptionParameter(IOption option)
            {
                this.option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
            }
        }

        class OffsetOptionSetParameter : OffsetParameter
        {
            private readonly IOptionSet<IOption> set;

            public OffsetOptionSetParameter(IOptionSet<IOption> set)
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

        class NoneOffsetParameter : OffsetParameter
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
