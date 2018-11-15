using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class SelectedTopicsDialog : Form
    {
        public Topics Topics { get; set; }

        public SelectedTopicsDialog()
        {
            InitializeComponent();

        }

        public SelectedTopicsDialog(Topics topics) : this()
        {
            Topics = topics;
        }

        private void SelectedTopicsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Equals(DialogResult.Cancel))
            {
                e.Cancel = changeDetector1.RequestDecision(); 
            }
        }

        private void SelectedTopicsDialog_Load(object sender, EventArgs e)
        {
            PopulateTopicsListView(Topics);
        }

        private void PopulateTopicsListView(Topics topics)
        {
            PopulateTopicsListView(topics, string.Empty);
        }

        private void PopulateTopicsListView(Topics topics, string filter)
        {
            topicListView.Items.Clear();
            topicListView.Items.AddRange(GetFilteredAllTopicsListViewItems(topics, filter));
        }

        private ListViewItem[] GetFilteredAllTopicsListViewItems(Topics topics, string filter)
        {
            return GetAllTopicsListViewItems(topics).Where(item => ContainsFilter(item, filter)).ToArray();
        }

        private ListViewItem[] GetAllTopicsListViewItems(Topics topics)
        {
            return topics.Select(topic => GetSingleTopicListViewItem(topic)).ToArray();
        }

        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            var item = new ListViewItem(topic.Title);
            item.Tag = topic;
            item.Checked = true;
            var subItems = new string[topicListView.Columns.Count - 1];
            for (int i = 0; i < subItems.Length; i++)
            {
                switch (topicListView.Columns[i+1].Text)
                {
                    case "Filename":
                        subItems[i] = topic.Filename;
                        break;
                    case "Folder":
                        subItems[i] = topic.Folder;
                        break;
                    case "Path":
                        subItems[i] = topic.Filepath;
                        break;
                    default:
                        subItems[i] = string.Empty;
                        break;
                }
            }
            item.SubItems.AddRange(subItems);

            return item;
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

        private void button4_Click(object sender, EventArgs e)
        {
            var d = new TopicsDialog(Topics);
            if (d.ShowDialog().Equals(DialogResult.OK))
            {
                Topics = d.Topics;
                PopulateTopicsListView(Topics);
                changeDetector1.Changed = true;
            }
        }
    }
}
