using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nono.Engine.Tests
{
    public static class TestResources
    {
        public static IEnumerable<(string name, Stream stream)> GetAllFiles()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var embeddedTests = assembly.GetManifestResourceNames()
                .Where(x => x.EndsWith(".non"));

            return embeddedTests.Select<string, (string, Stream)>(
                name => (name, assembly.GetManifestResourceStream(name)));
        }
    }
}
