using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToLowerCaseString(this bool value)
        {
            return value ? "true" : "false";
        }
    }
}
