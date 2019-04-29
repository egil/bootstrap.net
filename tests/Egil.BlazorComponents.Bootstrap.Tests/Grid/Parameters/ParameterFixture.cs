using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Egil.BlazorComponents.Bootstrap.Grid.Parameters;
using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid
{
    public abstract class ParameterFixture
    {
        public static readonly IEnumerable<object[]> BreakpointTypes = new EnumEnumerator<BreakpointType>();

        protected readonly TestComponent sut = new TestComponent();

        public class TestComponent
        {
            public OrderParameter Order { get; set; } = OrderParameter.None;
            public SpanParameter Span { get; set; } = SpanParameter.Default;
            public OffsetParameter Offset { get; set; } = OffsetParameter.None;
        }
    }
}