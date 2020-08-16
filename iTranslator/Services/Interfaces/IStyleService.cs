using System;

namespace iTranslator.Services.Interfaces
{
    public interface IStyleService
    {
        void RegisterStyle(Style style);

        Style FetchStyle(string styleName);
    }
}
