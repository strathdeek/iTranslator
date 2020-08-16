using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using iTranslator.Core.Services.Interfaces;
using iTranslator.Enums;
using iTranslator.Services;
using NUnit.Framework;
using Test.ViewModels;

namespace Test.Services
{
    [TestFixture]
    public class TranslationServiceTests
    {
        TranslationService translationService;

        private List<(string, Culture)> cultures;
        private List<(string, string)> translations;
        private List<(string, string)> synonyms;

        private async Task Setup()
        {
            SimpleIoc.Default.Register<IFileStreamService, TestFileStreamService>();
            translationService = new TranslationService();
            await InitializeTestData();
        }

        private async Task InitializeTestData()
        {
            await translationService.LoadTranslationData("data.xml");

            cultures = new List<(string, Culture)>()
            {
                ("Erfolg", Culture.DE),
                ("Gelingen", Culture.DE),
                ("Gedeihen", Culture.DE),
                ("Ergebnis", Culture.DE),
                ("Resultat", Culture.DE),
                ("Folge", Culture.DE),
                ("Leistung", Culture.DE),
                ("Erfüllung", Culture.DE),
                ("Erzielen", Culture.DE),
                ("Erfüllung", Culture.DE),
                ("success", Culture.EN),
                ("result", Culture.EN),
                ("achievement", Culture.EN),
                ("thrive", Culture.EN),
                ("flourishing", Culture.EN),
                ("successful outcome", Culture.EN),
                ("outcome", Culture.EN),
                ("effect", Culture.EN),
                ("episode", Culture.EN),
                ("sequence", Culture.EN),
                ("performance", Culture.EN),
                ("power", Culture.EN),
                ("effort", Culture.EN),
                ("fulfillment", Culture.EN),
                ("satisfaction", Culture.EN)
            };

            translations = new List<(string, string)>()
            {
                ("Erfolg", "success"),
                ("Gelingen", "successful outcome"),
                ("Gedeihen", "thrive"),
                ("Ergebnis", "result"),
                ("Resultat", "outcome"),
                ("Folge", "episode"),
                ("Leistung", "performance"),
                ("Erfüllung", "fulfillment"),
                ("Erzielen", "achievement"),
                ("success", "Gelingen"),
                ("result", "Resultat"),
                ("achievement", "Erfüllung"),
                ("thrive", "Gedeihen"),
                ("flourishing", "Gedeihen"),
                ("successful outcome", "Gelingen"),
                ("outcome", "Resultat"),
                ("effect", "Folge"),
                ("episode", "Folge"),
                ("sequence", "Folge"),
                ("performance", "Leistung"),
                ("power", "Leistung"),
                ("effort", "Leistung"),
                ("fulfillment", "Erfüllung"),
                ("satisfaction", "Erfüllung")
            };

            synonyms = new List<(string, string)>()
            {
                ("Erfolg", "Gelingen"),
                ("Gelingen", "Gedeihen"),
                ("Ergebnis", "Resultat"),
                ("Resultat", "Ergebnis"),
                ("Folge", "Erfolg"),
                ("Leistung", "Erzielen"),
                ("Erfüllung", "Leistung"),
                ("Erzielen", "Erfüllung"),
                ("success", "achievement"),
                ("result", "outcome"),
                ("achievement", "performance"),
                ("thrive", "success"),
                ("flourishing", "thrive"),
                ("successful outcome", "success"),
                ("outcome", "result"),
                ("effect", "outcome"),
                ("episode", "sequence"),
                ("performance", "effort"),
                ("power", "effort"),
                ("fulfillment", "satisfaction"),
            };
        }

        [Test]
        public async Task CheckCulturesIdentifiedCorrectly()
        {
            await Setup();
            foreach (var word in cultures)
            {
                Assert.AreEqual(word.Item2, await translationService.GetCultureForWord(word.Item1));
            }
        }

        [Test]
        public async Task CheckTranslationsPerformedCorrectly()
        {
            await Setup();
            foreach (var word in translations)
            {
                var firstWordTranslations = await translationService.GenerateTranslations(word.Item1);
                var secondWordTranslations = await translationService.GenerateTranslations(word.Item2);
                Assert.IsTrue(firstWordTranslations.Any(x => x.Type == TranslationType.translation && x.Word.Equals(word.Item2)));
                Assert.IsTrue(secondWordTranslations.Any(x => x.Type == TranslationType.translation && x.Word.Equals(word.Item1)));
            }
        }

        [Test]
        public async Task CheckSynonymsPerformedCorrectly()
        {
            await Setup();
            foreach (var word in synonyms)
            {
                var firstWordSynonyms = await translationService.GenerateTranslations(word.Item1);
                var secondWordSynonyms = await translationService.GenerateTranslations(word.Item2);
                var firstSynonym = firstWordSynonyms.FirstOrDefault(x => x.Type == TranslationType.synonym && x.Word.Equals(word.Item2));
                Assert.AreEqual(word.Item2, firstSynonym?.Word);
                var secondSynonym = secondWordSynonyms.FirstOrDefault(x => x.Type == TranslationType.synonym && x.Word.Equals(word.Item1));
                Assert.AreEqual(word.Item1, secondSynonym?.Word);
            }
        }
    }
}
