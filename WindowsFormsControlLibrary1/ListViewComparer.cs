//------------------------------------------------------------------------------------
// <copyright file="ListViewComparer.cs" company="Ion Gireada">
//    Copyright (c) 2018 Ion Gireada
// </copyright>
//------------------------------------------------------------------------------------

namespace WindowsFormsControlLibrary1
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="ListViewComparer" />
    /// </summary>
    internal class ListViewComparer : IComparer
    {
        /// <summary>
        /// Defines the columnNumber
        /// </summary>
        private int columnNumber;

        /// <summary>
        /// Gets or sets the SortColumn
        /// </summary>
        public int SortColumn
        {
            get { return columnNumber; }
            set { columnNumber = value; }
        }

        /// <summary>
        /// Defines the sortOrder
        /// </summary>
        private SortOrder sortOrder;

        /// <summary>
        /// Gets or sets the Order
        /// </summary>
        public SortOrder Order
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        /// <summary>
        /// Defines the listViewItemComparer
        /// </summary>
        private Comparer listViewItemComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewComparer"/> class.
        /// </summary>
        /// <param name="column">The column<see cref="int"/></param>
        /// <param name="sortOrder">The sortOrder<see cref="SortOrder"/></param>
        public ListViewComparer(int column, SortOrder sortOrder)
        {
            this.columnNumber = column;
            SortColumn = columnNumber;
            this.sortOrder = sortOrder;
            Order = this.sortOrder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewComparer"/> class.
        /// </summary>
        public ListViewComparer()
        {
            SortColumn = 0;
            Order = SortOrder.None;
        }

        /// <summary>
        /// The Compare
        /// </summary>
        /// <param name="x">The x<see cref="object"/></param>
        /// <param name="y">The y<see cref="object"/></param>
        /// <returns>The <see cref="int"/></returns>
        public int Compare(object x, object y)
        {
            // Get the objects as ListViewItems.
            ListViewItem item_x = x as ListViewItem;
            ListViewItem item_y = y as ListViewItem;

            // Get the corresponding sub-item values.
            string string_x;
            if (item_x.SubItems.Count <= columnNumber)
            {
                string_x = "";
            }
            else
            {
                string_x = item_x.SubItems[columnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= columnNumber)
            {
                string_y = "";
            }
            else
            {
                string_y = item_y.SubItems[columnNumber].Text;
            }

            // Compare them.
            int result;
            double double_x, double_y;
            if (double.TryParse(string_x, out double_x) &&
                double.TryParse(string_y, out double_y))
            {
                // Treat as a number.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                DateTime date_x, date_y;
                if (DateTime.TryParse(string_x, out date_x) &&
                    DateTime.TryParse(string_y, out date_y))
                {
                    // Treat as a date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    // Treat as a string.
                    result = string_x.CompareTo(string_y);
                }
            }

            // Return the correct result depending on whether
            // we're sorting ascending or descending.
            if (sortOrder == SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }
}
