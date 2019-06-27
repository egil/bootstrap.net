namespace Egil.RazorComponents.Bootstrap.Utilities.Animations.Timers
{
    public class AnimationTimer : AnimationTimerBase
    {
        private readonly AnimationTimerCallback _callback;

        /// <summary>
        /// Instantiates a timer that will call the provided callback whenever it elapses.
        /// In Blazor, remember to use the <see cref="Microsoft.AspNetCore.Components.ComponentBase.Invoke(System.Action)"/> or 
        /// <see cref="Microsoft.AspNetCore.Components.ComponentBase.InvokeAsync(System.Func{System.Threading.Tasks.Task})"/> methods
        /// as wrapper for the callback. E.g.:
        /// <code>
        /// new AnimationTimer(async () => await InvokeAsync(SomeCallbackMethod));
        /// </code>
        /// </summary>
        /// <param name="callback"></param>
        public AnimationTimer(AnimationTimerCallback callback)
        {
            _callback = callback;
        }

        protected override void TimerElapsed()
        {
            _callback();
        }
    }

    public class AnimationTimer<TState> : AnimationTimerBase
    {
        private readonly AnimationTimerCallback<TState> _callback;
        private readonly TState _state;

        public AnimationTimer(AnimationTimerCallback<TState> callback, TState state)
        {
            _callback = callback;
            _state = state;
        }

        protected override void TimerElapsed()
        {
            _callback(_state);
        }
    }
}
