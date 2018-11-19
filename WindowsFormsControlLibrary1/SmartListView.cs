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
        /// Defines the listColumnSorter
        /// </summary>
        private ListViewComparer listColumnSorter;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartListView"/> class.
        /// </summary>
        public SmartListView()
        {
            InitializeComponent();
            this.listColumnSorter = new ListViewComparer(0, SortOrder.None);
            this.ListViewItemSorter = listColumnSorter;
            this.SetSortIcon(listColumnSorter.SortColumn, listColumnSorter.Order);
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

                        // capture first time columns are painted,
                        // and set first column as sorting column
                        // Order is None only on the first run of this method.
                        if (listColumnSorter.Order == SortOrder.None)
                        {
                            listColumnSorter.SortColumn = 0;
                            listColumnSorter.Order = SortOrder.Ascending;
                            this.SetSortIcon(listColumnSorter.SortColumn, listColumnSorter.Order);
                        }
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
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == listColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (listColumnSorter.Order == SortOrder.Ascending)
                {
                    listColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    listColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                listColumnSorter.SortColumn = e.Column;
                listColumnSorter.Order = SortOrder.Ascending;
            }

            Sort();
            this.SetSortIcon(listColumnSorter.SortColumn, listColumnSorter.Order);
        }
    }
}
