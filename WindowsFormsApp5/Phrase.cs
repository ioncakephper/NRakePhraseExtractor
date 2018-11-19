//------------------------------------------------------------------------------
// <copyright file="phrase.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using NRakeCore;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Phrase" />
    /// </summary>
    public class Phrase
    {
        private List<Topic> topics;
        private int v;

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="string"/></param>
        public Phrase(string keyword)
        {
            Text = keyword;
            Words = keyword.Split(' ');
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="string"/></param>
        /// <param name="aggregatedLeagueTable">The aggregatedLeagueTable<see cref="SortedList{string, double}"/></param>
        public Phrase(string keyword, double score) : this(keyword)
        {
            Score = score;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="string"/></param>
        /// <param name="aggregatedLeagueTable">The aggregatedLeagueTable<see cref="SortedList{string, double}"/></param>
        /// <param name="rank">The rank<see cref="int"/></param>
        public Phrase(string keyword, double score, int rank) : this(keyword, score)
        {
            Rank = rank;
        }

        public Phrase(string keyword, double score, Topics topics, int rank) : this(keyword, score)
        {
            Topics = topics;
            Rank = rank;
        }



        /// <summary>
        /// Gets the Rank
        /// </summary>
        public int Rank { get; private set; }

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the Topics
        /// </summary>
        public Topics Topics { get; set; } = new Topics();

        /// <summary>
        /// Gets the Words
        /// </summary>
        public string[] Words { get; private set; }

        /// <summary>
        /// The TopicKeywords
        /// </summary>
        /// <param name="topic">The topic<see cref="Topic"/></param>
        /// <param name="stopFilter">The stopFilter<see cref="NRakeCore.StopWordFilters.IStopWordFilter"/></param>
        /// <returns>The <see cref="string[]"/></returns>
        private string[] TopicKeywords(Topic topic, NRakeCore.StopWordFilters.IStopWordFilter stopFilter)
        {
            var tke = new KeywordExtractor(stopFilter);
            return tke.FindKeyPhrases(topic.GetText());
        }
    }
}
