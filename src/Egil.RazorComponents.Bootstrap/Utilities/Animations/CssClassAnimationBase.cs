using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public abstract class CssClassAnimationBase : ICssClassAnimation
    {
        private readonly IAnimationDelayFactory _animationDelayFactory;

        public bool Ready => !Running && !Completed;

        public bool Running { get; private set; }

        public bool Completed { get; private set; }

        public abstract int Count { get; }

        protected CssClassAnimationBase(IAnimationDelayFactory? animationDelayFactory = null)
        {
            _animationDelayFactory = animationDelayFactory ?? DefaultAnimationDelayFactory.Instance;
        }

        public void Reset()
        {
            if (Running) throw new InvalidOperationException("The animation is running.");
            Completed = false;
        }

        public Task Run()
        {
            if (Running)
            {
                throw new InvalidOperationException("The animation is already running.");
            }
            if (Completed) return Task.CompletedTask;

            Running = true;
            return _animationDelayFactory
                .StandardDelay()
                .ContinueWith(x =>
                {
                    Running = false;
                    Completed = true;
                });
        }

        public abstract IEnumerator<string> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
