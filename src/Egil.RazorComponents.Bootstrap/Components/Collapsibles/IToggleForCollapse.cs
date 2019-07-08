using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{

    public interface IToggleForCollapse : IBootstrapComponent, IDisposable
    {
        /// <summary>
        /// Gets or sets the IDs of the <see cref="Collapse"/> components that this 
        /// component should be used to toggle. One or more IDs can be specified 
        /// by separating them with a comma or space.
        /// </summary>
        [Parameter] public string? ToggleFor { get; set; }

        protected internal event EventHandler OnToggled;

        protected internal void SetExpandedState(bool isExpanded);

        protected static void Connect(IToggleForCollapse toggler)
        {
            if (toggler.BootstrapContext != null && toggler.ToggleFor != null)
            {
                foreach (var targetId in toggler.ToggleFor.SplitOnCommaOrSpace())
                {
                    toggler.BootstrapContext.ConnectToPartner<IToggleForCollapse>(targetId, toggler);
                }
            }
        }

        protected static void Disconnect(IToggleForCollapse toggler)
        {
            if (toggler.BootstrapContext != null && toggler.ToggleFor != null)
            {
                foreach (var targetId in toggler.ToggleFor.SplitOnCommaOrSpace())
                {
                    toggler.BootstrapContext.DisconnectFromPartner<IToggleForCollapse>(targetId, toggler);
                }
            }
        }
    }
}
