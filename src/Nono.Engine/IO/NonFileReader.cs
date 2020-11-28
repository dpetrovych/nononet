using System;
using System.IO;
using System.Linq;

namespace Nono.Engine.IO
{
    public class NonFileReader : IDisposable
    {
        private readonly string _name;
        private Stream? _stream;
        private bool disposedValue = false;

        public NonFileReader(FileStream stream)
            : this(stream.Name, stream)
        {
        }

        public NonFileReader(string name, Stream stream)
        {
            _name = name;
            _stream = stream;
        }

        public Nonogram Read()
        {
            if (_stream == null)
                throw new ObjectDisposedException(nameof(_stream));

            _stream.Position = 0;

            int height = 0;
            int width = 0;

            int[][]? rows = null;
            int[][]? columns = null;

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

                        default:
                            continue;
                    }
                } while (true);

                return new Nonogram(rows!, columns!, _name);
            }
        }

        private int[][] ReadNumberHints(StreamReader reader, int height)
        {
            var list = new int[height][];
            for (var i = 0; i < height; i++)
            {
                var line = reader.ReadLine();
                list[i] = line.Split(",").Select(x => int.Parse(x)).ToArray();
            }

            return list;
        }
        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _stream?.Dispose();
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
