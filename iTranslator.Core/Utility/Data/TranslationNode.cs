using System;
using System.Collections.Generic;
using iTranslator.Enums;
using iTranslator.Utility.Extensions;

namespace iTranslator.Utility.Data
{
    public class TranslationNode
    {
        public List<TranslationNode> Neighbors;

        public TranslationNode(Record record)
        {
            Word = record.Word;
            Culture = record.Culture.ToCultureFromXml();
            Neighbors = new List<TranslationNode>();
        }

        public TranslationNode(Link link, TranslationNode parentNode)
        {
            Word = link.Word;
            Culture = link.Culture.ToCultureFromXml();
            Neighbors = new List<TranslationNode>()
            {
                parentNode
            };
        }

        public void AddNeighbor(TranslationNode node)
        {
            if (!Neighbors.Contains(node))
            {
                Neighbors.Add(node);
            }
        }

        public string Word { get; set; }
        public Culture Culture { get; set; }
        public bool Visited { get; set; }
    }
}
