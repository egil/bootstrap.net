using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public sealed class NoneAnimation : ICssClassAnimation
    {
        public bool Ready { get; } = true;

        public bool Running { get; } = false;

        public bool Completed { get; } = false;

        public int Count { get; } = 0;

        private NoneAnimation() { }

        public IEnumerator<string> GetEnumerator()
        {
            yield break;
        }

        public void Reset()
        {
        }

        public Task Run() => Task.CompletedTask;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public static readonly NoneAnimation Instance = new NoneAnimation();
    }


}
