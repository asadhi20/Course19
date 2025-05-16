using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Extensions
{
    public static class DataGridViewExtensions
    {
        public static bool ResizeCloumnsWight(this DataGridView dgv, params (string colName, int widthSize)[] columnsRESizeWidthInfo)
        {
            try {
                foreach ((string colName, int widthSize) item in columnsRESizeWidthInfo) {
                    dgv.Columns[item.colName].Width = item.widthSize;
                }
                return true;
            }
            catch { return false; }
        }

        public static bool SortingColumns(this DataGridView dgv, params (string colName, ListSortDirection listSortDirection)[] columnsSortingInfo)
        {
            try {
                foreach ((string colName, ListSortDirection listSortDirection) item in columnsSortingInfo) {
                    dgv.Sort(dgv.Columns[item.colName], item.listSortDirection);
                }
                return true;
            }
            catch { return false; }
        }
    }
}
