//------------------------------------------------------------------------------
// <copyright file="phrase.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Phrase" />
    /// </summary>
    public class Phrase
    {
        /// <summary>
        /// Defines the v
        /// </summary>
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
        public Phrase(string keyword, SortedList<string, double> aggregatedLeagueTable) : this(keyword)
        {
            Score = aggregatedLeagueTable[keyword];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="string"/></param>
        /// <param name="aggregatedLeagueTable">The aggregatedLeagueTable<see cref="SortedList{string, double}"/></param>
        /// <param name="rank">The rank<see cref="int"/></param>
        public Phrase(string keyword, SortedList<string, double> aggregatedLeagueTable, int rank) : this(keyword, aggregatedLeagueTable)
        {
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
        public Topics Topics { get; internal set; } = new Topics();

        /// <summary>
        /// Gets the Words
        /// </summary>
        public string[] Words { get; private set; } = new string[] { };
    }
}
