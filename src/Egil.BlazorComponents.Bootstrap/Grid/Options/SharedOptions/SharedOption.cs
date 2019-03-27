using System;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class SharedOption : ISharedOption
    {
        public abstract string Value { get; }

        public static SharedOptionsSet operator |(int number, SharedOption option)
        {
            return new SharedOptionsSet { new Number<ISharedOption>(number), option };
        }
        public static SharedOptionsSet operator |(SharedOption option, int number)
        {
            return new SharedOptionsSet { new Number<ISharedOption>(number), option };
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
}