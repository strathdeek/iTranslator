using System;
using System.IO;
using iTranslator.Core.Services.Interfaces;

namespace iTranslator.Services
{
    public class FileStreamService : IFileStreamService
    {
        public FileStreamService()
        {
        }

        public StreamReader GetStreamReaderForFile(string path)
        {
            return new StreamReader(path);
        }
    }
}
