using IncoherentMeshChecker.Calculations.Nodes;

namespace IncoherentMeshChecker.Calculations.Calculations
{
    /// <summary>
    /// Algorithm
    /// 1. check if nodeToCheck has the same coordinates as elementNode1 and elementNode2
    /// 2. if NO = >check if vector elementNode1 - nodeToCheck and elementNode1 - elementNode2 ae paralell
    /// 3. if Yes=> check if nodeToCheck lyies between elementNode1 and elementNode2
    /// 4 if YES => incoherent node and element
    /// </summary>
    public static class NodePositionCheck
    {
        public static bool CheckNodes(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            if ((elementNode1 == nodeToCheck || elementNode2 == nodeToCheck))
                return false;
            if (!(NodePosition.NodesLinearity(elementNode1, elementNode2, nodeToCheck)))
                return false;
            if (!(NodePosition.CheckNodePosition(elementNode1, elementNode2, nodeToCheck)))
                return false;

            return true;
        }
    }
}