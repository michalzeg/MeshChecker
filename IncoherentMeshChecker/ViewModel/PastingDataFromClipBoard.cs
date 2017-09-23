using IncoherentMeshChecker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace IncoherentMeshChecker.ViewModel
{
    
    public class PasteToDataGridView
    {
        private readonly char[] rowSplitter = { '\n', '\r' };  // Cr and Lf.
        private readonly char columnSplitter = '\t';         // Tab.
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
            int indexOfNode = tableOperations.FindIndex("Node");//rowsInClipboard[0].IndexOf("Element");
            int indexOfX = tableOperations.FindIndex("X");
            int indexOfY = tableOperations.FindIndex("Y");
            int indexOfZ = tableOperations.FindIndex("Z");

            foreach (string row in rowsInclipboard)
            {
                //check if row is first row
                if (row == clipboardOperation.FirstRow)
                    continue;
                // Split up rows to get individual cells:
                string[] valuesInRow = clipboardOperation.SplitRow(row).ToArray();

                // Cycle through cells.
                // Assign cell value only if within columns of grid:
                
                int node;
                double x, y, z;


                if (int.TryParse(valuesInRow[indexOfNode], out node) &&
                    double.TryParse(valuesInRow[indexOfX], out x) &&
                    double.TryParse(valuesInRow[indexOfY], out y) &&
                    double.TryParse(valuesInRow[indexOfZ], out z))
                {
                    //OK
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
            int indexOfElement = tableOperations.FindIndex("Element");//rowsInClipboard[0].IndexOf("Element");
            int indexOfNode1 = tableOperations.FindIndex("Node1");
            int indexOfNode2 = tableOperations.FindIndex("Node2");
            int indexOfNode3 = tableOperations.FindIndex("Node3");
            int indexOfNode4 = tableOperations.FindIndex("Node4");

            //check if columt names exist
            if (indexOfElement == -1 || indexOfNode1 == -1 || indexOfNode2 == -1 || indexOfNode3 == -1 || indexOfNode4 == -1)
                    return false;
            foreach (string row in rowsInclipboard)
            {
                //check if row is first row
                if (row == clipboardOperation.FirstRow)
                    continue;
                // Split up rows to get individual cells:
                string[] valuesInRow = clipboardOperation.SplitRow(row).ToArray();

                // Cycle through cells.
                // Assign cell value only if within columns of grid:
                ElementTable elementTable = new ElementTable();
                int element, node1, node2, node3, node4;


                if (int.TryParse(valuesInRow[indexOfElement], out element) &&
                    int.TryParse(valuesInRow[indexOfNode1], out node1) &&
                    int.TryParse(valuesInRow[indexOfNode2], out node2) &&
                    int.TryParse(valuesInRow[indexOfNode3], out node3) &&
                    int.TryParse(valuesInRow[indexOfNode4], out node4))
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
            } // end while
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
    public class TableValidation
    {
        private readonly string[] nodeTableHeaders = { "Node", "X", "Y", "Z" };
        private readonly string[] elementTableHeaders = { "Element", "Node1", "Node2", "Node3", "Node4" };
        private readonly string nodeValidationPattern = @"\bNode\b.*\bX.*\bY.*\bZ";
        private readonly string elementValidationPattern = @"\bElement\b.*\bNode1\b.*\bNode2\b.*\bNode3\b.*\bNode4\b";

        
        public bool ValidateNodeHeader(IEnumerable<string> headers)
        {
            //checks if node table contains appropriate headers
            bool result = true;
            foreach (string header in nodeTableHeaders)
            {

                if (!headers.Contains(header))
                
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        public bool ValidateNodeHeader(string firstRow)
        {
            Regex regex = new Regex(this.nodeValidationPattern);
            Match match = regex.Match(firstRow);

            return match.Success;
        }
        public bool ValidateElementHeader(IEnumerable<string> headers)
        {
            bool result = true;
            foreach (string header in elementTableHeaders)
            {
                if (!headers.Contains(header))
                {
                    result = false;
                    break;
                }
            }
            return result;
    }
        public bool ValidateElementHeader(string firstRow)
        {
            Regex regex = new Regex(this.elementValidationPattern);
            Match match = regex.Match(firstRow);
            return match.Success;
        }


    }
    

    public class TableOperations
    {
        private IEnumerable<string> table;

        public TableOperations(IEnumerable<string> table)
        {
            this.table = table;
        }

        public int FindIndex(string value)
        {
            int index = 0;
            int length = value.Trim().Length;
            foreach (string item in this.table)
            {
                string itemToCheck = item.Trim().ToLower();
                string valueToCheck = value.ToLower();
                if (itemToCheck.Length < valueToCheck.Length)
                {
                    index++;
                    continue;
                }
                if (itemToCheck.Substring(0,length)==valueToCheck)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }

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
                    string[] valuesInFirstRow = this.rowsInClipboard.ToArray()[0].Split(columnSplitter);
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
                    string firstRow = this.rowsInClipboard.ToArray()[0];
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
