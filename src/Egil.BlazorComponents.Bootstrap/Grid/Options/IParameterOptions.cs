using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public interface IOption<T> where T : IOption<T> { }
    public interface IOrderOption : IOption<IOrderOption> { }
    public interface ISpanOption : IOption<ISpanOption> { }
    public interface IOrderSpanOption : IOption<IOrderSpanOption>, IOption<IOrderOption>, IOption<ISpanOption> { }

    public interface IXxxOption : IOption<IXxxOption> { }

    public interface IYyyOption : IOption<IYyyOption> { }

    public interface IXYOption : IOption<IXYOption>, IOption<IYyyOption>, IOption<IXxxOption> { }

    public class Opt<T> where T : IOption<T>
    {
        public static List<T> operator |(Opt<T> option1, IOption<T> option2)
        {
            throw new NotImplementedException();
        }
    }

    public class XxxTest1 : Opt<IXxxOption>, IXxxOption
    {

    }

    public class XxxTest2 : Opt<IXxxOption>, IXxxOption
    {

    }

    public class YyyTest1 : Opt<IYyyOption>, IYyyOption
    {

    }

    public class YyyTest2 : Opt<IYyyOption>, IYyyOption
    {

    }

    public class YXTest : Opt<IXYOption>, IXYOption
    {

    }
}
