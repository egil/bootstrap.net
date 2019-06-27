using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Egil.RazorComponents.Bootstrap.Utilities.Spacing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Egil.RazorComponents.Bootstrap.Components.Groups
{
    public sealed class Toolbar : BootstrapParentComponentBase
    {
        private const string ToolbarCssClass = "btn-toolbar";
        private const string DefaultRole = "toolbar";

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

        public Toolbar()
        {
            DefaultCssClass = ToolbarCssClass;
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(HtmlTags.DIV);
            builder.AddClassAttribute(CssClassValue);
            builder.AddRoleAttribute(Role);
            builder.AddContent(ChildContent);
            builder.CloseElement();
        }
    }
}
