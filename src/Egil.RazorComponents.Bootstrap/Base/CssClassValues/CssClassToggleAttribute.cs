using System;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    /// <summary>
    /// Attach to a bool <see cref="Microsoft.AspNetCore.Components.ParameterAttribute"/>
    /// and provide a css claas value to use when the parameter is true and 
    /// optionally a css class value to use when the parameter is false.
    /// This will allow <see cref="BootstrapComponentBase"/> include the specified 
    /// css class values in the CssClassValue property in it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CssClassToggleParameterAttribute : Attribute
    {
        private readonly string _trueValue;
        private readonly string _falseValue;

        public CssClassToggleParameterAttribute(string trueValue, string? falseValue = null)
        {
            _trueValue = trueValue;
            _falseValue = falseValue ?? string.Empty;
        }

        public string GetValue(bool toggled) => toggled ? _trueValue : _falseValue;
    }
}
