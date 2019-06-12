using Egil.RazorComponents.Bootstrap.Parameters;
using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Tests
{
    sealed class TestComponent : BootstrapComponentBase
    {
        [Parameter]
        public TestCssClassParameter? Param { get; set; }

        public new string CssClassValue => base.CssClassValue;
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

    public class BoostrapComponentBaseTest
    {
        private TestComponent Sut { get; } = new TestComponent();

        [Fact(DisplayName = "If no css classes are specified CssClassValue returns empty string")]
        public void MyTestMethodAsync()
        {
            Sut.CssClassValue.ShouldBeEmpty();
        }

        [Fact(DisplayName = "If only DefaultCssClass is present then only that is returned")]
        public void MyTestMethod()
        {
            Sut.DefaultCssClass = "CLASS";
            Sut.CssClassValue.ShouldBe(Sut.DefaultCssClass);
        }

        [Fact(DisplayName = "If only Class is present is present then only that is returned")]
        public void MyTestMethod2()
        {
            Sut.Class = "CLASS";
            Sut.CssClassValue.ShouldBe(Sut.Class);
        }


        [Fact(DisplayName = "If only a CssClassParam is present is present then only that is returned")]
        public void MyTestMethod4()
        {
            Sut.Param = new TestCssClassParameter("class1", "class2");

            Sut.CssClassValue.ShouldBe($"{Sut.Param.CssClasses[0]} {Sut.Param.CssClasses[1]}");
        }

        [Fact(DisplayName = "If DefaultCssClass and Class is present they are combined with DefaultCssClass first")]
        public void MyTestMethod3()
        {
            Sut.DefaultCssClass = "DefaultCssClass";
            Sut.Class = "Class";

            Sut.CssClassValue.ShouldBe($"{Sut.DefaultCssClass} {Sut.Class}");
        }

        [Fact(DisplayName = "If DefaultCssClass and CssClassParams are present they are combined with DefaultCssClass first")]
        public void MyTestMethod5()
        {
            Sut.DefaultCssClass = "DefaultCssClass";
            Sut.Param = new TestCssClassParameter("class1", "class2");

            Sut.CssClassValue.ShouldBe($"{Sut.DefaultCssClass} {Sut.Param.CssClasses[0]} {Sut.Param.CssClasses[1]}");
        }

        [Fact(DisplayName = "If CssClassParams and Class are present they are combined with CssClassParams first")]
        public void MyTestMethod6()
        {
            Sut.Param = new TestCssClassParameter("class1", "class2");
            Sut.Class = "Class";

            Sut.CssClassValue.ShouldBe($"{Sut.Param.CssClasses[0]} {Sut.Param.CssClasses[1]} {Sut.Class}");
        }

        [Fact(DisplayName = "If all css sources are present are combined in order DefaultCssClass, CssClassParams, Class")]
        public void MyTestMethod7()
        {
            Sut.DefaultCssClass = "DefaultCssClass";
            Sut.Param = new TestCssClassParameter("class1", "class2");
            Sut.Class = "Class";

            Sut.CssClassValue.ShouldBe($"{Sut.DefaultCssClass} {Sut.Param.CssClasses[0]} {Sut.Param.CssClasses[1]} {Sut.Class}");
        }
    }
}
