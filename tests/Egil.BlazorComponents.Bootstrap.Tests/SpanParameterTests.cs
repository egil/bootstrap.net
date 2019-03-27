//using Egil.BlazorComponents.Bootstrap.Grid.Options;
//using Shouldly;
//using Xunit;
//using static Egil.BlazorComponents.Bootstrap.Grid.Options.Statics;

//namespace Egil.BlazorComponents.Bootstrap.Grid
//{
//    public class SpanOptionTests : ParameterTests
//    {
//        void AssertCorrectCssClass(Option appliedOption)
//        {
//            sut.Span.CssClass.ShouldBe($"col-{appliedOption.CssClass}");
//        }

//        [Fact(DisplayName = "CssClass returns 'col' by default")]
//        public void CssClassReturnsColByDefault()
//        {
//            sut.Span.CssClass.ShouldBe("col");
//        }

//        [Fact(DisplayName = "Span can have a width specified by assignment")]
//        public void SpanCanHaveWidthSpecifiedByAssignment()
//        {
//            var width = 4;
//            sut.Span = width;
//            sut.Span.CssClass.ShouldBe($"col-{width}");
//        }

//        [Fact(DisplayName = "Span can have a breakpoint specified by assignment")]
//        public void SpanCanHaveBreakpointSpecifiedByAssignment()
//        {
//            var bp = new Breakpoint(BreakpointType.Large);
//            sut.Span = bp;
//            AssertCorrectCssClass(bp);
//        }

//        [Fact(DisplayName = "Span can have a breakpoint with width specified by assignment")]
//        public void SpanCanHaveBreakpointWithWidthSpecifiedByAssignment()
//        {
//            var bp = new Breakpoint(BreakpointType.Large) - 4;
//            sut.Span = bp;
//            AssertCorrectCssClass(bp);
//        }

//        [Fact(DisplayName = "Span can have a breakpoint with auto option specified by assignment")]
//        public void SpanCanHaveBreakpointWithAutoOptionSpecifiedByAssignment()
//        {
//            var bp = new Breakpoint(BreakpointType.Large) - new AutoOption();
//            sut.Span = bp;
//            AssertCorrectCssClass(bp);
//        }

//        [Fact(DisplayName = "Span width and breakpoint can be combined using the | operator")]
//        public void SpanWidthAndBreakpointCanBeCombinedUsingOrOperator()
//        {
//            var auto = new AutoOption();
//            var first = new AutoOption();
//            var bp = new Breakpoint(BreakpointType.Medium);
//            var bp2 = new Breakpoint(BreakpointType.Large);
//            var bpn = new BreakpointWithNumber(BreakpointType.Large, 5);
//            var bpn2 = new BreakpointWithNumber(BreakpointType.ExtraLarge, 8);
//            int width = 3;

//            var wbp = width | bp;
//            sut.Span = wbp;
//            sut.Span.CssClass.ShouldBe($"col-{width} col-{bp.CssClass}");

//            var bpw = bp | width;
//            sut.Span = bpw;
//            sut.Span.CssClass.ShouldBe($"col-{bp.CssClass} col-{width}");

//            var bpnw = bpn | width;
//            sut.Span = bpnw;
//            sut.Span.CssClass.ShouldBe($"col-{bpn.CssClass} col-{width}");

//            var wbpn = width | bpn;
//            sut.Span = wbpn;
//            sut.Span.CssClass.ShouldBe($"col-{width} col-{bpn.CssClass}");

//            var bpa = bp | auto;
//            sut.Span = bpa;
//            sut.Span.CssClass.ShouldBe($"col-{bp.CssClass} col-{auto.CssClass}");

//            var abp = auto | bp;
//            sut.Span = abp;
//            sut.Span.CssClass.ShouldBe($"col-{auto.CssClass} col-{bp.CssClass}");

//            var wbpa = width | bp | auto;
//            sut.Span = wbpa;
//            sut.Span.CssClass.ShouldBe($"col-{width} col-{bp.CssClass} col-{auto.CssClass}");

//            var wbpbp2 = width | bp | bp2;
//            sut.Span = wbpbp2;
//            sut.Span.CssClass.ShouldBe($"col-{width} col-{bp.CssClass} col-{bp2.CssClass}");

//            var wbpbpn = width | bp | bpn;
//            sut.Span = wbpbpn;
//            sut.Span.CssClass.ShouldBe($"col-{width} col-{bp.CssClass} col-{bpn.CssClass}");

//            var bpn2wbpbpn = bpn2 | width | bp | bpn;
//            sut.Span = bpn2wbpbpn;
//            sut.Span.CssClass.ShouldBe($"col-{bpn2.CssClass} col-{width} col-{bp.CssClass} col-{bpn.CssClass}");

//            //var bpf = bp | first;
//            //sut.Span = bpf; // TODO: SHOULD NOT BE POSSIBLE
//            //sut.Span.CssClass.ShouldBe("// TODO: SHOULD NOT BE POSSIBLE");
//        }

//        //[Fact(DisplayName = "Breakpoint and number option combined with a number using | operator")]
//        //public void BreakpointAndNumberOption()
//        //{
//        //    var number = 3;
//        //    var bp = new Breakpoint(BreakpointType.Medium);
//        //    sut.Span = bp | number;
//        //    os.Option.ShouldBe(bp);
//        //    os.Number.ShouldBe(number);

//        //    sut.Span = number | bp;
//        //    osReverse.Breakpoint.ShouldBe(bp);
//        //    osReverse.Number.ShouldBe(number);
//        //}
//    }
//}
