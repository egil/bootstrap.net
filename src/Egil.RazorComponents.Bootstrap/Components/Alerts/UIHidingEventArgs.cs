using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts
{
    public class UIHidingEventArgs : UIEventArgs
    {
        public bool Cancel { get; set; }
        public bool KeepInRenderTree { get; set; }
    }
}
