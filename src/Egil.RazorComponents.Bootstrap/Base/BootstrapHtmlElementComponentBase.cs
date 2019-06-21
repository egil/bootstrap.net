using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public abstract class BootstrapHtmlElementComponentBase : BootstrapParentComponentBase
    {
        private bool _ignoreParentContext;

        [Parameter] public bool DisableOverride { get; set; } = false;

        protected internal sealed override string CssClassValue => DisableOverride
            ? Class ?? string.Empty
            : base.CssClassValue;

        public sealed override bool IgnoreParentContext
        {
            get => DisableOverride || _ignoreParentContext;
            set => _ignoreParentContext = value;
        }
    }
}
