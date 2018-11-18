﻿//------------------------------------------------------------------------------
// <copyright file="selectedtopicsdialog.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsApp5
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="SelectedTopicsDialog" />
    /// </summary>
    public partial class SelectedTopicsDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedTopicsDialog"/> class.
        /// </summary>
        public SelectedTopicsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedTopicsDialog"/> class.
        /// </summary>
        /// <param name="topics">The topics<see cref="Topics"/></param>
        public SelectedTopicsDialog(Topics topics) : this()
        {
            Topics = topics;
        }

        /// <summary>
        /// Gets or sets the Topics
        /// </summary>
        public Topics Topics { get; set; } = new Topics();

        /// <summary>
        /// The button4_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
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
            var item = new ListViewItem(topic.Title);
            item.Tag = topic;
            item.Checked = true;
            var subItems = new string[topicListView.Columns.Count - 1];
            for (int i = 0; i < subItems.Length; i++)
            {
                switch (topicListView.Columns[i + 1].Text)
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
            topicListView.Items.Clear();

            ListViewItem[] items = topics.Select(topic => GetSingleTopicListViewItem(topic)).Where(item => ContainsFilter(item, filter)).ToArray();
            topicListView.Items.AddRange(items);
        }

        /// <summary>
        /// The SelectedTopicsDialog_FormClosing
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/></param>
        private void SelectedTopicsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Equals(DialogResult.Cancel))
            {
                e.Cancel = changeDetector1.RequestDecision();
            }
        }

        /// <summary>
        /// The SelectedTopicsDialog_Load
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void SelectedTopicsDialog_Load(object sender, EventArgs e)
        {
            PopulateTopicsListView(Topics);
        }
    }
}
