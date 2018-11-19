//------------------------------------------------------------------------------
// <copyright file="smartlistview.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsControlLibrary1
{
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="SmartListView" />
    /// </summary>
    public partial class SmartListView : ListView
    {
        /// <summary>
        /// Defines the sortingColumn
        /// </summary>
        private ColumnHeader sortingColumn = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartListView"/> class.
        /// </summary>
        public SmartListView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The WndProc
        /// </summary>
        /// <param name="m">The m<see cref="Message"/></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0xf;
            // if the control is in details view mode and columns
            // have been added, then intercept the WM_PAINT message
            // and reset the last column width to fill the list view
            switch (m.Msg)
            {
                case WM_PAINT:
                    if (this.View == View.Details && this.Columns.Count > 0)
                    {
                        this.Columns[this.Columns.Count - 1].Width = -2;
                    }
                    break;
            }

            // pass messages on to the base control for processing
            base.WndProc(ref m);
        }

        /// <summary>
        /// The SmartListView_ColumnClick
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="ColumnClickEventArgs"/></param>
        private void SmartListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnHeader newSortingColumn = this.Columns[e.Column];
            System.Windows.Forms.SortOrder sortOrder;

            if (sortingColumn == null)
            {
                sortOrder = SortOrder.Ascending;
            }
            else
            {
                if (newSortingColumn == sortingColumn)
                {
                    sortOrder = (sortingColumn.Text.StartsWith("> ")) ? SortOrder.Descending : SortOrder.Ascending;
                }
                else
                {
                    sortOrder = SortOrder.Ascending;
                }

                sortingColumn.Text = sortingColumn.Text.Substring(2);
            }

            sortingColumn = newSortingColumn;
            if (sortOrder == SortOrder.Ascending)
            {
                sortingColumn.Text = "> " + sortingColumn.Text;
            }
            else
            {
                sortingColumn.Text = "< " + sortingColumn.Text;
            }

            this.ListViewItemSorter = new ListViewComparer(e.Column, sortOrder);
            Sort();
        }
    }
}
