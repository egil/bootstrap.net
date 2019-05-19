using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public class SpanParameter : ParameterBase, IParameter
    {
        protected const string OptionPrefix = "col";

        public override int Count => 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return OptionPrefix;
        }

        public static implicit operator SpanParameter(int number)
        {
            return new OptionParameter(Number.ToSpanNumber(number));
        }

        public static implicit operator SpanParameter(Breakpoint option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(BreakpointWithNumber option)
        {
            option.Number.ValidateAsSpanNumber();
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(AutoOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(BreakpointAuto option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator SpanParameter(OptionSet<ISpanOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static implicit operator SpanParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly SpanParameter Default = new SpanParameter();

        class OptionParameter : SpanParameter
        {
            private readonly IOption option;

            public OptionParameter(IOption option)
            {
                this.option = option;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
            }
        }

        class OptionSetParameter : SpanParameter
        {
            private readonly IReadOnlyCollection<string> set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                this.set = set.Select(option =>
                {
                    if (option is BreakpointWithNumber bwn) bwn.Number.ValidateAsSpanNumber();
                    if (option is Number n) n.ValidateAsSpanNumber();
                    return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
                }).ToArray();
            }

            public override int Count => set.Count;

            public override IEnumerator<string> GetEnumerator() => set.GetEnumerator();

        }
    }
}
