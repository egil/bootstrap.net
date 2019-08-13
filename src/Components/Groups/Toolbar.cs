using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Groups.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Groups
{
    public sealed class Toolbar : ParentComponentBase
    {
        private const string ToolbarCssClass = "btn-toolbar";
        private const string DefaultRole = "toolbar";

        /// <summary>
        /// Gets or sets the role HTML attribute used on the component.
        /// </summary>
        [Parameter] public string Role { get; set; } = DefaultRole;

        /// <summary>
        /// Sets the color of the buttons in the group.
        /// </summary>
        [Parameter, CssClassExcluded] public ColorParameter<ButtonColor>? Color { get; set; }

        /// <summary>
        /// Gets or sets the size of the button group.
        /// </summary>
        [Parameter, CssClassExcluded] public SizeParamter<GroupSize>? Size { get; set; }

        /// <summary>
        /// Gets or sets the padding of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        /// <summary>
        /// Gets or sets the margin of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        public Toolbar()
        {
            DefaultCssClass = ToolbarCssClass;
        }

        protected override void OnCompomnentParametersSet()
        {
            AddOverride(HtmlAttrs.ROLE, Role);
        }

        protected override void ApplyChildHooks(ComponentBase component)
        {
            if(component is Group group)
            {
                group.OnParametersSetHook = _ =>
                {
                    if (group.Color is null && Color != null)
                        group.Color = Color;

                    if(group.Size is null && Size != null) 
                        group.Size = Size;
                };
            }
        }
    }
}
