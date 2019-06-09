using Egil.RazorComponents.Bootstrap.Parameters;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Utilities.Animations
{
    public interface ICssClassAnimation : ICssClassParameter
    {
        public bool Ready { get; }

        public bool Running { get; }

        public bool Completed { get; }
        public void Reset();

        public Task Run();
    }
}
