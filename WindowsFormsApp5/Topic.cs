//------------------------------------------------------------------------------
// <copyright file="topic.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using HtmlAgilityPack;

    /// <summary>
    /// Defines the <see cref="Topic" />
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Defines the _filepath
        /// </summary>
        private string _filepath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Topic"/> class.
        /// </summary>
        public Topic()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Topic"/> class.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        public Topic(string filePath) : this()
        {
            Filepath = filePath;
            Title = GetTitle();
        }

        /// <summary>
        /// Gets or sets the Filename
        /// </summary>
        public string Filename { get; internal set; }

        /// <summary>
        /// Gets or sets the Filepath
        /// </summary>
        public string Filepath
        {
            get { return _filepath; }
            set
            {
                _filepath = value;
                Filename = System.IO.Path.GetFileName(_filepath);
                Folder = System.IO.Path.GetDirectoryName(_filepath);
            }
        }

        /// <summary>
        /// Gets or sets the Folder
        /// </summary>
        public string Folder { get; internal set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The GetText
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetText()
        {
            // From File
            var doc = new HtmlDocument();
            doc.Load(_filepath);

            var node = doc.DocumentNode.SelectSingleNode("//body");
            string text = (node == null) ? string.Empty : node.InnerText;

            return text;
        }

        /// <summary>
        /// The GetTitle
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public virtual string GetTitle()
        {
            // From File
            var doc = new HtmlDocument();
            doc.Load(_filepath);

            var node = doc.DocumentNode.SelectSingleNode("//head/title");
            string title = (node == null) ? string.Empty : node.InnerText;

            return title;
        }
    }
}
