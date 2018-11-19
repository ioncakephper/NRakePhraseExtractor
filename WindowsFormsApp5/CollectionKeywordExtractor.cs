//------------------------------------------------------------------------------
// <copyright file="collectionkeywordextractor.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using NRakeCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CollectionKeywordExtractor" />
    /// </summary>
    public class CollectionKeywordExtractor
    {
        private KeywordExtractor kwe;

        /// <summary>
        /// Defines the topics
        /// </summary>
        private readonly Topics topics;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        public CollectionKeywordExtractor(Topics topics) : this(topics, new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter())
        {
            this.topics = topics;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics"></param>
        /// <param name="filter"></param>
        public CollectionKeywordExtractor(Topics topics, NRakeCore.StopWordFilters.IStopWordFilter filter)
        {
            this.topics = topics;
            this.kwe = new KeywordExtractor(filter);
        }

        /// <summary>
        /// The GetText
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var topic in topics)
            {
                sb.Append(topic.GetText());
            }

            return sb.ToString();
        }

        /// <summary>
        /// The FindAllKeyPhrases
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Phrase}"/></returns>
        internal IEnumerable<Phrase> FindAllKeyPhrases()
        {
            Dictionary<string, KeyPhraseStats> keyphraseTopics = new Dictionary<string, KeyPhraseStats>();
            foreach (var topic in topics)
            {
                var topicKeyPhrases = kwe.FindKeyPhrases(topic.GetText());
                foreach (var keyPhrase in topicKeyPhrases)
                {
                    if (!keyphraseTopics.ContainsKey(keyPhrase))
                    {
                        keyphraseTopics.Add(keyPhrase, new KeyPhraseStats());
                    }
                    
                    keyphraseTopics[keyPhrase].Score += kwe.AggregatedLeagueTable[keyPhrase];
                    keyphraseTopics[keyPhrase].Topics.Add(topic);
                }             
            }
            var keyphrases = keyphraseTopics.OrderByDescending(k => k.Value.Score).Select(k => k.Key).ToArray();

            int rank = 0;
            return keyphrases.Select(keyphrase => new Phrase(keyphrase, keyphraseTopics[keyphrase].Score, keyphraseTopics[keyphrase].Topics, ++rank)).ToArray();            
        }
    }
}
