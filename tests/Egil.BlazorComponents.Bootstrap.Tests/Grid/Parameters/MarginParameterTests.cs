using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using Egil.BlazorComponents.Bootstrap.Tests.Utilities;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Parameters
{
    public class MarginParameterTests : SpacingParameterTests<MarginSpacing>
    {
        protected override string ParamPrefix => "m";
    }
}
