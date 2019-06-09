using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public class DefaultAnimationDelayFactory : IAnimationDelayFactory
    {
        private const int FadeAnimationTime = 150; // matches bootstraps fade 0.15s

        public Task StandardDelay() => Task.Delay(TimeSpan.FromMilliseconds(FadeAnimationTime));

        public static readonly IAnimationDelayFactory Instance = new DefaultAnimationDelayFactory();
    }
}
