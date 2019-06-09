using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Utilities.Colors.OptionsFactory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
    public class ColorOptionTest
    {
        [Fact(DisplayName = "ColorFactory returns ColorOptions with the correct css clsas")]
        public void MyTestMethod()
        {
            primary.Value.ShouldBe("primary");
            secondary.Value.ShouldBe("secondary");
            success.Value.ShouldBe("success");
            danger.Value.ShouldBe("danger");
            warning.Value.ShouldBe("warning");
            info.Value.ShouldBe("info");
            light.Value.ShouldBe("light");
            dark.Value.ShouldBe("dark");
            //body.Value.ShouldBe("body");
            //muted.Value.ShouldBe("muted");
            //white.Value.ShouldBe("white");
            //black50.Value.ShouldBe("black-50");
            //white50.Value.ShouldBe("white-50");
        }
    }
}
