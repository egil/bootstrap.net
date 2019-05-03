using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    [DebuggerDisplay("Option: {Value}")]
    public abstract class Option
    {
        public const string OptionSeparator = "-";
    }
}