namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public class SharedOptionsSet : BaseOptionSet<ISharedOption>
    {
        public static SharedOptionsSet operator |(SharedOptionsSet set, int number)
        {
            set.Add(new GridNumber<ISharedOption>(number));
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

}