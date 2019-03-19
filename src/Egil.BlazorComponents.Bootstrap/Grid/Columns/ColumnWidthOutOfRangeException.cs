using System;

namespace Egil.BlazorComponents.Bootstrap.Grid
{
    public class ColumnWidthOutOfRangeException : Exception
    {
        public ColumnWidthOutOfRangeException(int invalidWidth)
            : base($"A columns width must be between 1 and 12. The specified width '{invalidWidth}' is not allowed.")
        {
        }
    }
}
