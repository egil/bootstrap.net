//using Egil.BlazorComponents.Bootstrap.Grid.Options;
//using Shouldly;
//using System;
//using System.Text;
//using Xunit;

//namespace Egil.BlazorComponents.Bootstrap.Grid
//{

//    public class BreakpointTests
//    {
//        [Fact(DisplayName ="CssClass returns correct value based on breakpoint")]
//        public void CssClassReturnsCorrectBootstrapBreakpoint()
//        {
//            new Breakpoint(BreakpointType.Small).CssClass.ShouldBe("sm");
//            new Breakpoint(BreakpointType.Medium).CssClass.ShouldBe("md");
//            new Breakpoint(BreakpointType.Large).CssClass.ShouldBe("lg");
//            new Breakpoint(BreakpointType.ExtraLarge).CssClass.ShouldBe("xl");
//        }

//        [Fact(DisplayName = "Breakpoint can have width specified via - operator")]
//        public void BreakpointCanHaveWidthSpecifiedViaMinusOperator()
//        {
//            var bp = new Breakpoint(BreakpointType.Large);
//            var span = 2;
//            var bpw = bp - span;
//            bpw.CssClass.ShouldBe($"{bp.CssClass}-{span}");
//        }

//        [Fact(DisplayName ="Breakpoint can be combined with first option via - operator")]
//        public void BreakpoingWithFirst()
//        {
//            var bp = new Breakpoint(BreakpointType.Large);
//            var first = new FirstOption();
//            var bpf = bp - first;
//            bpf.CssClass.ShouldBe($"{bp.CssClass}-{first.CssClass}");
//        }

//        [Fact(DisplayName = "Breakpoint can be combined with last option via - operator")]
//        public void BreakpoingWithLast()
//        {
//            var bp = new Breakpoint(BreakpointType.Large);
//            var last = new LastOption();
//            var bpf = bp - last;
//            bpf.CssClass.ShouldBe($"{bp.CssClass}-{last.CssClass}");
//        }

//        [Fact(DisplayName = "Breakpoint can be combined with auto option using - operator")]
//        public void BreakpointWithAutoOption()
//        {
//            var bp = new Breakpoint(BreakpointType.Large);
//            var auto = new AutoOption();
//            var bpa = bp - auto;
//            bpa.CssClass.ShouldBe($"{bp.CssClass}-{auto.CssClass}");
//        }

//        //[Fact(DisplayName = "Breakpoint and number option combined with a number using | operator")]
//        //public void BreakpointAndNumberOption()
//        //{
//        //    var number = 3;
//        //    var bp = new Breakpoint(BreakpointType.Medium);
//        //    var os = bp | number;
//        //    var osReverse = number | bp;
//        //}


//        // validate number option within range
//    }
//}