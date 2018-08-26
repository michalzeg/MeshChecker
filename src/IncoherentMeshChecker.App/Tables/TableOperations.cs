using System.Collections.Generic;

namespace IncoherentMeshChecker.ViewModel
{
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
                if (itemToCheck.Substring(0, length) == valueToCheck)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}