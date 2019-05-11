using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Options.CommonOptions;
using Shouldly;
using Xunit;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Options.CommonOptions
{
    public class NumberTests
    {
        [Theory(DisplayName = "Spacing number returns correct value for positive numbers")]
        [NumberRangeData(0, 12)]
        public void SpacingNumberValue(int number)
        {
            var option = (Number)number;
            option.Value.ShouldBe(number.ToString());
        }

        [Theory(DisplayName = "Spacing number returns correct value for negative numbers")]
        [NumberRangeData(1, 5)]
        public void SpacingNumberNegativeValue(int number)
        {
            var option = (Number)(-number);
            option.Value.ShouldBe($"n{number}");
        }


        [Theory(DisplayName = "Creating a grid number throws if number value is out of range")]
        [InlineData(-1)]
        [InlineData(13)]
        public void ConvertingInvalidNumberToGridNumberThrows(int invalidNumber)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Number.ToGridNumber(invalidNumber));
        }

        [Theory(DisplayName = "Creating a spacing number throws if number value is out of range")]
        [InlineData(-6)]
        [InlineData(6)]
        public void ConvertingInvalidNumberToSpacingNumberThrows(int invalidNumber)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => Number.ToSpacingNumber(invalidNumber));
        }
    }
}
