using IncoherentMeshChecker.App.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IncoherentMeshChecker.ViewModel
{
    public class PasteToDataGridView
    {
        private readonly char[] rowSplitter = { '\n', '\r' };
        private CancellationToken cancellationToken;

        public List<string> Errors { get; private set; }

        public PasteToDataGridView(CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            this.Errors = new List<string>();
        }

        public bool PasteNodeTable(ref IList<NodeTable> table, IDataObject dataInClipboard)
        {
            bool result = true;
            this.Errors.Clear();
            ClipboardOperations clipboardOperation = new ClipboardOperations(dataInClipboard);
            IEnumerable<string> rowsInclipboard = clipboardOperation.AllRows;
            IEnumerable<string> firstRow = clipboardOperation.ItemsInFirstRow;
            TableOperations tableOperations = new TableOperations(firstRow);
            int indexOfNode = tableOperations.FindIndex("Node");
            int indexOfX = tableOperations.FindIndex("X");
            int indexOfY = tableOperations.FindIndex("Y");
            int indexOfZ = tableOperations.FindIndex("Z");

            foreach (string row in rowsInclipboard)
            {
                if (row == clipboardOperation.FirstRow)
                    continue;

                string[] valuesInRow = clipboardOperation.SplitRow(row).ToArray();

                if (int.TryParse(valuesInRow[indexOfNode], out int node) &&
                    double.TryParse(valuesInRow[indexOfX], out double x) &&
                    double.TryParse(valuesInRow[indexOfY], out double y) &&
                    double.TryParse(valuesInRow[indexOfZ], out double z))
                {
                    NodeTable nodeTable = new NodeTable();
                    nodeTable.Node = node;
                    nodeTable.X = x;
                    nodeTable.Y = y;
                    nodeTable.Z = z;
                    table.Add(nodeTable);
                }
                else
                {
                    this.Errors.Add(string.Format("Node {0} has wrong coordinates", node));
                    result = false;
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
            return result;
        }

        public bool PasteElementTable(ref ICollection<ElementTable> table, IDataObject dataInClipboard)
        {
            bool result = true;
            this.Errors.Clear();
            ClipboardOperations clipboardOperation = new ClipboardOperations(dataInClipboard);

            IEnumerable<string> rowsInclipboard = clipboardOperation.AllRows;

            IEnumerable<string> firstRow = clipboardOperation.ItemsInFirstRow;
            TableOperations tableOperations = new TableOperations(firstRow);
            int indexOfElement = tableOperations.FindIndex("Element");
            int indexOfNode1 = tableOperations.FindIndex("Node1");
            int indexOfNode2 = tableOperations.FindIndex("Node2");
            int indexOfNode3 = tableOperations.FindIndex("Node3");
            int indexOfNode4 = tableOperations.FindIndex("Node4");

            if (indexOfElement == -1 || indexOfNode1 == -1 || indexOfNode2 == -1 || indexOfNode3 == -1 || indexOfNode4 == -1)
                return false;
            foreach (string row in rowsInclipboard)
            {
                if (row == clipboardOperation.FirstRow)
                    continue;

                string[] valuesInRow = clipboardOperation.SplitRow(row).ToArray();

                ElementTable elementTable = new ElementTable();

                if (int.TryParse(valuesInRow[indexOfElement], out int element) &&
                    int.TryParse(valuesInRow[indexOfNode1], out int node1) &&
                    int.TryParse(valuesInRow[indexOfNode2], out int node2) &&
                    int.TryParse(valuesInRow[indexOfNode3], out int node3) &&
                    int.TryParse(valuesInRow[indexOfNode4], out int node4))
                {
                    if (node1 <= 0 || node2 <= 0 || node3 < 0 || node4 < 0)
                    {
                        result = false;
                        this.Errors.Add(string.Format("Element {0} has wrong node numbers", element));
                    }
                    else
                    {
                        elementTable.Element = element;
                        elementTable.Node1 = node1;
                        elementTable.Node2 = node2;
                        elementTable.Node3 = node3;
                        elementTable.Node4 = node4;
                        table.Add(elementTable);
                    }
                }
                else
                {
                    result = false;
                    this.Errors.Add(string.Format("Element {0} has wrong node numbers", element));
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
            return result;
        }

        private string[] splitRows()
        {
            IDataObject dataInClipboard = Clipboard.GetDataObject();
            string stringInClipboard = dataInClipboard.GetData(DataFormats.Text).ToString();
            string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);
            return rowsInClipboard;
        }
    }
}