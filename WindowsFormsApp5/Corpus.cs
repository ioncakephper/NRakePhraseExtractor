//------------------------------------------------------------------------------
// <copyright file="Corpus.cs" company="Ion Giread">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    /// <summary>
    /// Defines the <see cref="Corpus" />
    /// </summary>
    public class Corpus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Corpus"/> class.
        /// </summary>
        public Corpus()
        {
            Topics = new Topics();
            Phrases = new Phrases();
        }

        /// <summary>
        /// Gets or sets the Phrases
        /// </summary>
        public Phrases Phrases { get; set; }

        /// <summary>
        /// Gets or sets the Topics
        /// </summary>
        public Topics Topics { get; set; }
    }
}
