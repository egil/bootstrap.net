using Egil.RazorComponents.Bootstrap.Layout.Parameters;
using Shouldly;
using System.Linq;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
{
    public class NoGuttersParameterTests
    {
        [Fact(DisplayName = "Default no-gutter type returns no css class")]
        public void MyTestMethod()
        {
            NoGuttersParameter.Default.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "NoGutter type returns correct css class")]
        public void MyTestMethod3()
        {
            NoGuttersParameter.NoGutter.Single().ShouldBe("no-gutter");
        }

        [Fact(DisplayName = "NoGutterParameter parameter is convertable from boolean value")]
        public void MyTestMethod4()
        {
            NoGuttersParameter hasNoGutter = true;
            hasNoGutter.ShouldBe(NoGuttersParameter.NoGutter);

            NoGuttersParameter hasGutter = false;
            hasGutter.ShouldBe(NoGuttersParameter.Default);
        }
    }
}

