﻿//------------------------------------------------------------------------------
// <copyright file="collectionkeywordextractor.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using NRakeCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CollectionKeywordExtractor" />
    /// </summary>
    public class CollectionKeywordExtractor : KeywordExtractor
    {
        /// <summary>
        /// Defines the topics
        /// </summary>
        private readonly Topics topics;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionKeywordExtractor"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        public CollectionKeywordExtractor(Topics topics) : base(new NRakeCore.StopWordFilters.EnglishSmartStopWordFilter())
        {
            this.topics = topics;
        }

        /// <summary>
        /// The FindPhrases
        /// </summary>
        /// <param name="text">The text<see cref="string"/></param>
        /// <returns>The <see cref="Phrase[]"/></returns>
        public Phrase[] FindPhrases(string text)
        {
            string[] keywords = FindKeyPhrases(text);
            int rank = 0;
            var phrases = keywords.Select(kw => new Phrase(kw, AggregatedLeagueTable, ++rank)).ToArray();

            return phrases;
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
            var keyphrases = FindKeyPhrases(GetText());

            int rank = 0;
            return keyphrases.Select(keyPhrase => new Phrase(keyPhrase, AggregatedLeagueTable, topics, StopWordFilter, ++rank)).Where(phrase => phrase.Topics.Count > 0).ToArray();
        }
    }
}
