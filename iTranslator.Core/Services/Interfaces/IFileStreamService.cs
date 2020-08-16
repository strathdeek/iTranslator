using System;
using System.IO;

namespace iTranslator.Core.Services.Interfaces
{
    public interface IFileStreamService
    {
        StreamReader GetStreamReaderForFile(string path);
    }
}
