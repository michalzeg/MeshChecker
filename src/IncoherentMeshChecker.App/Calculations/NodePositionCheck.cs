using IncoherentMeshChecker.Model.Elements;

namespace IncoherentMeshChecker.Model.Incoherentness
{
    public static class NodePositionCheck
    {
        //class checks if given node is "incoherent" node, it means if it lyies between two other nodes

        public static bool CheckNodes(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            //algorithm
            //1. check if nodeToCheck has the same coordinates as elementNode1 and elementNode2
            //2. if NO = >check if vector elementNode1 - nodeToCheck and elementNode1 - elementNode2 ae paralell
            //3. if Yes=> check if nodeToCheck lyies between elementNode1 and elementNode2
            //4 if YES => incoherent node and element

            if ((elementNode1 == nodeToCheck || elementNode2 == nodeToCheck))
                return false;
            if (!(PositionOfNodes.NodesLinearity(elementNode1, elementNode2, nodeToCheck)))
                return false;
            if (!(PositionOfNodes.CheckNodePosition(elementNode1, elementNode2, nodeToCheck)))
                return false;
            //node is incoherent node
            return true;
        }




    }

}
