using System.Collections.Generic;
using System.Linq;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using Shouldly;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public static class OptionFixtureExtensions
    {
        public static CombineAttemptResult CombinedWith<TOption1, TOption2>(this TOption1 first, TOption2 second)
            where TOption1 : IOption
            where TOption2 : IOption
        {
            try
            {
                return TryCombine(first, second);
            }
            catch (RuntimeBinderException ex)
            {
                return new CombineAttemptResult(ex);
            }

            CombineAttemptResult TryCombine(dynamic o1, dynamic o2) =>
                new CombineAttemptResult(o1 | o2);
        }

        public static CombineAttemptResult CombinedWith<TOption1>(this IOptionSet<TOption1> first, TOption1 second)
            where TOption1 : IOption
        {
            try
            {
                return TryCombine(first, second);
            }
            catch (RuntimeBinderException ex)
            {
                return new CombineAttemptResult(ex);
            }

            CombineAttemptResult TryCombine(dynamic o1, dynamic o2) =>
                new CombineAttemptResult(o1 | o2);
        }

        public static CombineAttemptResult ShouldResultInSetOf<TOptionSet>(this CombineAttemptResult combineAttempt)
        {
            combineAttempt.ResultSet.ShouldBeAssignableTo<TOptionSet>();
            return combineAttempt;
        }

        public static CombineAttemptResult ShouldNotResultInSetOf<TOptionSet>(this CombineAttemptResult combineAttempt)
        {
            combineAttempt.ResultSet.ShouldNotBeAssignableTo<TOptionSet>();
            return combineAttempt;
        }

        public static void ShouldStartWithOneOf(this string text, params string[] matches)
        {
            matches.Any(match => text.StartsWith(match))
                .ShouldBeTrue($"The text: {text}{Environment.NewLine}" +
                $"  should start with one of{Environment.NewLine}" +
                $"\"{string.Join("\", \"", matches)}\"{Environment.NewLine}" +
                $"  but did not.");
        }

        public static IEnumerable<(T first, T second)> AllPairs<T>(this IEnumerable<T> optionSource)
        {
            var options = optionSource.ToArray();
            for (int i = 0; i < options.Length; i++)
            {
                for (int j = i; j < options.Length; j++)
                {
                    var first = options[i];
                    var second = options[j];
                    yield return (first, second);
                }
            }
        }

        public static IEnumerable<(T1 first, T2 second)> AllPairsWith<T1, T2>(this IEnumerable<T1> firstOptionSource, IEnumerable<T2> secondOptionSource)
        {
            var secondSource = secondOptionSource.ToArray();
            foreach (var first in firstOptionSource)
                foreach (var second in secondSource)
                    yield return (first, second);
        }

        public static IEnumerable<(T2 second, T1 first)> ReversePairs<T1, T2>(this IEnumerable<(T1 first, T2 second)> source)
        {
            foreach (var (first, second) in source)
                yield return (second, first);
        }

        public static IEnumerable<object[]> ToFixtureData<T>(this IEnumerable<T> fixtureData, Func<T, object[]>? converter = null)
            where T : object
        {
            Func<T, object[]> defaultConverter = x => new[] { (object)x };
            converter = converter ?? defaultConverter;
            return fixtureData.Select(converter);
        }
    }
}
