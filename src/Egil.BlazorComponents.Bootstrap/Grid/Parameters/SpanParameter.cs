using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{
    public class SpanParameter : Parameter
    {
        protected const string OptionPrefix = "col";

        public override int Count => 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return OptionPrefix;
        }

        public static implicit operator SpanParameter(int number)
        {
            return new SpanOptionParameter(Number.ToGridNumber(number));
        }

        public static implicit operator SpanParameter(Breakpoint option)
        {
            return new SpanOptionParameter(option);
        }

        public static implicit operator SpanParameter(BreakpointWithNumber option)
        {
            return new SpanOptionParameter(option);
        }

        public static implicit operator SpanParameter(AutoOption option)
        {
            return new SpanOptionParameter(option);
        }

        public static implicit operator SpanParameter(BreakpointAuto option)
        {
            return new SpanOptionParameter(option);
        }

        public static implicit operator SpanParameter(OptionSet<ISpanOption> set)
        {
            return new SpanOptionSetParameter(set);
        }

        public static implicit operator SpanParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new SpanOptionSetParameter(set);
        }

        public static readonly SpanParameter Default = new SpanParameter();

        class SpanOptionParameter : SpanParameter
        {
            private readonly IOption option;

            public SpanOptionParameter(IOption option)
            {
                this.option = option;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
            }
        }

        class SpanOptionSetParameter : SpanParameter
        {
            private readonly IOptionSet<IOption> set;

            public SpanOptionSetParameter(IOptionSet<IOption> set)
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
    }
}
