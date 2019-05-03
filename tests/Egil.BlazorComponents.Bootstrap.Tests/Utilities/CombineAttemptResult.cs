using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public sealed class CombineAttemptResult
    {
        public IOptionSet<IOption> ResultSet { get; }

        private CombineAttemptResult(IOptionSet<IOption> resultSet)
        {
            ResultSet = resultSet;
        }

        private CombineAttemptResult(RuntimeBinderException ex)
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

        public static CombineAttemptResult TryCombine(dynamic first, dynamic second)
        {
            try
            {
                return new CombineAttemptResult(first | second);
            }
            catch (RuntimeBinderException ex)
            {
                return new CombineAttemptResult(ex);
            }
        }
    }
}
