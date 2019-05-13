using Egil.RazorComponents.Bootstrap.Parameters;
using Shouldly;
using System.Linq;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
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
