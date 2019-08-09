using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Options;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class StringExtensions
    {
        public const string OptionSeparator = "-";

        private readonly static char[] CommaSpace = new char[] { ',', ' ' };

        public static string[] SplitOnCommaOrSpace(this string value)
        {
            return value.Split(CommaSpace, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string CombineWith(this string cssClassFragment, IOption other)
        {
            return string.Concat(cssClassFragment, OptionSeparator, other.Value);
        }
    }
}
