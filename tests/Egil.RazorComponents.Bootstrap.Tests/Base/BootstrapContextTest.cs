using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components.Html;
using Microsoft.AspNetCore.Components;
using Moq;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public class BootstrapContextTest
    {
        class TestComponent : IComponent
        {
            internal bool Changed { get; private set; }

            public void Configure(RenderHandle renderHandle)
            {
                Changed = true;
            }

            public Task SetParametersAsync(ParameterCollection parameters)
            {
                Changed = true;
                return Task.CompletedTask;
            }

            public bool Do { get => Changed; set => Changed = true; }
        }

        class T1 : TestComponent { }
        class T2 : TestComponent { }
        class T3 : TestComponent { }
        class T4 : TestComponent { }
        class T5 : TestComponent { }
        class T6 : TestComponent { }

        [Fact(DisplayName = "When a component does not have a registered rule, it remains unchanged when calling UpdateChild")]
        public void MyTestMethod()
        {
            var sut = new BootstrapContext();
            var component = new TestComponent();

            sut.UpdateChild(component);

            component.Changed.ShouldBeFalse();
        }

        [Fact(DisplayName = "When a component has a rule, the rule is applied when calling UpdateChild")]
        public void MyTestMethod2()
        {
            var component = new TestComponent();
            var sut = new BootstrapContext();
            sut.RegisterRule<TestComponent>(x => x.SetParametersAsync(new ParameterCollection()));

            sut.UpdateChild(component);

            component.Changed.ShouldBeTrue();
        }

        [Fact(DisplayName = "Register 1 sub components register works correctly")]
        public void MyTestMethod3()
        {
            var sut = new BootstrapContext();
            var c0 = new TestComponent();
            var c1 = new T1();

            sut.RegisterRule<TestComponent, T1>(x => x.Do = true);

            sut.UpdateChild(c0);
            sut.UpdateChild(c1);
            c0.Changed.ShouldBeTrue();
            c1.Changed.ShouldBeTrue();
        }

        [Fact(DisplayName = "Register 2 sub components register works correctly")]
        public void MyTestMethod4()
        {
            var sut = new BootstrapContext();
            var c0 = new TestComponent();
            var c1 = new T1();
            var c2 = new T2();

            sut.RegisterRule<TestComponent, T1, T2>(x => x.Do = true);

            sut.UpdateChild(c0);
            sut.UpdateChild(c1);
            sut.UpdateChild(c2);
            c0.Changed.ShouldBeTrue();
            c1.Changed.ShouldBeTrue();
            c2.Changed.ShouldBeTrue();
        }

        [Fact(DisplayName = "Register 3 sub components register works correctly")]
        public void MyTestMethod5()
        {
            var sut = new BootstrapContext();
            var c0 = new TestComponent();
            var c1 = new T1();
            var c2 = new T2();
            var c3 = new T3();

            sut.RegisterRule<TestComponent, T1, T2, T3>(x => x.Do = true);

            sut.UpdateChild(c0);
            sut.UpdateChild(c1);
            sut.UpdateChild(c2);
            sut.UpdateChild(c3);
            c0.Changed.ShouldBeTrue();
            c1.Changed.ShouldBeTrue();
            c2.Changed.ShouldBeTrue();
            c3.Changed.ShouldBeTrue();
        }


        [Fact(DisplayName = "Register 4 sub components register works correctly")]
        public void MyTestMethod6()
        {
            var sut = new BootstrapContext();
            var c0 = new TestComponent();
            var c1 = new T1();
            var c2 = new T2();
            var c3 = new T3();
            var c4 = new T4();

            sut.RegisterRule<TestComponent, T1, T2, T3, T4>(x => x.Do = true);

            sut.UpdateChild(c0);
            sut.UpdateChild(c1);
            sut.UpdateChild(c2);
            sut.UpdateChild(c3);
            sut.UpdateChild(c4);
            c0.Changed.ShouldBeTrue();
            c1.Changed.ShouldBeTrue();
            c2.Changed.ShouldBeTrue();
            c3.Changed.ShouldBeTrue();
            c4.Changed.ShouldBeTrue();
        }

        [Fact(DisplayName = "Register 5 sub components register works correctly")]
        public void MyTestMethod7()
        {
            var sut = new BootstrapContext();
            var c0 = new TestComponent();
            var c1 = new T1();
            var c2 = new T2();
            var c3 = new T3();
            var c4 = new T4();
            var c5 = new T5();

            sut.RegisterRule<TestComponent, T1, T2, T3, T4, T5>(x => x.Do = true);

            sut.UpdateChild(c0);
            sut.UpdateChild(c1);
            sut.UpdateChild(c2);
            sut.UpdateChild(c3);
            sut.UpdateChild(c4);
            sut.UpdateChild(c5);
            c0.Changed.ShouldBeTrue();
            c1.Changed.ShouldBeTrue();
            c2.Changed.ShouldBeTrue();
            c3.Changed.ShouldBeTrue();
            c4.Changed.ShouldBeTrue();
            c5.Changed.ShouldBeTrue();
        }

        [Fact(DisplayName = "Register 6 sub components register works correctly")]
        public void MyTestMethod8()
        {
            var sut = new BootstrapContext();
            var c0 = new TestComponent();
            var c1 = new T1();
            var c2 = new T2();
            var c3 = new T3();
            var c4 = new T4();
            var c5 = new T5();
            var c6 = new T6();

            sut.RegisterRule<TestComponent, T1, T2, T3, T4, T5, T6>(x => x.Do = true);

            sut.UpdateChild(c0);
            sut.UpdateChild(c1);
            sut.UpdateChild(c2);
            sut.UpdateChild(c3);
            sut.UpdateChild(c4);
            sut.UpdateChild(c5);
            sut.UpdateChild(c6);
            c0.Changed.ShouldBeTrue();
            c1.Changed.ShouldBeTrue();
            c2.Changed.ShouldBeTrue();
            c3.Changed.ShouldBeTrue();
            c4.Changed.ShouldBeTrue();
            c5.Changed.ShouldBeTrue();
            c6.Changed.ShouldBeTrue();
        }
    }
}
