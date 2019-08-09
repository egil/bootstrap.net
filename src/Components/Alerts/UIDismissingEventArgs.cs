using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts
{
    public class UIDismissingEventArgs : UIEventArgs
    {
        /// <summary>
        /// Set Cancel to true to cancel the operation.
        /// </summary>
        public bool Cancel { get; set; } = false;

        /// <summary>
        /// Specifies whether the source components should have it's Enabled state set to true after the operation completes.
        /// </summary>
        public bool EnabledAfter { get; set; } = false;
    }
}
