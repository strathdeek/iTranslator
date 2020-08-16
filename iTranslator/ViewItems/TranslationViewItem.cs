using System;
using iTranslator.Enums;
using iTranslator.Utility.Data;

namespace iTranslator.ViewItems
{
    public class TranslationViewItem
    {
        public TranslationViewItem()
        {
        }

        public TranslationViewItem(string word, Culture language, TranslationType type)
        {
            Word = word;
            Culture = language;
            Type = type;
        }

        public string Word { get; set; }
        public Culture Culture { get; set; }
        public TranslationType Type { get; set; }
        public int SynonymRelevance { get; set; }
        public int SynonymOccurances { get; set; }
        public float SynonymScore {get; set;}
    }
}
