//------------------------------------------------------------------------------
// <copyright file="phrases.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Phrases" />
    /// </summary>
    public class Phrases : List<Phrase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Phrases"/> class.
        /// </summary>
        public Phrases() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrases"/> class.
        /// </summary>
        /// <param name="collection">The collection<see cref="IEnumerable{Phrase}"/></param>
        public Phrases(IEnumerable<Phrase> collection) : base(collection)
        {
        }
    }
}
