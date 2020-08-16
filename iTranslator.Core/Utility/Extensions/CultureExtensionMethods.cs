using System;
using iTranslator.Enums;

namespace iTranslator.Utility.Extensions
{
    public static class LanguageExtensionMethods
    {
        const string EnglishString = "English";
        const string GermanString = "German";

        const string EnglishXmlString = "en-US";
        const string GermanXmlString = "de-DE";

        public static string Name(this Culture culture)
        {
            switch (culture)
            {
                case Culture.EN:
                    return EnglishString;
                case Culture.DE:
                    return GermanString;
                default:
                    return string.Empty;
            }
        }

        public static Culture ToCulture(this string culture)
        {
            switch (culture)
            {
                case EnglishString:
                    return Culture.DE;
                case GermanString:
                    return Culture.EN;
                default:
                    return Culture.DE;
            }
        }

        public static Culture ToCultureFromXml(this string culture)
        {
            switch (culture)
            {
                case EnglishXmlString:
                    return Culture.EN;
                case GermanXmlString:
                    return Culture.DE;
                default:
                    return Culture.DE;
            }
        }
    }
}
