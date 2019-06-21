using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Components.Alerts.Parameters;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Parameters;
using Egil.RazorComponents.Bootstrap.Tests.TestUtilities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Egil.RazorComponents.Bootstrap.Utilities.Colors.Factory.LowerCase;

namespace Egil.RazorComponents.Bootstrap.Utilities.Colors
{
    public class AlertColorParameterTest : ColorParameterTest<AlertColor>
    {
        protected override string ParamPrefix => "alert";
    }

    public abstract class ColorParameterTest<TParamPrefix> : ParameterFixture<IColorOption>
        where TParamPrefix : ICssClassPrefix, new()
    {
        private ColorParameter<AlertColor>? Param { get; set; }

        protected abstract string ParamPrefix { get; }

        [Fact(DisplayName = "Parameter prefix returns correct value")]
        public void ParameterPrefixReturnsCorrectValue()
        {
            new TParamPrefix().Prefix.ShouldBe(ParamPrefix);
        }

        [Fact(DisplayName = "ColorParameter.None.Value returns empty string")]
        public void NoOptionsResultsInEmptyValue()
        {
            ColorParameter<AlertColor>.None.ShouldBeEmpty();
            ColorParameter<AlertColor>.None.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "ColorParameter can have a color-options assigned")]
        public void CanHaveAlignmentWithIndexSpecifiedByAssignment()
        {
            var option = primary;
            Param = option;
            Param.ShouldContainOptionsWithPrefix(ParamPrefix, option);
        }
    }
}
