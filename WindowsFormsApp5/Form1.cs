//------------------------------------------------------------------------------
// <copyright file="form1.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using System;
    using System.Linq;
    using System.Text;
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



            string s = string.Format("<OBJECT type=\"text/sitemap\">\r\n<param name=\"Name\" value=\"{0}\">", phrase.Text);

            StringBuilder sb = new StringBuilder();
            sb.Append(doc.CreateElement("NEWOBJECT").ToString()).Append("OBJECT type=\"text/sitemap\"").Append('>');
            sb.AppendLine();
            sb.Append(string.Format("<param name=\"Name\" value=\"{0}\">", phrase.Text));
            sb.AppendLine();

            foreach (Topic topic in phrase.Topics)
            {
                sb.AppendFormat("<param name=\"Name\" value=\"{0}\">", topic.Title);
                sb.AppendLine();

                sb.AppendFormat("<param name=\"Local\" value=\"{0}\">", topic.Filepath);
                sb.AppendLine();

            }

            sb.Append("</OBJECT>"); sb.AppendLine();

            li.InnerText = s;
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

                string template = @"<!DOCTYPE HTML PUBLIC "" -//IETF//DTD HTML//EN"">
<HTML>
< HEAD>
    <meta name=""Generator"" value=""Microsoft HTML Help Workshop 1.4"">
    <!-- Sitemap 1.0 --> 
</HEAD><BODY>
<UL>
{0}
</UL>
</BODY></HTML>";

                StringBuilder sb = new StringBuilder();
                foreach (var phrase in Corpus.Phrases)
                {
                    StringBuilder topicsBuilder = new StringBuilder();

                    foreach (var topic in phrase.Topics)
                    {
                        topicsBuilder.AppendFormat(@"       <param name=""Name"" value=""{0}"">
                <param name=""Local"" value=""{1}"">
                <param name=""URL"" value=""{1}"">
", topic.Title, topic.Filename);
                    }

                    sb.AppendFormat(@"  <LI><OBJECT type=""text/sitemap"">
        <param name=""Name"" value=""{0}"">
{1}
        </OBJECT>
", phrase.Text, topicsBuilder.ToString());
                }

                System.IO.File.WriteAllText(fileName, string.Format(template, sb.ToString()));
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
            var phrases = new Phrases();
            CollectionKeywordExtractor kwe = new CollectionKeywordExtractor(topics);
            phrases.AddRange(kwe.FindAllKeyPhrases());
            return phrases;
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
