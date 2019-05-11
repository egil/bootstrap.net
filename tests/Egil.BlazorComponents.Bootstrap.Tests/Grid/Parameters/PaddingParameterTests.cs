using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    public class PaddingParameterTests : SpacingParameterTests<PaddingSpacing>
    {
        protected override string ParamPrefix => "p";
    }
}
