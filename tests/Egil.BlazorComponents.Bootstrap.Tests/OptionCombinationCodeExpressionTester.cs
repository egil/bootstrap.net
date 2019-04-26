using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shouldly;
using Xunit.Abstractions;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public static class OptionCombinationCodeExpressionTester
    {
        private const string CombinationExpression = "(option1, option2) => { var x = option1 | option2; }";
        private static readonly ScriptOptions ScriptOptions = ScriptOptions.Default.AddReferences(typeof(Option).Assembly);
        private static readonly MethodInfo ShouldNotBeCombineableInfo;
        private static readonly MethodInfo ShouldBeCombineableInfo;

        static OptionCombinationCodeExpressionTester()
        {
            var type = typeof(OptionCombinationCodeExpressionTester);
            ShouldNotBeCombineableInfo = type.GetMethod(nameof(ShouldNotBeCombineable), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod);
            ShouldBeCombineableInfo = type.GetMethod(nameof(ShouldBeCombineable), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod);
        }

        /// <summary>
        /// Test if the expression 'var x = option1 | option2;' compiles, where
        /// option1 is optionType and option2 is otherOption.
        /// </summary>
        /// <param name="optionType">The type of option1</param>
        /// <param name="otherOption">The type of option2</param>
        /// <param name="output">If supplied, will use the output to write a message about each code expression being compiled</param>
        public static void ShouldBeCombineableWith(this Type optionType, Type otherOption,
            ITestOutputHelper? output = null)
        {
            object?[] args = { output };
            ShouldBeCombineableInfo.MakeGenericMethod(optionType, otherOption).Invoke(null, args);
        }

        /// <summary>
        /// Test if the expression 'var x = option1 | option2;' compiles, where
        /// option1 is optionType and option2 is otherOption.
        /// 
        /// If the expression compiles, the test fails. 
        /// The tests also fails if the reason the expression does not compile is not CS0019.
        /// </summary>
        /// <param name="optionType">The type of option1</param>
        /// <param name="otherOption">The type of option2</param>
        /// <param name="output">If supplied, will use the output to write a message about each code expression being compiled</param>
        public static void ShouldNotBeCombineableWith(this Type optionType, Type otherOption, ITestOutputHelper? output = null)
        {
            object?[] args = { output };

            ShouldNotBeCombineableInfo.MakeGenericMethod(optionType, otherOption).Invoke(null, args);
        }

        private static void ShouldNotBeCombineable<TOption1, TOption2>(ITestOutputHelper? output = null)
        {
            var message = $"var x = {typeof(TOption1).Name} | {typeof(TOption2).Name};";
            output?.WriteLine($"Testing expression: {message}");

            var actual = Should.Throw<CompilationErrorException>(TryCompile<TOption1, TOption2>, $"Expression should not be allowed by compiler: {message}");
            actual.Message.ShouldContain("error CS0019: Operator '|' cannot be applied to operands of type");
        }

        private static void ShouldBeCombineable<TOption1, TOption2>(ITestOutputHelper? output = null)
        {
            var message = $"var x = {typeof(TOption1).Name} | {typeof(TOption2).Name};";
            output?.WriteLine($"Testing expression: {message}");
            Should.NotThrow(TryCompile<TOption1, TOption2>, $"Expression should be allowed by compiler: {message}");
        }

        private static Task<Action<TOption1, TOption2>> TryCompile<TOption1, TOption2>() => CSharpScript.EvaluateAsync<Action<TOption1, TOption2>>(CombinationExpression, ScriptOptions);
    }
}
