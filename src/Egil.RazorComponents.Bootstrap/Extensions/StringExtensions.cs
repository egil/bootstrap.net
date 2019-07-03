using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class StringExtensions
    {
        private readonly static char[] Comma = new char[] { ',', ' ' };

        public static string[] SplitOnComma(this string value)
        {
            return value.Split(Comma, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
