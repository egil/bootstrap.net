using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.BlazorComponents.Bootstrap.Grid.Parameters
{
    public class OffsetParameter : Parameter
    {
        public override int Count => throw new NotImplementedException();

        public override IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static readonly OffsetParameter None = new NoneOffsetParameter();

        class NoneOffsetParameter : OffsetParameter
        {
            public override int Count => 0;

            public override IEnumerator<string> GetEnumerator()
            {
                yield break;
            }
        }
    }
}
