using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IncoherentMeshChecker.ViewModel
{
    public class TableValidation
    {
        private readonly string[] nodeTableHeaders = { "Node", "X", "Y", "Z" };
        private readonly string[] elementTableHeaders = { "Element", "Node1", "Node2", "Node3", "Node4" };
        private readonly string nodeValidationPattern = @"\bNode\b.*\bX.*\bY.*\bZ";
        private readonly string elementValidationPattern = @"\bElement\b.*\bNode1\b.*\bNode2\b.*\bNode3\b.*\bNode4\b";

        public bool ValidateNodeHeader(IEnumerable<string> headers)
        {
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
}