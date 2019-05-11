using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public sealed class CombineAttemptResult
    {
        public IOptionSet<IOption> ResultSet { get; }
        public string CompilerError { get; }

        private CombineAttemptResult(IOptionSet<IOption> resultSet)
        {
            ResultSet = resultSet;
            CompilerError = string.Empty;
        }

        private CombineAttemptResult(RuntimeBinderException ex)
        {
            ResultSet = OptionSet<IOption>.Empty;
            CompilerError = ex.Message;
        }

        public CombineAttemptResult ThatContains(params IOption[] options)
        {
            foreach (var option in options)
                ResultSet.ShouldContain(x => x.Value == option.Value);

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
