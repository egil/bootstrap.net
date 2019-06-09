using System;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    internal class AnimationDelayFactoryStub : IAnimationDelayFactory
    {
        public Task Task { get; } = new Task(() => { });

        public void RunAnimation()
        {
            Task.Start();
        }

        public Task StandardDelay() => Task;
    }

}
