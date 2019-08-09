using System;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options.SimpleOptions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;

namespace Egil.RazorComponents.Bootstrap.Components.Groups.Parameters
{
    public class OrientationParameter : CssClassProviderBase
    {
        private const string OptionPrefix = "btn-group";

        private readonly string _value;

        public override int Count { get; } = 1;

        private OrientationParameter(SpacingSideType orientation)
        {
            if (orientation == SpacingSideType.Vertical)
                _value = CombineWith(OptionPrefix, "vertical");
            else
                _value = OptionPrefix;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            yield return _value;
        }


        public static implicit operator OrientationParameter(HorizontalOption _) => Horizontal;
        public static implicit operator OrientationParameter(VerticalOption _) => Vertical;

        public static OrientationParameter Horizontal = new OrientationParameter(SpacingSideType.Horizontal);
        public static OrientationParameter Vertical = new OrientationParameter(SpacingSideType.Vertical);
    }
}
