using System;
using System.Diagnostics;
using System.Threading;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations.Timers
{
    public abstract class AnimationTimerBase : IDisposable
    {
        private bool _disposed = false;
        private TimeSpan _interval = Timeout.InfiniteTimeSpan;
        private readonly Timer _timer;
        private readonly Stopwatch _stopWatch = new Stopwatch();

        /// <summary>
        /// Gets the Elapsed time during the current cycle of the timer. 
        /// The Elapsed time resets after each callback.
        /// </summary>
        public TimeSpan Elapsed => _stopWatch.Elapsed;

        /// <summary>
        /// Gets or sets the DueTime of the timer. The DueTime is the first time the
        /// callback is triggered after <see cref="Start"/> is called.
        /// </summary>
        public TimeSpan DueTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Gets or sets the interval for which the callback is triggered.
        /// If the timers <see cref="Status"/> is running, the <see cref="DueTime"/>
        /// is updated to match the interval.
        /// </summary>
        public TimeSpan Interval
        {
            get => _interval; set
            {
                _interval = value;
                if (Status == AnimationTimerStatus.Running)
                {
                    DueTime = value;
                    Start();
                }
            }
        }

        /// <summary>
        /// Gets the status of the timer.
        /// </summary>
        public AnimationTimerStatus Status { get; private set; } = AnimationTimerStatus.Stopped;

        /// <summary>
        /// True if timer is stopped.
        /// </summary>
        public bool IsStopped => Status == AnimationTimerStatus.Stopped;

        /// <summary>
        /// True if timer is running.
        /// </summary>
        public bool IsRunning => Status == AnimationTimerStatus.Running;

        /// <summary>
        /// True if timer is paused.
        /// </summary>
        public bool IsPaused => Status == AnimationTimerStatus.Paused;

        protected AnimationTimerBase()
        {
            _timer = new Timer(TimerElapsed, this, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            Interval = Timeout.InfiniteTimeSpan;
        }

        /// <summary>
        /// Starts or restarts the timer.
        /// </summary>
        public void Start()
        {
            _timer.Change(DueTime, Interval);
            _stopWatch.Restart();
            Status = AnimationTimerStatus.Running;
        }

        /// <summary>
        /// Stops the timer and resets the <see cref="Elapsed"/> time. No callbacks will happen after the timer is stopped.
        /// </summary>
        public void Stop()
        {
            _timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            _stopWatch.Reset();
            Status = AnimationTimerStatus.Stopped;
        }

        /// <summary>
        /// Pauses the timer. No callbacks will happen before the timer is resumed via the <see cref="Resume"/> method.
        /// </summary>
        public void Pause()
        {
            _timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            _stopWatch.Stop();
            Status = AnimationTimerStatus.Paused;
        }

        /// <summary>
        /// Resumes the timer at the <see cref="Elapsed"/> time, if the timers <see cref="Status"/> is AnimationTimerStatus.Paused.
        /// Otherwise, it will redirect to <see cref="Start"/>.
        /// </summary>
        public void Resume()
        {
            if (Status == AnimationTimerStatus.Paused)
            {
                _timer.Change(Elapsed, Interval);
                _stopWatch.Start();
                Status = AnimationTimerStatus.Running;
            }
            else
            {
                Start();
            }
        }

        /// <summary>
        /// The TimerElapsed method is called each time the timer elapses.
        /// </summary>
        protected abstract void TimerElapsed();

        private void TimerElapsedBase()
        {
            TimerElapsed();
            _stopWatch.Restart();
        }

        private static void TimerElapsed(object state)
        {
            var animationTimer = (AnimationTimer)state;
            animationTimer.TimerElapsedBase();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();
                    _timer.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
