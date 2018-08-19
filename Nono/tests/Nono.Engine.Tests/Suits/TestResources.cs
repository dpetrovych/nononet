using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nono.Engine.Tests.Suits
{
    public static class TestResources
    {
        public static IEnumerable<(string, Stream)> GetFiles(string filepath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyResources = assembly.GetManifestResourceNames();

            var fileNameStart = $"{assembly.GetName().Name}.{filepath}";

            var embeddedTests = assemblyResources.Where(x => x.EndsWith(".non") && x.StartsWith(fileNameStart));

            return embeddedTests.Select(
                name => (name.Remove(0, fileNameStart.Length + 1), assembly.GetManifestResourceStream(name)));
        }
    }

}
