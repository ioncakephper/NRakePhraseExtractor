//------------------------------------------------------------------------------
// <copyright file="InvalidFilenameException.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="InvalidFilenameException" />
    /// </summary>
    [Serializable]
    public class InvalidFilenameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFilenameException"/> class.
        /// </summary>
        public InvalidFilenameException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFilenameException"/> class.
        /// </summary>
        /// <param name="path">The path<see cref="string"/></param>
        public InvalidFilenameException(string path) : base(string.Format(@"Invalid path: {0}", path))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFilenameException"/> class.
        /// </summary>
        /// <param name="path">The path<see cref="string"/></param>
        /// <param name="innerException">The innerException<see cref="Exception"/></param>
        public InvalidFilenameException(string path, Exception innerException) : base(string.Format(@"Invalid path: {0}", path), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFilenameException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/></param>
        /// <param name="context">The context<see cref="StreamingContext"/></param>
        protected InvalidFilenameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
