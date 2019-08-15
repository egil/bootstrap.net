using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Egil.RazorComponents.Bootstrap.Documentation.Services
{
    public class AssemblyEmbeddedExampleComponentRepository : IExampleComponentRepository
    {
        private readonly Assembly _sourceAssembly;
        private readonly Dictionary<string, string> _cache = new Dictionary<string, string>();

        public AssemblyEmbeddedExampleComponentRepository(Assembly sourceAssembly)
        {
            _sourceAssembly = sourceAssembly;
        }

        public async Task<string> GetExampleAsync(string fullname)
        {
            if (!_cache.ContainsKey(fullname))
            {
                await FindExampleInAssembly(fullname);
            }

            return _cache[fullname];
        }

        private async Task FindExampleInAssembly(string fullname)
        {
            var file = $"{fullname}.razor";
            using var stream = _sourceAssembly.GetManifestResourceStream(file);
            
            if(stream is null) throw new InvalidOperationException($"The file '{file}' is not embedded in the assembly.");

            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            _cache.Add(fullname, content);
        }
    }
}
