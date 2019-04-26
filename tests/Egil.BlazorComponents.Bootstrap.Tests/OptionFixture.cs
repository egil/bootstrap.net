using System;
using System.Collections.Generic;
using System.Linq;
using Egil.BlazorComponents.Bootstrap.Grid.Options;
using static Egil.BlazorComponents.Bootstrap.Grid.Options.OptionFactory.LowerCase.Abbr;

namespace Egil.BlazorComponents.Bootstrap.Tests
{
    public abstract class OptionFixture<TSutOption>
        where TSutOption : IOption
    {
        public static readonly IReadOnlyList<IOption> AllOptions = new List<IOption>
        {
            center, sm-start, // alignment
            sm, sm-1, // breakpoint
            auto, sm-auto, // span
            first, last, lg-first, md-last, // order
        };

        public static readonly IReadOnlyList<TSutOption> SutOptions = AllOptions.OfType<TSutOption>().ToList();

        //new List<IOptionSet<IOption>>
        //{
        //    new OptionSet2<IAlignmentOption>()
        //};

        //        public interface IOption<out T> : IOption { }
        //public interface ISharedOption : IOption<ISharedOption>, IOrderOption, ISpanOption { }
        //public interface IOrderOption : IOption<IOrderOption> { }
        //public interface ISpanOption : IOption<ISpanOption> { }
        //public interface IAlignmentOption : IOption<IAlignmentOption> { }


        //public static IEnumerable<Type[]> CreateCompatibleOptionTypePairs<TSutOptionType>()
        //    where TSutOptionType : IOption
        //{
        //    var types = GetOptionTypes<TSutOptionType>();
        //    var count = types.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        for (int j = i; j < count; j++)
        //        {
        //            yield return new[] { types[i], types[j] };
        //        }
        //    }
        //}

        //public static IEnumerable<Type[]> CreateIncompatibleOptionTypePairs<TSutOptionType>()
        //    where TSutOptionType : IOption
        //{
        //    Type sutOptionType = typeof(TSutOptionType);
        //    var allOptions = GetOptionTypes<IOption>();
        //    var sutOptions = allOptions.Where(type => sutOptionType.IsAssignableFrom(type)).ToList();
        //    var incompatibleOptions = allOptions.Where(type => !sutOptionType.IsAssignableFrom(type)).ToList();

        //    foreach (var sutOption in sutOptions)
        //    {
        //        foreach (var incompatibleOption in incompatibleOptions)
        //        {
        //            yield return new[] { sutOption, incompatibleOption };
        //        }
        //    }
        //}

        //private static List<Type> GetOptionTypes<TOptionTypeBase>() where TOptionTypeBase : IOption
        //{
        //    Type rootType = typeof(TOptionTypeBase);

        //    if (!rootType.IsInterface) throw new ArgumentException("The specified option type is not an interface.");

        //    return rootType.Assembly.GetTypes().Where(TypeFilter).ToList();

        //    bool TypeFilter(Type type) =>
        //        type.IsClass &&
        //        !type.IsAbstract &&
        //        !type.IsGenericType &&
        //        rootType.IsAssignableFrom(type);
        //}
    }
}
