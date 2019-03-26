using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public interface IOption { }

    public interface INumberOption : IOption { }

    public class OptSet<T1> : List<IOption>
        where T1 : IOption
    {
    }
    public class OptSet<T1, T2> : List<IOption>
        where T1 : IOption
        where T2 : IOption
    {
    }

    public class OptSet<T1, T2, T3> : List<IOption>
        where T1 : IOption
        where T2 : IOption
        where T3 : IOption
    {
    }

    public class OptSet<T1, T2, T3, T4> : List<IOption>
    where T1 : IOption
    where T2 : IOption
    where T3 : IOption
        where T4 : IOption
    {
    }

    public class Number : INumberOption
    {
        private int number;

        public Number(int number)
        {
            this.number = number;
        }

        public static implicit operator Number(int number)
        {
            return new Number(number);
        }

        public static Number operator -(int number, Number colnum)
        {
            return new Number(number);
        }

        public static Number operator -(Number colnum, int number)
        {
            return new Number(number);
        }

        public static OptSet<Number, XOpt> operator |(Number option1, XOpt option2)
        {
            return new OptSet<Number, XOpt> { option1, option2 };
        }
    }

    public class XOpt : IOption
    {
        public static OptSet<Number, XOpt> operator |(int number, XOpt option2)
        {
            return new OptSet<Number, XOpt> { new Number(number), option2 };
        }

        public static OptSet<XOpt> operator |(XOpt option1, XOpt option2)
        {
            return new OptSet<XOpt> { option1, option2 };
        }

        public static OptSet<XOpt> operator |(OptSet<XOpt> set, XOpt option2)
        {
            return set;
        }

        public static OptSet<Number, XOpt> operator |(OptSet<Number, XOpt> set, XOpt option2)
        {
            set.Add(option2);
            return set;
        }

        public static OptSet<Number, XOpt, YOpt> operator |(OptSet<Number, YOpt> set, XOpt option2)
        {
            set.Add(option2);
            return new OptSet<Number, XOpt, YOpt>();
        }

    }

    public class YOpt : IOption
    {
        public static OptSet<Number, YOpt> operator |(int number, YOpt option2)
        {
            return new OptSet<Number, YOpt> { new Number(number), option2 };
        }

        public static OptSet<Number, YOpt> operator |(OptSet<Number, YOpt> set, YOpt option2)
        {
            set.Add(option2);
            return set;
        }

        public static OptSet<Number, XOpt, YOpt> operator |(OptSet<Number, XOpt> set, YOpt option2)
        {
            set.Add(option2);
            return new OptSet<Number, XOpt, YOpt>();
        }
    }

    public class ZOpt : IOption
    {
        public static OptSet<Number, XOpt, YOpt, ZOpt> operator |(OptSet<Number, XOpt, YOpt> set, ZOpt option2)
        {
            set.Add(option2);
            return new OptSet<Number, XOpt, YOpt, ZOpt>();
        }
    }

    public static class Statics
    {
        public static readonly XOpt X = new XOpt();
        public static readonly YOpt Y = new YOpt();
        public static readonly ZOpt Z = new ZOpt();
    }
}
