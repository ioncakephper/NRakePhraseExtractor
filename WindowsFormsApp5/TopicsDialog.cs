using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class TopicsDialog : Form
    {
        public TopicsDialog()
        {
            Topics = new Topics();
            InitializeComponent();
        }

        private void PopulateTopicsListView(Topics topics)
        {
            PopulateTopicsListView(topics, string.Empty);
        }

        private void PopulateTopicsListView(Topics topics, string filter)
        {
            topicsListView.Items.Clear();
            topicsListView.Items.AddRange(GetFilteredAllTopicListViewItems(topics, filter));
        }

        private ListViewItem[] GetFilteredAllTopicListViewItems(Topics topics, string filter)
        {
            return GetAllTopicListViewItems(topics).Where(item => ContainsFilter(filter, item)).ToArray();
        }

        private bool ContainsFilter(string filter, ListViewItem item)
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

        private ListViewItem[] GetAllTopicListViewItems(Topics topics)
        {
            return topics.Select(topic => GetSingleTopicListViewItem(topic)).ToArray();
        }

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

        public TopicsDialog(Topics topics) : this()
        {
            Topics = topics;
        }

        public Topics Topics { get; set; }

        private void TopicsDialog_Load(object sender, System.EventArgs e)
        {
            PopulateTopicsListView(Topics);
        }

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

        private void TopicsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Equals(DialogResult.Cancel))
            {
                e.Cancel = changeDetector1.RequestDecision();
            }
        }
    }
}
