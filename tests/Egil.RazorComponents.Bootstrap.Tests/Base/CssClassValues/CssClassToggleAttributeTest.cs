using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Base.CssClassValues
{
    public class CssClassToggleAttributeTest
    {
        [Fact(DisplayName = "GetValue returns trueValue when GetValue(true) is called")]
        public void MyTestMethod()
        {
            var trueValue = "TRUE";
            var sut = new CssClassToggleParameterAttribute(trueValue);

            sut.GetValue(true).ShouldBe(trueValue);
        }

        [Fact(DisplayName = "GetValue returns falseValue when GetValue(false) is called")]
        public void MyTestMethod2()
        {
            var falseValue = "TRUE";
            var sut = new CssClassToggleParameterAttribute("TRUE", falseValue);

            sut.GetValue(false).ShouldBe(falseValue);
        }

        [Fact(DisplayName = "WHen no falseValue is provided, an empty string is used")]
        public void MyTestMethod3()
        {
            var sut = new CssClassToggleParameterAttribute("TRUE");

            sut.GetValue(false).ShouldBe(string.Empty);
        }

    }
}
