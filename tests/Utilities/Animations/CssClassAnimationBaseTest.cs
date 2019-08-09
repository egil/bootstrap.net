using Shouldly;
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
        public async void MyTestMethod2()
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

        [Fact(DisplayName = "When Reset is called, state is changed to ready")]
        public async Task MyTestMethod4()
        {
            var sut = new CssClassAnimationTester(DelayFactory);
            DelayFactory.RunAnimation();
            await sut.Run();

            sut.Reset();

            sut.Ready.ShouldBeTrue();
        }
    }
}
