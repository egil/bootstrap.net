using System;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{

    public interface IToggleForCollapse : IBootstrapComponent, IDisposable
    {
        [Parameter] public string? ToggleFor { get; set; }

        event EventHandler OnToggled;

        protected internal void SetExpandedState(bool isExpanded);

        protected static void Connect(IToggleForCollapse toggler)
        {
            if (toggler.BootstrapContext != null && toggler.ToggleFor != null)
            {
                foreach (var targetId in toggler.ToggleFor.SplitOnComma())
                {
                    toggler.BootstrapContext.ConnectToPartner<IToggleForCollapse>(targetId, toggler);
                }
            }
        }

        protected static void Disconnect(IToggleForCollapse toggler)
        {
            if (toggler.BootstrapContext != null && toggler.ToggleFor != null)
            {
                foreach (var targetId in toggler.ToggleFor.SplitOnComma())
                {
                    toggler.BootstrapContext.DisconnectFromPartner<IToggleForCollapse>(targetId, toggler);
                }
            }
        }
    }
}
