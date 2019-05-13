using Egil.RazorComponents.Bootstrap.Parameters;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
{
    public class MarginParameterTests : SpacingParameterTests<MarginSpacing>
    {
        protected override string ParamPrefix => "m";
    }
}
