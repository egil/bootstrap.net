using Egil.BlazorComponents.Bootstrap.Grid.Options;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{
    public class SpanParameter : Parameter
    {
        protected const string OptionPrefix = "col";

        public static readonly SpanParameter Default = new SpanParameter();

        public override IEnumerator<string> GetEnumerator()
        {
            yield return OptionPrefix;
        }

        public static implicit operator SpanParameter(int number)
        {
            return new SpanOptionParameter(new Number<ISpanOption>(number));
        }

        public static implicit operator SpanParameter(SharedOption option)
        {
            return new SpanOptionParameter(option);
        }

        public static implicit operator SpanParameter(SpanOption option)
        {
            return new SpanOptionParameter(option);
        }


        public static implicit operator SpanParameter(SharedOptionsSet set)
        {
            return new SpanOptionSetParameter<ISharedOption>(set);
        }

        public static implicit operator SpanParameter(OptionSet<ISpanOption> set)
        {
            return new SpanOptionSetParameter<ISpanOption>(set);
        }

        class SpanOptionParameter : SpanParameter
        {
            private readonly IOption<ISpanOption> option;

            public SpanOptionParameter(IOption<ISpanOption> option)
            {
                this.option = option;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, OptionSeparator, option.Value);
            }
        }

        class SpanOptionSetParameter<T> : SpanParameter where T : IOption<T>
        {
            private readonly BaseOptionSet<T> set;

            public SpanOptionSetParameter(BaseOptionSet<T> set)
            {
                this.set = set;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in set)
                {
                    yield return string.Concat(OptionPrefix, OptionSeparator, option.Value);
                }
            }
        }
    }
}
