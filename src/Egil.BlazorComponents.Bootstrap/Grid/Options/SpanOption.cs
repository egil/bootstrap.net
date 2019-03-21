using Egil.BlazorComponents.Bootstrap.Grid.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class SpanOption : Option
    {
        private const string OptionPrefix = "col";

        public override string CssClass => OptionPrefix;

        public static implicit operator SpanOption(int width)
        {
            return new SpanWidthOption(width);
        }

        public static implicit operator SpanOption(Breakpoint breakpoint)
        {
            return new SpanOptionOption(breakpoint);
        }

        public static implicit operator SpanOption(BreakpointWithNumber breakpoint)
        {
            return new SpanOptionOption(breakpoint);
        }

        public static implicit operator SpanOption(BreakpointWithAutoOption breakpoint)
        {
            return new SpanOptionOption(breakpoint);
        }

        public static IntermediateOptionSet operator |(SpanOption breakpoint, int number)
        {
            return new IntermediateOptionSet(breakpoint, number);
        }

        private class SpanWidthOption : SpanOption
        {
            private readonly int width;

            public SpanWidthOption(int width)
            {
                this.width = width;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, width);
        }

        private class SpanOptionOption : SpanOption
        {
            private readonly Option option;

            public SpanOptionOption(Option option)
            {
                this.option = option;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, option.CssClass);
        }
    }
}
