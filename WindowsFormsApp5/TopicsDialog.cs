//------------------------------------------------------------------------------
// <copyright file="TopicsDialog.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="TopicsDialog" />
    /// </summary>
    public partial class TopicsDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopicsDialog"/> class.
        /// </summary>
        public TopicsDialog()
        {
            Topics = new Topics();
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicsDialog"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        public TopicsDialog(Topics topics) : this()
        {
            Topics = topics;
        }

        /// <summary>
        /// Gets or sets the Topics
        /// </summary>
        public Topics Topics { get; set; }

        /// <summary>
        /// The button1_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                var fileNames = openFileDialog1.FileNames;
                Topics.AddRange(fileNames.Select(filePath => new Topic(filePath)).ToArray());
                PopulateTopicsListView(Topics);
                changeDetector1.Changed = true;
            }
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
        /// The GetSingleTopicListViewItem
        /// </summary>
        /// <param name="topic">The topic<see cref="Topic"/></param>
        /// <returns>The <see cref="ListViewItem"/></returns>
        private ListViewItem GetSingleTopicListViewItem(Topic topic)
        {
            ListViewItem item = new ListViewItem(topic.Title);
            item.Tag = topic;
            var subItems = new string[topicsListView.Columns.Count - 1];
            for (int i = 0; i < subItems.Length; i++)
            {
                switch (topicsListView.Columns[i + 1].Text)
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

        /// <summary>
        /// The PopulateTopicsListView
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        private void PopulateTopicsListView(Topics topics)
        {
            PopulateTopicsListView(topics, string.Empty);
        }

        /// <summary>
        /// The PopulateTopicsListView
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        /// <param name="filter">The filter<see cref="string"/></param>
        private void PopulateTopicsListView(Topics topics, string filter)
        {
            topicsListView.Items.Clear();

            ListViewItem[] items = topics.Select(topic => GetSingleTopicListViewItem(topic)).Where(item => ContainsFilter(item, filter)).ToArray();
            topicsListView.Items.AddRange(items);
        }

        /// <summary>
        /// The TopicsDialog_FormClosing
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/></param>
        private void TopicsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Equals(DialogResult.Cancel))
            {
                e.Cancel = changeDetector1.RequestDecision();
            }
        }

        /// <summary>
        /// The TopicsDialog_Load
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private void TopicsDialog_Load(object sender, System.EventArgs e)
        {
            PopulateTopicsListView(Topics);
        }
    }
}
