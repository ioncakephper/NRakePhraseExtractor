//------------------------------------------------------------------------------
// <copyright file="ListViewExtensions.cs" company="Ion Gireada">
//      Copyright (c) Ion Gireada. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace WindowsFormsControlLibrary1
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="ListViewExtensions" />
    /// </summary>
    internal static class ListViewExtensions
    {
        /// <summary>
        /// Defines the HDF_BITMAP
        /// </summary>
        internal const Int32 HDF_BITMAP = 0x2000;

        /// <summary>
        /// Defines the HDF_BITMAP_ON_RIGHT
        /// </summary>
        internal const Int32 HDF_BITMAP_ON_RIGHT = 0x1000;

        /// <summary>
        /// Defines the HDF_CENTER
        /// </summary>
        internal const Int32 HDF_CENTER = 0x0002;

        /// <summary>
        /// Defines the HDF_IMAGE
        /// </summary>
        internal const Int32 HDF_IMAGE = 0x0800;

        /// <summary>
        /// Defines the HDF_JUSTIFYMASK
        /// </summary>
        internal const Int32 HDF_JUSTIFYMASK = 0x0003;

        /// <summary>
        /// Defines the HDF_LEFT
        /// </summary>
        internal const Int32 HDF_LEFT = 0x0000;

        /// <summary>
        /// Defines the HDF_OWNERDRAW
        /// </summary>
        internal const Int32 HDF_OWNERDRAW = 0x8000;

        /// <summary>
        /// Defines the HDF_RIGHT
        /// </summary>
        internal const Int32 HDF_RIGHT = 0x0001;

        /// <summary>
        /// Defines the HDF_RTLREADING
        /// </summary>
        internal const Int32 HDF_RTLREADING = 0x0004;

        /// <summary>
        /// Defines the HDF_SORTDOWN
        /// </summary>
        internal const Int32 HDF_SORTDOWN = 0x0200;

        /// <summary>
        /// Defines the HDF_SORTUP
        /// </summary>
        internal const Int32 HDF_SORTUP = 0x0400;

        /// <summary>
        /// Defines the HDF_STRING
        /// </summary>
        internal const Int32 HDF_STRING = 0x4000;

        /// <summary>
        /// Defines the HDI_BITMAP
        /// </summary>
        internal const Int32 HDI_BITMAP = 0x0010;

        /// <summary>
        /// Defines the HDI_DI_SETITEM
        /// </summary>
        internal const Int32 HDI_DI_SETITEM = 0x0040;

        /// <summary>
        /// Defines the HDI_FILTER
        /// </summary>
        internal const Int32 HDI_FILTER = 0x0100;

        /// <summary>
        /// Defines the HDI_FORMAT
        /// </summary>
        internal const Int32 HDI_FORMAT = 0x0004;

        /// <summary>
        /// Defines the HDI_HEIGHT
        /// </summary>
        internal const Int32 HDI_HEIGHT = HDI_WIDTH;

        /// <summary>
        /// Defines the HDI_IMAGE
        /// </summary>
        internal const Int32 HDI_IMAGE = 0x0020;

        /// <summary>
        /// Defines the HDI_LPARAM
        /// </summary>
        internal const Int32 HDI_LPARAM = 0x0008;

        /// <summary>
        /// Defines the HDI_ORDER
        /// </summary>
        internal const Int32 HDI_ORDER = 0x0080;

        /// <summary>
        /// Defines the HDI_TEXT
        /// </summary>
        internal const Int32 HDI_TEXT = 0x0002;

        /// <summary>
        /// Defines the HDI_WIDTH
        /// </summary>
        internal const Int32 HDI_WIDTH = 0x0001;

        /// <summary>
        /// Defines the HDM_FIRST
        /// </summary>
        internal const Int32 HDM_FIRST = 0x1200;

        /// <summary>
        /// Defines the HDM_GETIMAGELIST
        /// </summary>
        internal const Int32 HDM_GETIMAGELIST = HDM_FIRST + 9;

        /// <summary>
        /// Defines the HDM_GETITEM
        /// </summary>
        internal const Int32 HDM_GETITEM = HDM_FIRST + 11;

        /// <summary>
        /// Defines the HDM_SETIMAGELIST
        /// </summary>
        internal const Int32 HDM_SETIMAGELIST = HDM_FIRST + 8;

        /// <summary>
        /// Defines the HDM_SETITEM
        /// </summary>
        internal const Int32 HDM_SETITEM = HDM_FIRST + 12;

        /// <summary>
        /// Defines the LVM_FIRST
        /// </summary>
        internal const Int32 LVM_FIRST = 0x1000;

        /// <summary>
        /// Defines the LVM_GETHEADER
        /// </summary>
        internal const Int32 LVM_GETHEADER = LVM_FIRST + 31;

        /// <summary>
        /// The SetSortIcon
        /// </summary>
        /// <param name="listView">The listView<see cref="ListView"/></param>
        /// <param name="columnIndex">The columnIndex<see cref="int"/></param>
        /// <param name="order">The order<see cref="SortOrder"/></param>
        public static void SetSortIcon(this ListView listView, int columnIndex, SortOrder order)
        {
            IntPtr columnHeader = SendMessage(listView.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

            for (int columnNumber = 0; columnNumber <= listView.Columns.Count - 1; columnNumber++)
            {
                IntPtr columnPtr = new IntPtr(columnNumber);
                Lvcolumn lvColumn = new Lvcolumn();
                lvColumn.mask = HDI_FORMAT;

                SendMessageLVCOLUMN(columnHeader, HDM_GETITEM, columnPtr, ref lvColumn);

                if ((order != SortOrder.None) && columnNumber == columnIndex)
                {
                    switch (order)
                    {
                        case System.Windows.Forms.SortOrder.Ascending:
                            lvColumn.fmt &= ~HDF_SORTDOWN;
                            lvColumn.fmt |= HDF_SORTUP;
                            break;
                        case System.Windows.Forms.SortOrder.Descending:
                            lvColumn.fmt &= ~HDF_SORTUP;
                            lvColumn.fmt |= HDF_SORTDOWN;
                            break;
                    }
                    lvColumn.fmt |= (HDF_LEFT | HDF_BITMAP_ON_RIGHT);
                }
                else
                {
                    lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP & ~HDF_BITMAP_ON_RIGHT;
                }

                SendMessageLVCOLUMN(columnHeader, HDM_SETITEM, columnPtr, ref lvColumn);
            }
        }

        /// <summary>
        /// The SendMessage
        /// </summary>
        /// <param name="hWnd">The hWnd<see cref="IntPtr"/></param>
        /// <param name="Msg">The Msg<see cref="uint"/></param>
        /// <param name="wParam">The wParam<see cref="IntPtr"/></param>
        /// <param name="lParam">The lParam<see cref="IntPtr"/></param>
        /// <returns>The <see cref="IntPtr"/></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The SendMessageLVCOLUMN
        /// </summary>
        /// <param name="hWnd">The hWnd<see cref="IntPtr"/></param>
        /// <param name="Msg">The Msg<see cref="Int32"/></param>
        /// <param name="wParam">The wParam<see cref="IntPtr"/></param>
        /// <param name="lPLVCOLUMN">The lPLVCOLUMN<see cref="Lvcolumn"/></param>
        /// <returns>The <see cref="IntPtr"/></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref Lvcolumn lPLVCOLUMN);

        /// <summary>
        /// Defines the <see cref="Lvcolumn" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Lvcolumn
        {
            /// <summary>
            /// Defines the cchTextMax
            /// </summary>
            public Int32 cchTextMax;

            /// <summary>
            /// Defines the cx
            /// </summary>
            public Int32 cx;

            /// <summary>
            /// Defines the fmt
            /// </summary>
            public Int32 fmt;

            /// <summary>
            /// Defines the hbm
            /// </summary>
            public IntPtr hbm;

            /// <summary>
            /// Defines the iImage
            /// </summary>
            public Int32 iImage;

            /// <summary>
            /// Defines the iOrder
            /// </summary>
            public Int32 iOrder;

            /// <summary>
            /// Defines the iSubItem
            /// </summary>
            public Int32 iSubItem;

            /// <summary>
            /// Defines the mask
            /// </summary>
            public Int32 mask;

            /// <summary>
            /// Defines the pszText
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
        }
    }
}
