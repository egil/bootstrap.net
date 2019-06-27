using System;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.CommonOptions;

namespace Egil.RazorComponents.Bootstrap.Utilities.Sizings
{
    public class SizeParamter<TParamPrefix> : CssClassValueProvider, ICssClassProvider
        where TParamPrefix : ICssClassPrefix, new()
    {
        private static readonly TParamPrefix SizeType = new TParamPrefix();

        private SizeParamter(SizeOption? size = null) : base(CombineWith(SizeType, size)) { }

        public static implicit operator SizeParamter<TParamPrefix>(SizeOption size)
        {
            if (size == SizeOption.Small) return Small;
            if (size == ExtendedSizeOption.Medium) return Medium;
            if (size == SizeOption.Large) return Large;
            else throw new InvalidOperationException("Unknown SizeOption type.");
        }

        public static SizeParamter<TParamPrefix> Small = new SizeParamter<TParamPrefix>(Factory.Small);
        public static SizeParamter<TParamPrefix> Medium = new SizeParamter<TParamPrefix>();
        public static SizeParamter<TParamPrefix> Large = new SizeParamter<TParamPrefix>(Factory.Large);
    }
}
