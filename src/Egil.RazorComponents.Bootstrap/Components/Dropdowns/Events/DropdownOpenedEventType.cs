using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Services.EventBus;

namespace Egil.RazorComponents.Bootstrap.Components.Dropdowns.Events
{
    internal class DropdownOpenedEventType : IEventType
    {
        public string Key { get; } = nameof(DropdownOpenedEventType);
        public bool CacheLatest { get; } = false;

        private DropdownOpenedEventType() { }

        public static readonly DropdownOpenedEventType Instance = new DropdownOpenedEventType();
    }
}
