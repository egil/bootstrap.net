using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public interface ICssClassAnimation : ICssClassProvider
    {
        public bool Ready { get; }

        public bool Running { get; }

        public bool Completed { get; }
        public void Reset();

        public Task Run();
    }
}
