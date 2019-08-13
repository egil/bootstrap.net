using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Cards;
using Egil.RazorComponents.Bootstrap.Components.Dropdowns;
using Egil.RazorComponents.Bootstrap.Components.Groups.Parameters;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Egil.RazorComponents.Bootstrap.Components.Html.Parameters;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Colors;
using Egil.RazorComponents.Bootstrap.Utilities.Sizings;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Groups
{
    public sealed class Group : ParentComponentBase, IChildTrackingParentComponent
    {
        private const string DefaultRole = "group";
        private const string CardGroupCssClass = "card-group";
        private const GroupType DefaultGroupType = GroupType.ButtonGroup;

        private int _itemCount = 0;

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
        /// Sets the color of the buttons in the group.
        /// </summary>
        [Parameter, CssClassExcluded] public ColorParameter<ButtonColor>? Color { get; set; }

        /// <summary>
        /// Gets or sets the size of the button group.
        /// </summary>
        [Parameter] public SizeParamter<GroupSize>? Size { get; set; }

        protected override void OnCompomnentParametersSet()
        {
            ConfigureParametersForGroupType();
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);
            builder.AddRoleAttribute(Role);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddMultipleAttributes(OverriddenAttributes);
            builder.AddContent(ChildContent);
            builder.AddElementReferenceCapture(DomElementCapture);
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

        protected override void ApplyChildHooks(ComponentBase component)
        {
            switch (component)
            {
                case Button button:
                    button.OnParametersSetHook = _ =>
                    {
                        if (button.Color is null && Color != null)
                            button.Color = Color;
                    };
                    break;
                case Dropdown dropdown:
                    dropdown.OnParametersSetHook = _ =>
                    {
                        dropdown.DefaultCssClass = "btn-group";
                        if (dropdown.Color is null && Color != null)
                            dropdown.Color = Color;
                    };
                    break;
            }
        }

        void IChildTrackingParentComponent.AddChild(ComponentBase component)
        {
            var originalType = Type;
            Type = component switch
            {
                Button _ when Type == GroupType.ButtonGroup || Type == GroupType.Undetermined => GroupType.ButtonGroup,
                Dropdown _ when Type == GroupType.ButtonGroup || Type == GroupType.Undetermined => GroupType.ButtonGroup,
                Card _ when Type == GroupType.CardGroup || Type == GroupType.Undetermined => GroupType.CardGroup,
                _ when Type != GroupType.Undetermined => throw new InvalidChildContentException($"Different types of child components are not allowed inside a {nameof(Group)} component."),
                _ => throw new InvalidChildContentException($"Components of type {component.GetType().Name} is not allowed inside a {nameof(Group)} component.")
            };

            _itemCount++;

            if (Type != DefaultGroupType && originalType == GroupType.Undetermined)
            {
                ConfigureParametersForGroupType();
                StateHasChanged();
            }
        }

        void IChildTrackingParentComponent.RemoveChild(ComponentBase component)
        {
            _itemCount--;

            if (_itemCount == 0)
            {
                Type = GroupType.Undetermined;
                StateHasChanged();
            }
        }
    }
}
