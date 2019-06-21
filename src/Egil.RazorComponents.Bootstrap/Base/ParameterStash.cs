using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Base
{
    class ParameterStash
    {
        /// <summary>
        /// Gets or sets the padding of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        /// <summary>
        /// Gets or sets the margin of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        /// <summary>
        /// Gets or sets the width of the component using standard Bootstrap
        /// Widths: 25, 50, 70, 100.
        /// <see cref="https://bootstrap.egilhansen.com/docs/4.3/utilities/sizing/"/>
        /// </summary>
        [Parameter] public NumericSizeParameter<WidthSizePrefix> Width { get; set; } = NumericSizeParameter<WidthSizePrefix>.None;

        /// <summary>
        /// Gets or sets the height of the component using standard Bootstrap
        /// Widths: 25, 50, 70, 100.
        /// <see cref="https://bootstrap.egilhansen.com/docs/4.3/utilities/sizing/"/>
        /// </summary>
        [Parameter] public NumericSizeParameter<HeightSizePrefix> Height { get; set; } = NumericSizeParameter<HeightSizePrefix>.None;
    }
}
