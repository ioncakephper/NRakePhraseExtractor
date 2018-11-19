//------------------------------------------------------------------------------
// <copyright file="topicsdialog.cs" company="Ion Gireada">
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
        /// The AddRelativePath
        /// </summary>
        /// <param name="tocPath">The tocPath<see cref="string"/></param>
        /// <param name="tocFile">The tocFile<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        private string AddRelativePath(string tocPath, string tocFile)
        {
            // TODO: Get relative path from tocPath to tocFile. Make sure NO trailing \
            string relativePath = tocPath;
            return string.Format(@"{0}\{1}", relativePath, tocFile);
        }

        /// <summary>
        /// The AddTopicFiles
        /// </summary>
        /// <param name="fileNames">The fileNames<see cref="string[]"/></param>
        private void AddTopicFiles(string[] fileNames)
        {
            Topics.AddRange(fileNames.Select(filePath => new Topic(filePath)).ToArray());
            PopulateTopicsListView(Topics);
            changeDetector1.Changed = true;
        }

        /// <summary>
        /// The AddTopicsFromToc
        /// </summary>
        /// <param name="tocFileName">The tocFileName<see cref="string"/></param>
        private void AddTopicsFromToc(string tocFileName)
        {
            string[] topicFileNames = GetTopicFileNames(tocFileName);
            AddTopicFiles(topicFileNames);
        }

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
                AddTopicFiles(fileNames);
            }
        }

        /// <summary>
        /// The button2_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            openFileDialog2.FileName = string.Empty;
            if (openFileDialog2.ShowDialog().Equals(DialogResult.OK))
            {
                AddTopicsFromToc(openFileDialog2.FileName);
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
        /// The GetTopicFileNames
        /// </summary>
        /// <param name="tocFileName">The tocFileName<see cref="string"/></param>
        /// <returns>The <see cref="string[]"/></returns>
        private string[] GetTopicFileNames(string tocFileName)
        {
            if (!System.IO.File.Exists(tocFileName))
            {
                throw new InvalidFilenameException(tocFileName);
            }
            string tocPath = System.IO.Path.GetDirectoryName(tocFileName);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(tocFileName);

            string[] topicFileNames = new string[] { };

            var paramNodes = doc.DocumentNode.SelectNodes("//param").ToArray();
            if (paramNodes != null)
            {
                string[] tocfiles = paramNodes.Where(p => p.Attributes["name"].Value.Equals("Local")).Select(p => p.Attributes["value"].Value).ToArray();
                topicFileNames = tocfiles.Select(tocFile => AddRelativePath(tocPath, tocFile)).ToArray();
            }

            return topicFileNames;
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
            SetFormControlsEnabledStatus(topicsListView.Items.Count.Equals(0));
        }

        /// <summary>
        /// The SetFormControlsEnabledStatus
        /// </summary>
        /// <param name="isListViewEmpty">The isListViewEmpty<see cref="bool"/></param>
        private void SetFormControlsEnabledStatus(bool isListViewEmpty)
        {
            var status = !(isListViewEmpty);
            var controls = new Control[] { removeButton };
            controls.ToList().ForEach(b => b.Enabled = status);
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

        /// <summary>
        /// The button3_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="System.EventArgs"/></param>
        private void button3_Click(object sender, System.EventArgs e)
        {
            if (topicsListView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in topicsListView.SelectedItems)
                {
                    int index = Topics.IndexOf((Topic)item.Tag);
                    Topics.RemoveAt(index);
                }
                PopulateTopicsListView(Topics);
            }
        }
    }
}
