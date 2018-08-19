using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nono.Engine.Tests.Suits
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

    public class NonFileReader : IDisposable
    {
        private Stream _stream;
        private bool disposedValue = false;

        public NonFileReader(Stream stream)
        {
            _stream = stream;
        }

        public TestCase Read()
        {
            _stream.Position = 0;

            uint height = 0;
            uint width = 0;

            uint[][] rows = null;
            uint[][] columns = null;

            using (var reader = new StreamReader(_stream))
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;

                    var splits = line.Split(" ");

                    switch (splits.FirstOrDefault())
                    {
                        case "width":
                            width = uint.Parse(splits[1]);
                            break;

                        case "height":
                            height = uint.Parse(splits[1]);
                            break;

                        case "columns":
                            columns = ReadNumberHints(reader, width);
                            break;

                        case "rows":
                            rows = ReadNumberHints(reader, height);
                            break;

                        case "goal": 

                        default:
                            continue;
                    }
                } while (true);

                return new Test;
            }
        }

        private uint[][] ReadNumberHints(StreamReader reader, uint height)
        {
            var list = new uint[height][];
            for (var i = 0; i < height; i++)
            {
                var line = reader.ReadLine();
                list[i] = line.Split(",").Select(x => uint.Parse(x)).ToArray();
            }

            return list;
        }
        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                _stream = null;

                disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }

}
