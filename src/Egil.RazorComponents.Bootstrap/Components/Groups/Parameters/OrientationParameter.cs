using System;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
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

        public static implicit operator OrientationParameter(SpacingSide side)
        {
            // TODO: split spacing side up in orientation group and rest. Orientation and Sides
            // TODO: Rename SpacingSide to a more generalized use
            return side.Type switch
            {
                SpacingSideType.Horizontal => Horizontal,
                SpacingSideType.Vertical => Vertical,
                _ => throw new ArgumentException($"Group orientation can only be vertical or horizontal. {side.Type} is not allowed.")
            };
        }

        public static OrientationParameter Horizontal = new OrientationParameter(SpacingSideType.Horizontal);
        public static OrientationParameter Vertical = new OrientationParameter(SpacingSideType.Vertical);
    }
}
