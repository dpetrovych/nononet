using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nono.Engine.Tests
{
    public static class TestResources
    {
        public static IEnumerable<Stream> GetAllFiles()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var embeddedTests = assembly.GetManifestResourceNames()
                .Where(x => x.EndsWith(".non"));

            return embeddedTests.Select<string, Stream>(
                name => assembly.GetManifestResourceStream(name));
        }
    }
}
