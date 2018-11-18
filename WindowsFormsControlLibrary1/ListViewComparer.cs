using System;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
    internal class ListViewComparer : IComparer
    {
        private int columnNumber;
        private SortOrder sortOrder;

        public ListViewComparer(int column, SortOrder sortOrder)
        {
            this.columnNumber = column;
            this.sortOrder = sortOrder;
        }

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