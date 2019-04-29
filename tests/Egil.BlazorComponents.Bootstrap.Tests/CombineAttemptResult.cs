using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public sealed class CombineAttemptResult
    {
        public IOptionSet<IOption> ResultSet { get; }

        public CombineAttemptResult(IOptionSet<IOption> resultSet)
        {
            ResultSet = resultSet;
        }

        public CombineAttemptResult(RuntimeBinderException ex)
        {
            ResultSet = OptionSet<IOption>.Empty;
        }

        public CombineAttemptResult ThatContains(params IOption[] options)
        {
            foreach (var option in options)
            {
                ResultSet.ShouldContain(option);
            }
            return this;
        }
    }
}
