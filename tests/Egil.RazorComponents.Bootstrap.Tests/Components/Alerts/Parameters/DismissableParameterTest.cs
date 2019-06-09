using Egil.RazorComponents.Bootstrap.Parameters;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters

{
    public class DismissableParameterTest : ParameterFixture
    {
        [Fact(DisplayName = "Paramter returns correct css class values")]
        public void MyTestMethod1()
        {
            DismissableParameter.Dismissable.ShouldBe(new[] { "alert-dismissible" });
            DismissableParameter.Default.ShouldBeEmpty();
        }

        [Fact(DisplayName = "Paramter should be convertable from true false")]
        public void MyTestMethod2()
        {
            DismissableParameter sut = true;            
            sut.ShouldBe(DismissableParameter.Dismissable);

            sut = false;
            sut.ShouldBe(DismissableParameter.Default);
        }
    }
}
