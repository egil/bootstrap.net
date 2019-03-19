using System.Reflection;
using Egil.BlazorComponents.Bootstrap.Grid;
using Egil.BlazorComponents.Bootstrap.Grid.Columns;

namespace Egil.BlazorComponents.Bootstrap.Tests.Grid.Helpers
{
    class ColumnTestWrapper
    {
        private Column wrappedColumn;

        public ColumnTestWrapper(Column? column = null)
        {
            wrappedColumn = column ?? new Column();
        }

        public SpanOptionBase Span
        {
            get
            {
                var res = (SpanOptionBase)wrappedColumn
                    .GetType()
                    .GetProperty(nameof(Span), BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(wrappedColumn, null);

                return res;
            }
            set
            {
                wrappedColumn
                    .GetType()
                    .GetProperty(nameof(Span), BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(wrappedColumn, value);
            }
        }
    }
}
