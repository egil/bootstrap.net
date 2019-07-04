using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public interface IBootstrapComponent
    {
        protected IBootstrapContext? BootstrapContext { get;  }

        protected internal IDictionary<string, object> AdditionalAttributes { get; }
    }
}
