using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public class FadeOutAnimationTest
    {
        [Fact(DisplayName = "When FadeOut is in ready state, values 'fade' and 'show' are returned")]
        public void MyTestMethod()
        {
            var sut = new FadeOutAnimation();

            sut.Ready.ShouldBeTrue();
            sut.ShouldBe(new[] { "fade", "show" });
        }

        [Fact(DisplayName = "When FadeOut is in a running state, value 'fade' is returned")]
        public void MyTestMethod2()
        {
            var sut = new FadeOutAnimation();

            _ = sut.Run();

            sut.ShouldBe(new[] { "fade" });
        }
    }

}
