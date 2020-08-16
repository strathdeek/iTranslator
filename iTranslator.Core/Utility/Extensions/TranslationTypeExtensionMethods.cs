using System;
using iTranslator.Enums;

namespace iTranslator.Utility.Extensions
{
    public static class TranslationTypeExtensionMethods
    {
        const string TranslationString = "Translation";
        const string SynonymString = "Synonym";

        public static string Name(this TranslationType type)
        {
            switch (type)
            {
                case TranslationType.translation:
                    return TranslationString;
                case TranslationType.synonym:
                    return SynonymString;
                default:
                    return string.Empty;
            }
        }

        public static TranslationType ToTranslationType(this string type)
        {
            switch (type)
            {
                case TranslationString:
                    return TranslationType.translation;
                case SynonymString:
                    return TranslationType.synonym;
                default:
                    return TranslationType.translation;
            }
        }
    }
}
