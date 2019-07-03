using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public interface IBootstrapComponent
    {
        public IBootstrapContext? BootstrapContext { get;  }

        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; }
    }
}
