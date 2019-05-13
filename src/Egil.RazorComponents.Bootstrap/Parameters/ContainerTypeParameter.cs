using System.Collections.Generic;

namespace Egil.RazorComponents.Bootstrap.Parameters
{
    public class ContainerTypeParameter : Parameter
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

        public static readonly ContainerTypeParameter Default = new ContainerTypeParameter("container");
        public static readonly ContainerTypeParameter Fluid = new ContainerTypeParameter("container-fluid");
    }
}
