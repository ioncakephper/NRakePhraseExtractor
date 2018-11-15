using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            Corpus = new Corpus();
            InitializeComponent();
        }

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

        private ListViewItem[] GetAllPhraseListViewItems(Phrases phrases)
        {
            return phrases.Select(phrase => GetSinglePhraseListViewItem(phrase)).ToArray();
        }

        private ListViewItem GetSinglePhraseListViewItem(Phrase phrase)
        {
            ListViewItem item = new ListViewItem(phrase.Text);
            item.Tag = phrase;
            var subItems = new string[phrasesListView.Columns.Count - 1];
            for (int i = 0; i < subItems.Length; i++)
            {
                switch (phrasesListView.Columns[i + 1].Text)
                {
                    default:
                        subItems[i] = string.Empty;
                        break;
                }
            }
            item.SubItems.AddRange(subItems);

            return item;
        }

        public Form1(Corpus corpus) : this()
        {
            Corpus = corpus;
        }

        public Corpus Corpus
        {
            get; set;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ManageTopics();
        }

        private void ManageTopics()
        {
            var topicsDialog = new TopicsDialog(Corpus.Topics);
            if (topicsDialog.ShowDialog().Equals(DialogResult.OK))
            {
                Corpus.Topics = topicsDialog.Topics;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = changeDetector1.RequestDecision();
        }
    }
}
