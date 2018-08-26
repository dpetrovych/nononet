using System;
using System.IO;
using System.Linq;
using Nono.Engine.Models;

namespace Nono.Engine.Tests.Suits
{
    public class NonFileReader : IDisposable
    {
        private readonly string _name;
        private Stream _stream;
        private bool _disposedValue = false;

        public NonFileReader(string name, Stream stream)
        {
            _name = name;
            _stream = stream;
        }

        public TestCase Read()
        {
            _stream.Position = 0;

            int height = 0;
            int width = 0;

            string title = null;

            uint[][] rows = null;
            uint[][] columns = null;

            Field goal = null;

            using (var reader = new StreamReader(_stream))
            {
                string line;
                do
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;

                    var splits = line.Split(" ", 2);

                    switch (splits.FirstOrDefault())
                    {
                        case "title":
                            title = splits[1].Replace("\"", string.Empty);
                            break;

                        case "width":
                            width = int.Parse(splits[1]);
                            break;

                        case "height":
                            height = int.Parse(splits[1]);
                            break;

                        case "columns":
                            columns = ReadNumberHints(reader, width);
                            break;

                        case "rows":
                            rows = ReadNumberHints(reader, height);
                            break;

                        case "goal":
                            var goalStreak = splits[1]
                                .Where(c => c == '0' || c == '1')
                                .Select(c => c == '0' ? Box.CrossedOut : Box.Filled)
                                .ToArray();

                            goal = new Field(goalStreak, height, width);
                            break;

                        default:
                            continue;
                    }
                } while (true);
                
                return new TestCase($"{title} ({_name})", rows, columns, goal);
            }
        }

        private uint[][] ReadNumberHints(StreamReader reader, int height)
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
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                _stream = null;

                _disposedValue = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
    }

}
