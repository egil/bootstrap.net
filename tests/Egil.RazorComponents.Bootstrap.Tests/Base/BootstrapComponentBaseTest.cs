using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Base
{
    sealed class TestComponent : BootstrapComponentBase
    {
        [Parameter]
        public TestCssClassParameter? Param { get; set; }
    }

    sealed class SecondTestComponent : BootstrapComponentBase
    {
        [Parameter, CssClassToggleParameter("true", "false")]
        public bool Toggled { get; set; }
    }

    sealed class TestCssClassParameter : CssClassProviderBase
    {
        public TestCssClassParameter(params string[] testCssClasses)
        {
            CssClasses = testCssClasses;
        }

        public override int Count => CssClasses.Length;

        public string[] CssClasses { get; }

        public override IEnumerator<string> GetEnumerator() =>
            CssClasses.AsEnumerable<string>().GetEnumerator();
    }

    public class BootstrapComponentBaseTest
    {
        [Fact(DisplayName = "If no css classes are specified CssClassValue returns empty string")]
        public void MyTestMethodAsync()
        {
            var sut = new TestComponent();

            sut.CssClassValue.ShouldBeEmpty();
        }

        [Fact(DisplayName = "If only DefaultCssClass is present then only that is returned")]
        public void MyTestMethod()
        {
            var sut = new TestComponent();
            sut.DefaultCssClass = "CLASS";
            sut.CssClassValue.ShouldBe(sut.DefaultCssClass);
        }

        [Fact(DisplayName = "If only Class is present is present then only that is returned")]
        public void MyTestMethod2()
        {
            var sut = new TestComponent();
            sut.Class = "CLASS";
            sut.CssClassValue.ShouldBe(sut.Class);
        }

        [Fact(DisplayName = "If only a CssClassParam is present is present then only that is returned")]
        public void MyTestMethod4()
        {
            var sut = new TestComponent();
            sut.Param = new TestCssClassParameter("class1", "class2");

            sut.CssClassValue.ShouldBe($"{sut.Param.CssClasses[0]} {sut.Param.CssClasses[1]}");
        }

        [Fact(DisplayName = "If DefaultCssClass and Class is present they are combined with DefaultCssClass first")]
        public void MyTestMethod3()
        {
            var sut = new TestComponent();
            sut.DefaultCssClass = "DefaultCssClass";
            sut.Class = "Class";

            sut.CssClassValue.ShouldBe($"{sut.DefaultCssClass} {sut.Class}");
        }

        [Fact(DisplayName = "If DefaultCssClass and CssClassParams are present they are combined with DefaultCssClass first")]
        public void MyTestMethod5()
        {
            var sut = new TestComponent();
            sut.DefaultCssClass = "DefaultCssClass";
            sut.Param = new TestCssClassParameter("class1", "class2");

            sut.CssClassValue.ShouldBe($"{sut.DefaultCssClass} {sut.Param.CssClasses[0]} {sut.Param.CssClasses[1]}");
        }

        [Fact(DisplayName = "If CssClassParams and Class are present they are combined with CssClassParams first")]
        public void MyTestMethod6()
        {
            var sut = new TestComponent();
            sut.Param = new TestCssClassParameter("class1", "class2");
            sut.Class = "Class";

            sut.CssClassValue.ShouldBe($"{sut.Param.CssClasses[0]} {sut.Param.CssClasses[1]} {sut.Class}");
        }

        [Fact(DisplayName = "If all css sources are present are combined in order DefaultCssClass, CssClassParams, Class")]
        public void MyTestMethod7()
        {
            var sut = new TestComponent();
            sut.DefaultCssClass = "DefaultCssClass";
            sut.Param = new TestCssClassParameter("class1", "class2");
            sut.Class = "Class";

            sut.CssClassValue.ShouldBe($"{sut.DefaultCssClass} {sut.Param.CssClasses[0]} {sut.Param.CssClasses[1]} {sut.Class}");
        }

        [Fact(DisplayName = "If a CssClassValueAttribute is assigned to a parameter, its values are included in CssClassValue")]
        public void MyTestMethod8()
        {
            var sut = new SecondTestComponent();

            sut.CssClassValue.ShouldBe("false");   
        }

        // TODO: Use BootstrapComponentFixture and make a generic test that tests all components that inherits from it, and check that they correctly include CssClassValue when rendered and additoinal attributes.
    }
}
