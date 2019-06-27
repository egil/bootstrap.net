using System;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;

namespace Egil.RazorComponents.Bootstrap.Utilities.Sizings
{
    public class NumericSizeParameter<TParamPrefix> : CssClassValueProvider, ICssClassProvider
        where TParamPrefix : ICssClassPrefix, new()
    {
        private static readonly TParamPrefix SizeType = new TParamPrefix();

        private NumericSizeParameter(int? size = null) : base(CombineWith(SizeType, size))
        {
        }

        public static implicit operator NumericSizeParameter<TParamPrefix>(short size)
        {
            return size switch
            {
                25 => Size25,
                50 => Size50,
                75 => Size75,
                100 => Size100,
                _ => throw new ArgumentException($"Valid sizes are 25, 50, 75, or 100. {size} is not allowed. " +
                $"Specify it directly on the compnent using the 'style' attribute, e.g. 'style='width: {size}%;'")
            };
        }

        public static readonly NumericSizeParameter<TParamPrefix> Size25 = new NumericSizeParameter<TParamPrefix>(25);
        public static readonly NumericSizeParameter<TParamPrefix> Size50 = new NumericSizeParameter<TParamPrefix>(50);
        public static readonly NumericSizeParameter<TParamPrefix> Size75 = new NumericSizeParameter<TParamPrefix>(75);
        public static readonly NumericSizeParameter<TParamPrefix> Size100 = new NumericSizeParameter<TParamPrefix>(100);
        public static readonly NumericSizeParameter<TParamPrefix> None = new NumericSizeParameter<TParamPrefix>();
    }
}
