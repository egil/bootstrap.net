using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Columns;
using Egil.BlazorComponents.Bootstrap.Tests.Grid.Helpers;
using Shouldly;
using System;
using System.Text;
using Xunit;
using static Egil.BlazorComponents.Bootstrap.Grid.Columns.SpanOption;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid
{
    public class SpanOptionsTests
    {
        private ColumnTestWrapper sut = new ColumnTestWrapper();

        [Fact(DisplayName = "When no Span rules are specified, the css class returned is 'col'")]
        public void NoSpanRulesGiveDefaultColCssClass()
        {
            sut.Span.CssClass.ShouldBe("col");
        }

        [Fact(DisplayName = "Assigning static SpanOptions to Span returns expected css class")]
        public void StaticSpanOptionAssigned()
        {
            sut.Span = Default;
            sut.Span.CssClass.ShouldBe(Default.CssClass);

            sut.Span = Small;
            sut.Span.CssClass.ShouldBe(Small.CssClass);

            sut.Span = Medium;
            sut.Span.CssClass.ShouldBe(Medium.CssClass);

            sut.Span = Large;
            sut.Span.CssClass.ShouldBe(Large.CssClass);

            sut.Span = ExtraLarge;
            sut.Span.CssClass.ShouldBe(ExtraLarge.CssClass);
        }

        [Theory(DisplayName = "When Span is specified with a number only, the css class returned is 'col-#' ")]
        [ClassData(typeof(BootstrapGridWidths))]
        public void SpanCanBeSpecifiedAsNumberOnly(int width)
        {
            var expected = $"col-{width}";
            sut.Span = width;
            sut.Span.CssClass.ShouldBe(expected);
        }

        [Theory(DisplayName = "SpanOptions width can be set via minus operator")]
        [ClassData(typeof(BootstrapGridSpanOptionsAndWidths))]
        public void SpanOptionsWidthSetViaMinusOperator(SpanOption option, int width)
        {
            var expected = option.CssClass + "-" + width;
            sut.Span = option;

            sut.Span = sut.Span - width;

            sut.Span.CssClass.ShouldBe(expected);
        }

        [Fact(DisplayName = "SpanOptions can be combined using the | operator")]
        public void SpanOptionsCanBeCombinedUsingOrOperator()
        {
            sut.Span = 1 | Small - 2 | Medium - 3 | Large - 4 | ExtraLarge - 6;
            sut.Span.CssClass.ShouldBe("col-1 col-sm-2 col-md-3 col-lg-4 col-xl-6");
        }

        [Fact(DisplayName = "Span with two options or more cannot have width assigned to it through - operator")]
        public void SpanWithTwoOptionsOrMoreCannotHaveWidthAssigned()
        {
            sut.Span = 1 | Small - 2;

            Should.Throw<InvalidOperationException>(() => sut.Span - 4,
                "Span contains two or more options. " +
                "You cannot change or set the width on more than one option.");
        }

        [Fact(DisplayName = "Duplicated options are ignored when added")]
        public void DuplicatedOptionsAreIgnoredWhenAdded()
        {
            sut.Span |= 2;

            sut.Span |= 2;

            sut.Span.CssClass.ShouldBe("col col-2");
        }

        [Fact(DisplayName = "Specifying a column width less-than 1 or greater-than 12 throws exception")]
        public void WidthMustBeWithinRange()
        {
            Should.Throw<ColumnWidthOutOfRangeException>(() => sut.Span - 0);
            Should.Throw<ColumnWidthOutOfRangeException>(() => sut.Span - 13);
        }

        [Fact]
        public void MyTestMethod()
        {
            sut.Span = Auto;
            sut.Span.CssClass.ShouldBe("col-auto");
        }

        [Theory(DisplayName = "Auto modifier can be applied to span options via minus operator")]
        [ClassData(typeof(BootstrapGridSpanOptions))]
        public void AutoSpanModifierCanBeAppliedToSpanOption(SpanOption option)
        {
            var expected = option.CssClass + "-auto";
            sut.Span = option;

            sut.Span -= Auto;

            sut.Span.CssClass.ShouldBe(expected);
        }
    }
}
