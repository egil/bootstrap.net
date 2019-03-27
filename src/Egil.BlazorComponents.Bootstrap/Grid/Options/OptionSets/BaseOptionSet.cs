using System.Collections.Generic;

namespace Egil.BlazorComponents.Bootstrap.Grid.Options
{
    public abstract class BaseOptionSet<T> : List<IOption<T>> where T : IOption<T> { }

}