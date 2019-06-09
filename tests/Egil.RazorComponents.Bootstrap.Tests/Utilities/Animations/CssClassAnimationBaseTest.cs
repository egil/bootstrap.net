using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public class CssClassAnimationBaseTest
    {
        class CssClassAnimationTester : CssClassAnimationBase
        {
            public CssClassAnimationTester(IAnimationDelayFactory? animationDelayFactory = null) : base(animationDelayFactory)
            {
            }

            public override int Count { get; }

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }

        private AnimationDelayFactoryStub DelayFactory { get; } = new AnimationDelayFactoryStub();

        [Fact(DisplayName = "State is ready when initialized")]
        public void MyTestMethod()
        {
            var sut = new CssClassAnimationTester(DelayFactory);

            sut.Ready.ShouldBeTrue();
            sut.Running.ShouldBeFalse();
        }

        [Fact(DisplayName = "State changes from running to completed correctly after call to Run")]
        public async Task MyTestMethod2()
        {
            var sut = new CssClassAnimationTester(DelayFactory);

            var runtask = sut.Run();

            sut.Running.ShouldBeTrue();
            
            DelayFactory.RunAnimation();
            await runtask;

            sut.Running.ShouldBeFalse();
            sut.Ready.ShouldBeFalse();
            sut.Completed.ShouldBeTrue();
        }

        [Fact(DisplayName = "Calling Run when state is running throws")]
        public void MyTestMethod3()
        {
            var sut = new CssClassAnimationTester(DelayFactory);

            _ = sut.Run();
            Should.Throw<InvalidOperationException>(() => _ = sut.Run())
                .Message.ShouldBe("The animation is already running.");
        }
    }
}
