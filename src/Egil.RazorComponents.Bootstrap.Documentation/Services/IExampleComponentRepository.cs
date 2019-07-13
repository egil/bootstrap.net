using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Documentation.Services
{
    public interface IExampleComponentRepository
    {
        public Task<string> GetExampleAsync(string fullname);
    }
}
