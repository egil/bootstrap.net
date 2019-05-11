using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    public class ContainerTypeParameterTests
    {
        [Fact(DisplayName = "Default container type returns correct css class")]
        public void MyTestMethod()
        {
            ContainerTypeParameter.Default.Single().ShouldBe("container");
        }
        
        [Fact(DisplayName = "Fluid container type returns correct css class")]
        public void MyTestMethod3()
        {
            ContainerTypeParameter.Fluid.Single().ShouldBe("container-fluid");
        }
    }
}
