using Egil.RazorComponents.Bootstrap.Layout.Parameters;
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

        [Fact(DisplayName = "ContainerTypeParameter parameter is convertable from boolean value")]
        public void MyTestMethod4()
        {
            ContainerTypeParameter parameter;
                       
            parameter = true;
            parameter.ShouldBe(ContainerTypeParameter.Fluid);

            parameter = false;
            parameter.ShouldBe(ContainerTypeParameter.Default);
        }
    }
}
