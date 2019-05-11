using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
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
