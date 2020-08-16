using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GalaSoft.MvvmLight.Ioc;
using iTranslator.Core.Services.Interfaces;
using iTranslator.Enums;
using iTranslator.Services.Interfaces;
using iTranslator.Utility.Data;
using iTranslator.ViewItems;

namespace iTranslator.Services
{
    public class TranslationService : ITranslationService
    {
        private Translations translations;

        private List<TranslationNode> graph;

        private bool initializing;
        private bool isInitialized;
        private string defaultDictionaryPath = "data.xml";

        public TranslationService()
        {
            isInitialized = false;
            initializing = false;
        }

        public async Task<Culture> GetCultureForWord(string searchTerm)
        {
            await InitializeIfNecessary();
            var word = graph.FirstOrDefault(x => x.Word.ToLower().Equals(searchTerm.ToLower()));
            return word.Culture;
        }

        public async Task<bool> IsInDictionary(string word)
        {
            await InitializeIfNecessary();
            var found = graph.Any(x => x.Word.ToLower().Equals(word.ToLower()));
            return found;
        }

        public bool FindLikelyMatch(string input, out string prediction)
        {
            var potentialMatches = graph.Where(x => x.Word.Length > input.Length);

            var matches = potentialMatches.Where(x => x.Word.Substring(0, input.Length).ToLower().Equals(input.ToLower()));

            if (matches.Count() == 1)
            {
                prediction = matches.First().Word;
                return true;
            }
            else
            {
                prediction = string.Empty;
                return false;
            }
        }

        public async Task<List<TranslationViewItem>> GenerateTranslations(string searchTerm)
        {
            await InitializeIfNecessary();
            List<TranslationViewItem> results = new List<TranslationViewItem>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var startNode = graph.FirstOrDefault(x => x.Word.ToLower().Equals(searchTerm.ToLower()));
                if (startNode != null)
                {
                    List<TranslationViewItem> translations = FindTranslations(startNode);
                    results.AddRange(translations);
                    List<TranslationViewItem> synonyms = FindSynonyms(startNode);
                    results.AddRange(synonyms);
                }
            }
            return results;
        }

        private List<TranslationViewItem> FindTranslations(TranslationNode startNode)
        {
            List<TranslationViewItem> results = new List<TranslationViewItem>();
            foreach (var node in startNode.Neighbors)
            {
                if (node.Culture != startNode.Culture)
                {
                    results.Add(new TranslationViewItem()
                    {
                        Word = node.Word,
                        Culture = node.Culture,
                        Type = TranslationType.translation
                    });
                }
            }
            return results;
        }

        private List<TranslationViewItem> FindSynonyms(TranslationNode startNode)
        {
            List<TranslationViewItem> results = new List<TranslationViewItem>();
            int relevance = 1;
            var currentLevel = startNode.Neighbors;
            while (currentLevel.Any())
            {
                List<TranslationNode> nextLevel = new List<TranslationNode>();
                foreach (var node in currentLevel.Where(x => !x.Visited && x != startNode))
                {
                    if (node.Culture == startNode.Culture)
                    {
                        if (!results.Any(x => x.Word.Equals(node.Word)))
                        {
                            results.Add(new TranslationViewItem()
                            {
                                Word = node.Word,
                                Culture = node.Culture,
                                Type = TranslationType.synonym,
                                SynonymRelevance = relevance,
                                SynonymOccurances = 1
                            });
                        }
                        else
                        {
                            results.First(x => x.Word.Equals(node.Word)).SynonymRelevance++;
                        }
                    }
                    if (!node.Visited)
                    {
                        nextLevel.AddRange(node.Neighbors);
                    }
                    node.Visited = true;
                }
                relevance++;
                currentLevel = nextLevel;
            }
            foreach (var node in graph)
            {
                node.Visited = false;
            }
            return results;
        }

        public async Task LoadTranslationData(string path)
        {
            if (initializing)
            {
                return;
            }
            initializing = true;
            XmlSerializer serializer = new XmlSerializer(typeof(Translations));

            var reader = SimpleIoc.Default.GetInstance<IFileStreamService>().GetStreamReaderForFile(path);
            translations = (Translations)serializer.Deserialize(reader);
            reader.Dispose();

            await GenerateGraph();
            isInitialized = true;

        }

        private async Task GenerateGraph()
        {
            graph = new List<TranslationNode>();
            foreach (var record in translations.Records)
            {
                TranslationNode recordNode;
                if (!graph.Any(x => x.Word.Equals(record.Word)))
                {
                    recordNode = new TranslationNode(record);
                    graph.Add(recordNode);
                }
                else
                {
                    recordNode = graph.First(x => x.Word == record.Word);
                }

                foreach (var link in record.Links)
                {
                    TranslationNode linkNode;
                    if (!graph.Any(x=> x.Word.Equals(link.Word)))
                    {
                        linkNode = new TranslationNode(link, recordNode);
                        graph.Add(linkNode);
                    }
                    else
                    {
                        linkNode = graph.First(x => x.Word.Equals(link.Word));
                    }
                    recordNode.AddNeighbor(linkNode);
                }
            }
        }

        private async Task InitializeIfNecessary()
        {
            if (!isInitialized)
            {
                await LoadTranslationData(defaultDictionaryPath);
            }
        }

        
    }
}
