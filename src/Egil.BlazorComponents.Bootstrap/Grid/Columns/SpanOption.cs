using System;
using System.Diagnostics;
using System.Linq;

namespace Egil.BlazorComponents.Bootstrap.Grid.Columns
{
    public class AutoSpanModifier
    {
    }
    
    [DebuggerDisplay("SpanOption: {CssClass}")]
    public class SpanOption : SpanOptionBase, IEquatable<SpanOption>
    {
        public BreakpointType Breakpoint { get; }

        public string BreakpointCss => Breakpoint switch
        {
            BreakpointType.None => "col",
            BreakpointType.ExtraSmall => "col",
            BreakpointType.Small => "col-sm",
            BreakpointType.Medium => "col-md",
            BreakpointType.Large => "col-lg",
            BreakpointType.ExtraLarge => "col-xl"
        };

        public int? Width { get; }

        public bool AutoModifier { get; }

        private SpanOption(BreakpointType breakpoint, int? width = null, bool autoModifier = false)
        {
            if (width.HasValue && (width.Value < 1 || width.Value > 12))
                throw new ColumnWidthOutOfRangeException(width.Value);

            Breakpoint = breakpoint;
            Width = width;
            AutoModifier = autoModifier;
        }

        public override string CssClass => Width.HasValue
            ? string.Concat(BreakpointCss, "-", Width)
            : AutoModifier 
            ? string.Concat(BreakpointCss, "-auto")
            : BreakpointCss;

        public override int GetHashCode()
        {
            return CssClass.GetHashCode();
        }

        public bool Equals(SpanOption other)
        {
            return CssClass.Equals(other.CssClass);
        }

        public static implicit operator SpanOption(int width)
        {
            return Default - width;
        }

        public static SpanOption operator -(SpanOption option, int width)
        {
            return new SpanOption(option.Breakpoint, width);
        }

        public static SpanOption operator -(SpanOption option, AutoSpanModifier _)
        {
            return new SpanOption(option.Breakpoint, autoModifier: true);
        }

        public static SpanOptionBase operator |(SpanOption option, SpanOption anotherOption)
        {
            return new SpanOptionComposite(option, anotherOption);
        }

        public static readonly AutoSpanModifier Auto = new AutoSpanModifier();
        public static readonly SpanOption Default = new SpanOption(BreakpointType.None);
        public static readonly SpanOption Small = new SpanOption(BreakpointType.Small);
        public static readonly SpanOption Medium = new SpanOption(BreakpointType.Medium);
        public static readonly SpanOption Large = new SpanOption(BreakpointType.Large);
        public static readonly SpanOption ExtraLarge = new SpanOption(BreakpointType.ExtraLarge);
        public static readonly SpanOption SM = Small;
        public static readonly SpanOption MD = Medium;
        public static readonly SpanOption LG = Large;
        public static readonly SpanOption XL = ExtraLarge;
    }
}
