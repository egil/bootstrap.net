using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.AlignmentOptions;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Layout.Parameters
{
    public abstract class HorizontalAlignmentParameter : CssClassProviderBase, ICssClassProvider
    {
        protected const string OptionPrefix = "justify-content";

        public static implicit operator HorizontalAlignmentParameter(JustifyOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator HorizontalAlignmentParameter(BreakpointAlignmentOption option)
        {
            return new OptionParameter(option);
        }

        public static implicit operator HorizontalAlignmentParameter(BreakpointJustifyOption option)
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
            private readonly IOption _option;

            public OptionParameter(IOption option)
            {
                this._option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, _option.Value);
            }
        }

        class OptionSetParameter : HorizontalAlignmentParameter
        {
            private readonly IOptionSet<IOption> _set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                this._set = set;
            }

            public override int Count => _set.Count;

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (var option in _set)
                {
                    yield return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
                }
            }
        }

        class NoneParameter : HorizontalAlignmentParameter
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
