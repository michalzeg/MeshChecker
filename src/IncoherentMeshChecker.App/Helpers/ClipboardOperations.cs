using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace IncoherentMeshChecker.ViewModel
{
    public class ClipboardOperations
    {
        private readonly char[] rowSplitter = { '\n', '\r' };  // Cr and Lf.
        private readonly char columnSplitter = '\t';         // Tab.

        private IEnumerable<string> rowsInClipboard;
        private bool dataExists;

        public ClipboardOperations(IDataObject dataInClipboard)
        {
            try
            {
                string stringInClipboard = dataInClipboard.GetData(DataFormats.Text).ToString();
                this.rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
                this.dataExists = true;
            }
            catch
            {
                this.dataExists = false;
                this.rowsInClipboard = null;
            }
        }

        public IEnumerable<string> AllRows
        {
            get
            {
                return this.rowsInClipboard;
            }
        }

        public IEnumerable<string> ItemsInFirstRow
        {
            get
            {
                if (this.dataExists)
                {
                    string[] valuesInFirstRow = this.rowsInClipboard.First().Split(columnSplitter);
                    return valuesInFirstRow;
                }
                return null;
            }
        }

        public string FirstRow
        {
            get
            {
                if (this.dataExists)
                {
                    string firstRow = this.rowsInClipboard.First();
                    return firstRow;
                }
                return string.Empty;
            }
        }

        public IEnumerable<string> SplitRow(string row)
        {
            return row.Split(columnSplitter);
        }
    }
}