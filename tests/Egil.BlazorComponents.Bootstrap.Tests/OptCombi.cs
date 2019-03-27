using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Tests.Options
{
    interface IOption<out T> { }
    interface ISharedOption : IOption<ISharedOption>, IOrderOption, ISpanOption { }
    interface IOrderOption : IOption<IOrderOption> { }
    interface ISpanOption : IOption<ISpanOption> { }

    class BaseOptionSet<T> : List<IOption<T>> where T : IOption<T> { }

    class SharedOptionsSet : BaseOptionSet<ISharedOption>
    {
        public static SharedOptionsSet operator |(SharedOptionsSet set, int number)
        {
            set.Add(new Number<ISharedOption>());
            return set;
        }
        public static SharedOptionsSet operator |(SharedOptionsSet set, ISharedOption option)
        {
            set.Add(option);
            return set;
        }
        public static OptionSet<ISpanOption> operator |(SharedOptionsSet set, ISpanOption option)
        {
            return ToSpanOptionSet(set, option);
        }
        public static OptionSet<ISpanOption> operator |(ISpanOption option, SharedOptionsSet set)
        {
            return ToSpanOptionSet(set, option);
        }
        public static OptionSet<IOrderOption> operator |(SharedOptionsSet set, IOrderOption option)
        {
            return ToOrderOptionSet(set, option);
        }
        public static OptionSet<IOrderOption> operator |(IOrderOption option, SharedOptionsSet set)
        {
            return ToOrderOptionSet(set, option);
        }
        static OptionSet<IOrderOption> ToOrderOptionSet(SharedOptionsSet set, IOrderOption option)
        {
            var res = new OptionSet<IOrderOption>();
            res.AddRange(set);
            res.Add(option);
            return res;
        }
        static OptionSet<ISpanOption> ToSpanOptionSet(SharedOptionsSet set, ISpanOption option)
        {
            var res = new OptionSet<ISpanOption>();
            res.AddRange(set);
            res.Add(option);
            return res;
        }
    }

    class OptionSet<T> : BaseOptionSet<T> where T : IOption<T>
    {
        public static OptionSet<T> operator |(OptionSet<T> set, int number)
        {
            set.Add(new Number<T>());
            return set;
        }

        public static OptionSet<T> operator |(OptionSet<T> set, T option)
        {
            set.Add(option);
            return set;
        }
    }

    class Option<T> : IOption<T> where T : IOption<T>
    {
        public static OptionSet<T> operator |(int number, Option<T> option)
        {
            return new OptionSet<T> { new Number<T>(), option };
        }
        public static OptionSet<T> operator |(Option<T> option, int number)
        {
            return new OptionSet<T> { new Number<T>(), option };
        }
        public static OptionSet<T> operator |(Option<T> option1, T option2)
        {
            return new OptionSet<T> { option1, option2 };
        }
    }

    class SpanOption : Option<ISpanOption>, ISpanOption
    {
    }

    class OrderOption : Option<IOrderOption>, IOrderOption
    {
    }

    class SharedOption : ISharedOption
    {
        public static SharedOptionsSet operator |(int number, SharedOption option)
        {
            return new SharedOptionsSet { new Number<ISharedOption>(), option };
        }
        public static SharedOptionsSet operator |(SharedOption option, int number)
        {
            return new SharedOptionsSet { new Number<ISharedOption>(), option };
        }
        public static SharedOptionsSet operator |(SharedOption option1, ISharedOption option2)
        {
            return new SharedOptionsSet { option1, option2 };
        }
        public static OptionSet<ISpanOption> operator |(SharedOption option1, ISpanOption option2)
        {
            return new OptionSet<ISpanOption> { option1, option2 };
        }
        public static OptionSet<IOrderOption> operator |(SharedOption option1, IOrderOption option2)
        {
            return new OptionSet<IOrderOption> { option1, option2 };
        }
    }

    class Number<T> : IOption<T> { }

    class BreakpointNumber : SharedOption
    {
    }

    class Breakpoint : SpanOption
    {
    }

    class BreakpointAuto : SpanOption
    {
    }

    class Auto : SpanOption
    {
    }

    class First : OrderOption
    {
    }

    class Last : OrderOption
    {
    }

    class BreakpointFirst : OrderOption
    {
    }

    class BreakpointLast : OrderOption
    {
    }

}