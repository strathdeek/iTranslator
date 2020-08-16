using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using iTranslator.Enums;
using iTranslator.Services.Interfaces;
using iTranslator.Utility.Extensions;
using iTranslator.ViewItems;

namespace iTranslator.Viewmodels
{
    public class MainViewModel : ViewModelBase
    {

        readonly ITranslationService translationService;

        private const string translationFilePath = "data.xml";

        private string mostRecentSuccessfulTranslation;
        private List<TranslationViewItem> translationBuffer;

        public event EventHandler<bool> UpdateUI;

        public MainViewModel()
        {
            Translations = new ObservableCollection<TranslationViewItem>();
            translationBuffer = new List<TranslationViewItem>();
            mostRecentSuccessfulTranslation = string.Empty;
            translationService = SimpleIoc.Default.GetInstance<ITranslationService>();
            InitializeTranslationService();
        }

        private async Task InitializeTranslationService()
        {
            await translationService.LoadTranslationData(translationFilePath);
        }

        public ObservableCollection<TranslationViewItem> Translations { get; set; }

        public string HeaderText => "iTranslator";

        public string SubmitButtonText => "Submit";

        public RelayCommand SubmitCommand => new RelayCommand( async () =>
        {
            Translations.Clear();
            if (mostRecentSuccessfulTranslation.Equals(SearchTerm) ||
                await SearchForTranslationAsync(SearchTerm))
            {
                SearchTermCulture = (await translationService.GetCultureForWord(SearchTerm)).Name();
                foreach (var translation in translationBuffer)
                {
                    Translations.Add(translation);
                }
                UpdateUI?.Invoke(this, true);
            }
            else
            {
                SearchTermCulture = "No Results";
                UpdateUI?.Invoke(this, false);
            }
        });

        private async Task<bool> SearchForTranslationAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return false;
            }
            if (searchTerm.Equals(mostRecentSuccessfulTranslation))
            {
                return true;
            }

            string likelyMatch;
            string wordToTranslate;

            if (await translationService.IsInDictionary(searchTerm))
            {
                // We can translate search term directly
                wordToTranslate = searchTerm;
            }
            else if(translationService.FindLikelyMatch(searchTerm,out likelyMatch))
            {
                // We cannot translate search term, but searchterm matches a substring of a word that we can translate
                if (likelyMatch.Equals(mostRecentSuccessfulTranslation))
                {
                    // Already translated this word, but the search term is not a match
                    return false;
                }
                wordToTranslate = likelyMatch;
            }
            else
            {
                // We cannot translate search term and it does not match anthing in our dictionary
                return false;
            }
            var intermediateResults = await translationService.GenerateTranslations(wordToTranslate);

            var sortedResults = SortTranslationListViewItems(intermediateResults);
            
            translationBuffer = sortedResults;
            mostRecentSuccessfulTranslation = wordToTranslate;

            return wordToTranslate == searchTerm;
        }

        private List<TranslationViewItem> SortTranslationListViewItems(IEnumerable<TranslationViewItem> translationViewItems)
        {
            var sortedResults = new List<TranslationViewItem>();
            var translations = translationViewItems.Where(x => x.Type == TranslationType.translation);
            var synonyms = translationViewItems.Where(x => x.Type == TranslationType.synonym);

            // Rank synonyms by relevance (search depth) and occurances.
            float maxScore = synonyms.Max(x => x.SynonymOccurances * (1f / (float)x.SynonymRelevance));
            foreach (var synonym in synonyms)
            {
                synonym.SynonymScore = (float)(synonym.SynonymOccurances * (1f / (float)synonym.SynonymRelevance)) / maxScore;
            }

            sortedResults.AddRange(translations);
            sortedResults.AddRange(synonyms.OrderByDescending(x => x.SynonymScore));
            return sortedResults;
        }


        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                searchTerm = value;
                SearchForTranslationAsync(searchTerm);
                RaisePropertyChanged(()=>SearchTerm);
            }
        }

        private string searchTermCulture = string.Empty;
        public string SearchTermCulture
        {
            get
            {
                return searchTermCulture;
            }
            set
            {
                searchTermCulture = value;
                RaisePropertyChanged(()=> SearchTermCulture);
            }
        }

    }
}
