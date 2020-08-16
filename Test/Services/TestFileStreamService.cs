using System.IO;
using System.Reflection;
using iTranslator.Core.Services.Interfaces;

namespace Test.ViewModels
{
    internal class TestFileStreamService : IFileStreamService
    {
        public StreamReader GetStreamReaderForFile(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "data.xml";
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            StreamReader reader = new StreamReader(stream);
            return reader;
        }
    }
}