using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTranslator.Enums;
using iTranslator.ViewItems;

namespace iTranslator.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<bool> IsInDictionary(string word);
        bool FindLikelyMatch(string input, out string prediction);
        Task LoadTranslationData(string path);
        Task<List<TranslationViewItem>> GenerateTranslations(string searchTerm);
        Task<Culture> GetCultureForWord(string searchTerm);
    }
}
