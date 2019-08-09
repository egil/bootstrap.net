using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Options;
using Egil.RazorComponents.Bootstrap.Options.SimpleOptions;

namespace Egil.RazorComponents.Bootstrap.Components.Dropdowns.Parameters
{
    public sealed class DirectionParameter : CssClassProviderBase
    {
        private const string Prefix = "drop";
        private readonly string _value;

        public override int Count { get; }

        public string DirectionValue { get; }

        private DirectionParameter(IDirectionOption direction)
        {
            DirectionValue = direction.Value;
            if (direction == Factory.Down)
            {
                Count = 0;
                _value = string.Empty;
            }
            else
            {
                Count = 1;
                _value = $"{Prefix}{direction.Value}";
            }
        }

        public override IEnumerator<string> GetEnumerator()
        {
            if(Count == 1) yield return _value;
        }

        public static implicit operator DirectionParameter(LeftOption _) => Left;
        public static implicit operator DirectionParameter(RightOption _) => Right;
        public static implicit operator DirectionParameter(UpOption _) => Up;
        public static implicit operator DirectionParameter(DownOption _) => Default;

        public static readonly DirectionParameter Left = new DirectionParameter(Factory.Left);
        public static readonly DirectionParameter Right = new DirectionParameter(Factory.Right);
        public static readonly DirectionParameter Up = new DirectionParameter(Factory.Up);
        public static readonly DirectionParameter Default = new DirectionParameter(Factory.Down);
    }
}
