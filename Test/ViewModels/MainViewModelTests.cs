using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using iTranslator.Core.Services.Interfaces;
using iTranslator.Enums;
using iTranslator.Services;
using iTranslator.Services.Interfaces;
using iTranslator.Viewmodels;
using Moq;
using NUnit.Framework;

namespace Test.ViewModels
{
    [TestFixture()]
    public class MainViewModelTests
    {
        MainViewModel mainViewModel;

        public void Setup()
        {
            SimpleIoc.Default.Register<ITranslationService, TranslationService>();
            SimpleIoc.Default.Register<IFileStreamService, TestFileStreamService>();
            mainViewModel = new MainViewModel();
        }

        [Test]
        public async Task CheckViewModelInitialized()
        {
            Setup();
            Assert.NotNull(mainViewModel);
        }

        [Test]
        public async Task CheckTranslationsInitializedEmpty()
        {
            Setup();
            Assert.IsEmpty(mainViewModel.Translations);
        }

        [Test]
        public async Task CheckTranslationBasicGermanWord()
        {
            Setup();
            mainViewModel.SearchTerm = "Leistung";
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsNotEmpty(mainViewModel.Translations);
            Assert.AreEqual("German", mainViewModel.SearchTermCulture);
            Assert.True(mainViewModel.Translations.Any(x =>
            (x.Type == TranslationType.translation) &&
            (x.Word == "performance") &&
            (x.Culture == Culture.EN)));
        }

        [Test]
        public async Task CheckTranslationBasicEnglishWord()
        {
            Setup();
            mainViewModel.SearchTerm = "Success";
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsNotEmpty(mainViewModel.Translations);
            Assert.AreEqual("English", mainViewModel.SearchTermCulture);
            Assert.True(mainViewModel.Translations.Any(x =>
            (x.Type == TranslationType.translation) &&
            (x.Word == "Erfolg") &&
            (x.Culture == Culture.DE)));
        }

        [Test]
        public async Task CheckSynonymBasicEnglishWord()
        {
            Setup();
            mainViewModel.SearchTerm = "thrive";
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsNotEmpty(mainViewModel.Translations);
            Assert.AreEqual("English", mainViewModel.SearchTermCulture);
            Assert.True(mainViewModel.Translations.Any(x =>
            (x.Type == TranslationType.synonym) &&
            (x.Word == "flourishing") &&
            (x.Culture == Culture.EN)));
        }

        [Test]
        public async Task CheckSynonymBasicGermanWord()
        {
            Setup();
            mainViewModel.SearchTerm = "Resultat";
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsNotEmpty(mainViewModel.Translations);
            Assert.AreEqual("German", mainViewModel.SearchTermCulture);
            Assert.True(mainViewModel.Translations.Any(x =>
            (x.Type == TranslationType.synonym) &&
            (x.Word == "Ergebnis") &&
            (x.Culture == Culture.DE)));
        }

        [Test]
        public async Task CheckSearchIncorrectWord()
        {
            Setup();
            mainViewModel.SearchTerm = "abcd1234";
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsEmpty(mainViewModel.Translations);
            Assert.AreEqual("No Results", mainViewModel.SearchTermCulture);
        }

        [Test]
        public async Task CheckSearchEmptyWord()
        {
            Setup();
            mainViewModel.SearchTerm = string.Empty;
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsEmpty(mainViewModel.Translations);
            Assert.AreEqual("No Results", mainViewModel.SearchTermCulture);
        }

        [Test]
        public async Task CheckSearchTermNull()
        {
            Setup();
            mainViewModel.SearchTerm = null;
            mainViewModel.SubmitCommand.Execute(this);
            Assert.IsEmpty(mainViewModel.Translations);
            Assert.AreEqual("No Results", mainViewModel.SearchTermCulture);
        }

        [Test]
        public async Task CheckPredictiveSearchWorking()
        {
            Setup();
            mainViewModel.SearchTerm = string.Empty;
            mainViewModel.SearchTerm += 'S';
            mainViewModel.SearchTerm += 'e';
            mainViewModel.SearchTerm += 'q';
            Assert.IsNotEmpty(mainViewModel.translationBuffer);
            Assert.AreEqual("sequence", mainViewModel.mostRecentSuccessfulTranslation);
        }

        [Test]
        public async Task CheckPredictiveSearchNoPrediction()
        {
            Setup();
            mainViewModel.SearchTerm = string.Empty;
            mainViewModel.SearchTerm += '1';
            mainViewModel.SearchTerm += 'a';
            mainViewModel.SearchTerm += 'z';
            mainViewModel.SearchTerm += '~';
            Assert.IsEmpty(mainViewModel.translationBuffer);
            Assert.IsEmpty(mainViewModel.mostRecentSuccessfulTranslation);
        }
    }
}
