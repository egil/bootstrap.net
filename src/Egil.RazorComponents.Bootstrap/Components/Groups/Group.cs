using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Components.Cards;
using Egil.RazorComponents.Bootstrap.Components.Groups.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Groups
{
    public sealed class Group : BootstrapParentComponentBase
    {
        private const string DefaultRole = "group";
        private const string CardGroupCssClass = "card-group";
        private const GroupType DefaultGroupType = GroupType.ButtonGroup;

        public GroupType Type { get; private set; } = GroupType.Undetermined;

        /// <summary>
        /// Gets or sets the role HTML attribute used on the component.
        /// </summary>
        [Parameter] public string Role { get; set; } = DefaultRole;

        /// <summary>
        /// Gets or sets the padding of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<PaddingSpacing> Padding { get; set; } = SpacingParameter<PaddingSpacing>.None;

        /// <summary>
        /// Gets or sets the margin of the component, using Bootstrap.NETs spacing syntax.
        /// </summary>
        [Parameter] public SpacingParameter<MarginSpacing> Margin { get; set; } = SpacingParameter<MarginSpacing>.None;

        /// <summary>
        /// Gets or sets the orientation of the components inside the group.
        /// </summary>
        [Parameter] public OrientationParameter? Orientation { get; set; } = OrientationParameter.Horizontal;

        /// <summary>
        /// Gets or sets the size of the button group.
        /// </summary>
        [Parameter] public SizeParamter<GroupSize>? Size { get; set; } = SizeParamter<GroupSize>.Medium;

        public Group()
        {
            AlwaysCascadeToChildren = true;
        }

        protected override void OnChildInit(BootstrapContextAwareComponentBase component)
        {
            var originalType = Type;
            Type = component switch
            {
                Button _ when Type == GroupType.ButtonGroup || Type == GroupType.Undetermined => GroupType.ButtonGroup,
                Card _ when Type == GroupType.CardGroup || Type == GroupType.Undetermined => GroupType.CardGroup,
                _ when Type != GroupType.Undetermined => throw new Exception("Mixed children not allowed"),
                _ => throw new Exception("Unknown child type")
            };

            // Now that we have determined the type of children, there is 
            // no need to continue to provide Group to them as their parent.
            AlwaysCascadeToChildren = false;

            if (Type != DefaultGroupType && originalType == GroupType.Undetermined)
            {                
                ConfigureParametersForGroupType();                
                StateHasChanged();
            }
        }

        protected override void OnBootstrapParametersSet()
        {
            ConfigureParametersForGroupType();
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddRoleAttribute(Role);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }

        private void ConfigureParametersForGroupType()
        {
            if (Type == GroupType.CardGroup)
            {
                Orientation = null;
                Size = null;
                Role = string.Empty;
                DefaultCssClass = CardGroupCssClass;
            }
        }
    }
}
