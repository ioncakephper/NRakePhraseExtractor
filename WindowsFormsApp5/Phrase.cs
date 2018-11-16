﻿//------------------------------------------------------------------------------
// <copyright file="Phrase.cs" company="Ion Giread">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    /// <summary>
    /// Defines the <see cref="Phrase" />
    /// </summary>
    public class Phrase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="keyword">The keyword<see cref="string"/></param>
        public Phrase(string keyword)
        {
            Text = keyword;
        }

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public string Text
        {
            get; set;
        }
    }
}
