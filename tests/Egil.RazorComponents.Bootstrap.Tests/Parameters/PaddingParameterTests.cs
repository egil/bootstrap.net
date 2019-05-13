using Egil.RazorComponents.Bootstrap.Parameters;

namespace Egil.RazorComponents.Bootstrap.Tests.Parameters
{
    public class PaddingParameterTests : SpacingParameterTests<PaddingSpacing>
    {
        protected override string ParamPrefix => "p";
    }
}
