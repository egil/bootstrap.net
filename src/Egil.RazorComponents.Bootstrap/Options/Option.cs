using System;
using System.Diagnostics;

namespace Egil.RazorComponents.Bootstrap.Options
{
    [DebuggerDisplay("Option: {Value}")]
    public abstract class Option : IOption, IEquatable<IOption>
    {
        public const string OptionSeparator = "-";

        public abstract string Value { get; }

        public string CombineWith(IOption otherOption) => CombineWith(this, otherOption);

        public static string CombineWith(IOption firstOption, IOption otherOption) => string.Concat(firstOption.Value, OptionSeparator, otherOption.Value);        

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(IOption other)
        {
            if (other is null) return false;
            return Value.Equals(other.Value, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (obj is IOption other)
                return Equals(other);
            else
                return false;
        }

        public static bool operator ==(Option o1, Option o2)
        {
            if (o1 is null && o2 is null) return true;
            if (o1 is null) return false;
            if (o2 is null) return false;
            return o1.Equals(o2);
        }

        public static bool operator !=(Option o1, Option o2)
        {
            return !(o1 == o2);
        }
    }
}