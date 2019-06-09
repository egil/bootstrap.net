using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;
using Egil.RazorComponents.Bootstrap.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egil.RazorComponents.Bootstrap.Layout.Parameters
{
    public abstract class OffsetParameter : CssClassParameterBase, ICssClassParameter
    {
        protected const string OptionPrefix = "offset";

        public static implicit operator OffsetParameter(int number)
        {
            return new OffsetOptionParameter(Number.ToOffsetNumber(number));
        }

        public static implicit operator OffsetParameter(BreakpointWithNumber option)
        {
            option.Number.ValidateAsOffsetBreakpointNumber();
            return new OffsetOptionParameter(option);
        }

        public static implicit operator OffsetParameter(OptionSet<IOffsetOption> set)
        {
            return new OptionSetParameter(set);
        }

        public static implicit operator OffsetParameter(OptionSet<IBreakpointWithNumber> set)
        {
            return new OptionSetParameter(set);
        }

        public static readonly OffsetParameter None = new NoneOffsetParameter();

        class OffsetOptionParameter : OffsetParameter
        {
            private readonly IOption _option;

            public OffsetOptionParameter(IOption option)
            {
                this._option = option;
            }

            public override int Count => 1;

            public override IEnumerator<string> GetEnumerator()
            {
                yield return string.Concat(OptionPrefix, Option.OptionSeparator, _option.Value);
            }
        }

        class OptionSetParameter : OffsetParameter
        {
            private readonly IReadOnlyCollection<string> _set;

            public OptionSetParameter(IOptionSet<IOption> set)
            {
                this._set = set.Select(option =>
                {
                    if (option is BreakpointWithNumber bwn) bwn.Number.ValidateAsOffsetBreakpointNumber();
                    if (option is Number n) n.ValidateAsOffsetNumber();
                    return string.Concat(OptionPrefix, Option.OptionSeparator, option.Value);
                }).ToArray();
            }

            public override int Count => _set.Count;

            public override IEnumerator<string> GetEnumerator() => _set.GetEnumerator();
        }

        class NoneOffsetParameter : OffsetParameter
        {
            public override int Count { get; } = 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
