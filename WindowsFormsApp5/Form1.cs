//------------------------------------------------------------------------------
// <copyright file="form1.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using NRakeCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="Form1" />
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            Corpus = new Corpus();
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        /// <param name="corpus">The corpus<see cref="Corpus"/></param>
        public Form1(Corpus corpus) : this()
        {
            Corpus = corpus;
        }

        /// <summary>
        /// Gets or sets the Corpus
        /// </summary>
        public Corpus Corpus { get; set; }

        /// <summary>
        /// The aboutToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutBox1();
            d.ShowDialog();
        }

        /// <summary>
        /// The ContainsFilter
        /// </summary>
        /// <param name="item">The item<see cref="ListViewItem"/></param>
        /// <param name="filter">The filter<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ContainsFilter(ListViewItem item, string filter)
        {
            if (item.Text.Contains(filter))
            {
                return true;
            }
            foreach (var subItem in item.SubItems)
            {
                if (subItem.ToString().Contains(filter))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The CreatePhraseListItem
        /// </summary>
        /// <param name="phrase">The phrase<see cref="Phrase"/></param>
        /// <param name="doc">The doc<see cref="XmlDocument"/></param>
        /// <returns>The <see cref="XmlElement"/></returns>
        private XmlElement CreatePhraseListItem(Phrase phrase, XmlDocument doc)
        {
            var li = doc.CreateElement("LI");

            var objectElement = doc.CreateElement("object");
            objectElement.SetAttribute("type", "text/sitemap");

            var paramPhrase = doc.CreateElement("param");
            paramPhrase.SetAttribute("name", "Name");
            paramPhrase.SetAttribute("value", phrase.Text);

            objectElement.AppendChild(paramPhrase);

            foreach (Topic topic in GetTopicsOf(phrase))
            {
                var paramName = doc.CreateElement("param");
                paramName.SetAttribute("name", "Name");
                paramName.SetAttribute("value", topic.Title);

                var paramLocal = doc.CreateElement("param");
                paramLocal.SetAttribute("name", "Local");
                paramLocal.SetAttribute("value", topic.Filepath);

                objectElement.AppendChild(paramName);
                objectElement.AppendChild(paramLocal);
            }

            li.AppendChild(objectElement);
            return li;
        }

        /// <summary>
        /// The customizeToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new CustomizeDialog();
            d.ShowDialog();
        }

        /// <summary>
        /// The ExportPhrases
        /// </summary>
        private void ExportPhrases()
        {
            if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                var fileName = saveFileDialog1.FileName;

                var webBrowser = new WebBrowser();
                // var doc = webBrowser.Document;
                var doc = new XmlDocument();

                var html = doc.CreateElement("HTML");

                var head = doc.CreateElement("HEAD");
                var meta = doc.CreateElement("META");
                meta.SetAttribute("name", "Generator");
                meta.SetAttribute("content", "Microsoft HTML Help Workshop 4.1");

                var comment = doc.CreateComment(" Sitemap 1.0 ");
                head.AppendChild(meta);
                head.AppendChild(comment);

                var ul = doc.CreateElement("UL");
                foreach (var phrase in Corpus.Phrases)
                {
                    ul.AppendChild(CreatePhraseListItem(phrase, doc));
                }

                var body = doc.CreateElement("BODY");
                body.AppendChild(ul);

                html.AppendChild(head);
                html.AppendChild(body);

                doc.AppendChild(html);

                doc.Save(fileName);
            }
        }

        /// <summary>
        /// The ExtractPhrases
        /// </summary>
        private void ExtractPhrases()
        {
            var d = new SelectedTopicsDialog(Corpus.Topics);
            if (d.ShowDialog().Equals(DialogResult.OK))
            {
                var topics = d.Topics;
                Corpus.Phrases = GetTopicPhrases(topics);
                PopulatePhraseListView(Corpus.Phrases);
                changeDetector1.Changed = true;
            }
        }

        /// <summary>
        /// The Form1_FormClosing
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = changeDetector1.RequestDecision();
        }

        /// <summary>
        /// The GetPhraseTopics
        /// </summary>
        /// <param name="phrase">The phrase<see cref="Phrase"/></param>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        /// <param name="singleKeywordExtractor">The singleKeywordExtractor<see cref="KeywordExtractor"/></param>
        private void GetPhraseTopics(Phrase phrase, Topics topics, KeywordExtractor singleKeywordExtractor)
        {
            foreach (var topic in topics)
            {
                if (singleKeywordExtractor.FindKeyPhrases(topic.GetText()).Contains(phrase.Text))
                {
                    phrase.Topics.Add(topic);
                }
            }
        }

        /// <summary>
        /// The GetSinglePhraseListViewItem
        /// </summary>
        /// <param name="phrase">The phrase<see cref="Phrase"/></param>
        /// <returns>The <see cref="ListViewItem"/></returns>
        private ListViewItem GetSinglePhraseListViewItem(Phrase phrase)
        {
            ListViewItem item = new ListViewItem(phrase.Text);
            item.Tag = phrase;
            var subItems = new string[phrasesListView.Columns.Count - 1];
            for (int i = 0; i < subItems.Length; i++)
            {
                switch (phrasesListView.Columns[i + 1].Text)
                {
                    case "Words":
                        subItems[i] = phrase.Words.Length.ToString();
                        break;
                    case "Score":
                        subItems[i] = phrase.Score.ToString();
                        break;
                    case "Rank":
                        subItems[i] = phrase.Rank.ToString();
                        break;
                    case "Topics":
                        subItems[i] = phrase.Topics.Count.ToString();
                        break;
                    default:
                        subItems[i] = string.Empty;
                        break;
                }
            }
            item.SubItems.AddRange(subItems);

            return item;
        }

        /// <summary>
        /// The GetTopicPhrases
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        /// <returns>The <see cref="Phrases"/></returns>
        private Phrases GetTopicPhrases(Topics topics)
        {
            CollectionKeywordExtractor kwe = new CollectionKeywordExtractor(topics);
            var singleKeywordExtractor = new KeywordExtractor(kwe.StopWordFilter);
            var phrases = new Phrases(kwe.FindPhrases(kwe.GetText()));

            phrases.ForEach(phrase => GetPhraseTopics(phrase, topics, singleKeywordExtractor));

            return phrases;
        }

        /// <summary>
        /// The GetTopicsOf
        /// </summary>
        /// <param name="phrase">The phrase<see cref="Phrase"/></param>
        /// <returns>The <see cref="IEnumerable{Topic}"/></returns>
        private IEnumerable<Topic> GetTopicsOf(Phrase phrase)
        {
            return new Topic[] { new WindowsFormsApp5.Topic() { Title = "Sample", Filepath = @"C:\Users\shytiger\Documents\sample.html" }, new WindowsFormsApp5.Topic() { Title = "Sample", Filepath = @"C:\Users\shytiger\Documents\sample.html" } };
        }

        /// <summary>
        /// The ManageTopics
        /// </summary>
        private void ManageTopics()
        {
            var topicsDialog = new TopicsDialog(Corpus.Topics);
            if (topicsDialog.ShowDialog().Equals(DialogResult.OK))
            {
                Corpus.Topics = topicsDialog.Topics;
            }
        }

        /// <summary>
        /// The optionsToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new OptionsDialog();
            d.ShowDialog();
        }

        /// <summary>
        /// The PopulatePhraseListView
        /// </summary>
        /// <param name="phrases">The phrases<see cref="Phrases"/></param>
        private void PopulatePhraseListView(Phrases phrases)
        {
            PopulatePhraseListView(phrases, string.Empty);
        }

        /// <summary>
        /// The PopulatePhraseListView
        /// </summary>
        /// <param name="phrases">The phrases<see cref="Phrases"/></param>
        /// <param name="filter">The filter<see cref="string"/></param>
        private void PopulatePhraseListView(Phrases phrases, string filter)
        {
            phrasesListView.Items.Clear();

            ListViewItem[] items = phrases.Select(phrase => GetSinglePhraseListViewItem(phrase)).Where(item => ContainsFilter(item, filter)).ToArray();
            phrasesListView.Items.AddRange(items);
        }

        /// <summary>
        /// The toolStripButton1_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ManageTopics();
        }

        /// <summary>
        /// The toolStripButton2_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ExtractPhrases();
        }

        /// <summary>
        /// The toolStripButton3_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ExportPhrases();
        }
    }
}
