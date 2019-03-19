using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Columns
{
    public abstract class SpanOptionBase : IColumnOption
    {
        public abstract string CssClass { get; }

        public static implicit operator SpanOptionBase(int width)
        {
            return SpanOption.Default - width;
        }

        public static implicit operator SpanOptionBase(AutoSpanModifier autoSpanModifier)
        {
            return SpanOption.Default - autoSpanModifier;
        }

        public static SpanOption operator -(SpanOptionBase option, int width)
        {
            if (option is SpanOption colSpanOption)
                return colSpanOption - width;

            if (option is SpanOptionComposite)
                throw new InvalidOperationException("Span contains two or more options. " +
                    "You cannot change or set the width on more than one option.");

            throw new InvalidOperationException($"Unknown subtype of {nameof(SpanOptionBase)}");
        }

        public static SpanOption operator -(SpanOptionBase option, AutoSpanModifier autoModifier)
        {
            if (option is SpanOption colSpanOption)
                return colSpanOption - autoModifier;

            //if (option is SpanOptionComposite)
            //    throw new InvalidOperationException("ColSpan contains two or more options. " +
            //        "You cannot change or set the width on more than one option.");

            throw new InvalidOperationException($"Unknown subtype of {nameof(SpanOptionBase)}");
        }

        public static SpanOptionBase operator |(SpanOptionBase option, SpanOption anotherOption)
        {
            if (option is SpanOptionComposite composite)
                return composite | anotherOption;

            if (option is SpanOption opt)
                return opt | anotherOption;

            throw new InvalidOperationException($"Unknown subtype of {nameof(SpanOptionBase)}");
        }
    }
}
