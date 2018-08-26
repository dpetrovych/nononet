using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Nono.Engine.Tests.Data
{
    public class TestFilesAttribute : DataAttribute
    {
        private readonly string _filePath;

        public TestFilesAttribute(string filePath)
        {
            _filePath = filePath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var testFiles = TestResources.GetFiles(_filePath);

            foreach (var (name, stream) in testFiles)
            {
                using (var reader = new NonFileReader(name, stream))
                {
                    var testCase = reader.Read();

                    yield return new object[] { testCase };
                }
            }
        }
    }
}
