using Egil.BlazorComponents.Bootstrap.Grid.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{    
    public class SpanParameter : Parameter
    {
        private const string OptionPrefix = "col";

        public virtual string CssClass => OptionPrefix;

        public static implicit operator SpanParameter(int width)
        {
            return new SpanWidthParameter(width);
        }

        public static implicit operator SpanParameter(Breakpoint breakpoint)
        {
            return new SpanOptionParameter(breakpoint);
        }

        public static implicit operator SpanParameter(BreakpointWithNumber breakpoint)
        {
            return new SpanOptionParameter(breakpoint);
        }

        public static implicit operator SpanParameter(BreakpointWithAutoOption breakpoint)
        {
            return new SpanOptionParameter(breakpoint);
        }

        public static implicit operator SpanParameter(OptionSet options)
        {
            return new SpanParameterOptionSet(options);
        }

        private class SpanWidthParameter : SpanParameter
        {
            private readonly int width;

            public SpanWidthParameter(int width)
            {
                this.width = width;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, width);
        }

        private class SpanOptionParameter : SpanParameter
        {
            private readonly Option option;

            public SpanOptionParameter(Option option)
            {
                this.option = option;
            }

            public override string CssClass => string.Concat(base.CssClass, OptionSeparator, option.CssClass);
        }

        private class SpanParameterOptionSet : SpanParameter
        {
            private readonly OptionSet options;

            public SpanParameterOptionSet(OptionSet options)
            {
                this.options = options;
            }

            public override string CssClass
            {
                get
                {
                    var cssClasses = options.Select(option => string.Concat(base.CssClass, OptionSeparator, option.CssClass));
                    return string.Join(" ", cssClasses);
                }
            }
        }
    }
}
