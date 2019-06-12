using Egil.RazorComponents.Bootstrap.Parameters;
using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Layout.Parameters
{
    public class ContainerTypeParameter : CssClassProviderBase, ICssClassProvider
    {
        private string Value { get; }

        private ContainerTypeParameter(string value)
        {
            Value = value;
        }

        public override int Count => 1;

        public override IEnumerator<string> GetEnumerator()
        {
            yield return Value;
        }

        public static implicit operator ContainerTypeParameter(bool isFluid)
        {
            return isFluid ? Fluid : Default;
        }

        public static readonly ContainerTypeParameter Default = new ContainerTypeParameter("container");
        public static readonly ContainerTypeParameter Fluid = new ContainerTypeParameter("container-fluid");
    }
}
