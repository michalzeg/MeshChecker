using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncoherentMeshChecker.Model.Elements;
using IncoherentMeshChecker.Model.Geometry;
using Extensions;
using IncoherentMeshChecker.Helpers;
using System.Threading;

namespace IncoherentMeshChecker.Model.Incoherentness
{
    public class IncoherentnessChecker
    {
        private double radious;
        private ICollection<Element> elements;
        private ICollection<Node> nodes;
        private IProgress<ProgressArgument> progressReport;
        private CancellationToken cancellationToken;

        public IncoherentnessChecker(ICollection<Element> elements,ICollection<Node> nodes,double radious, IProgress<ProgressArgument> progressReport, CancellationToken cancellationToken)
        {
            this.progressReport = progressReport;
            this.radious = radious;
            this.elements = elements;
            this.nodes = nodes;
            this.cancellationToken = cancellationToken;
        }

        public ICollection<string> FindIncoherentNodes()
        {
            ICollection<string> incoherentElements = new List<string>();
            int numberOfElements = this.elements.Count;
            int currentElement = 1;
            foreach (Element element in this.elements)
            {

                var nodesInRadious = this.nodes.Where(e =>
                {
                    var distance = new Vector(element.Centre, e.Coordinates).GetLength();
                    return distance <= this.radious;
                });

                foreach(Node nodeToCheck in nodesInRadious)//this.nodes)
                {
                    //check distance between centre of the element and node
                    //double distance = new Vector(element.Centre, nodeToCheck.Coordinates).GetLength();
                    this.cancellationToken.ThrowIfCancellationRequested();
                    //if (distance<=this.radious)
                    //{
                        //node is in proximity of element
                        //check all nodes
                        for (int i = 0; i<=element.Nodes.Count-2 ; i++)
                        {
                            Node elementNode1 = element.Nodes[i];
                            Node elementNode2 = element.Nodes[i + 1];

                            if (NodePositionCheck.CheckNodes(elementNode1,elementNode2,nodeToCheck))
                            {
                                //incoherent element
                                incoherentElements.Add(element.Number.ToString());

                                //adding adjacent elements to incoherent node
                                var adjacentElements = this.elements.Where(e => e.Nodes.Any(n => n == nodeToCheck));
                                var adjacentElementList = adjacentElements.Select(e => e.Number.ToString()).ToList();
                                incoherentElements = incoherentElements.Concat(adjacentElementList).ToList();
                                //incoherentElements.AddRange(adjacentElements);
                            }
                        }

                    //}

                }
                ProgressArgument progressArgument = new ProgressArgument();
                progressArgument.Progress = Convert.ToInt32(Convert.ToDouble(currentElement) / Convert.ToDouble(numberOfElements) * 100);
                progressArgument.Message = element.Number.ToString();
                this.progressReport.Report(progressArgument);
                currentElement++;
            }
            return incoherentElements.Distinct().ToList();
        }

    }

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
    public static class PositionOfNodes
    {
        public static bool NodesLinearity(Node elementNode1,Node elementNode2, Node nodeToCheck)
        {

            //function checks if cross product of vectors elementNode1-elementNode2 and elementNode1-nodeToCheck are equal
            Vector v1 = new Vector(elementNode1.Coordinates, elementNode2.Coordinates);
            Vector v2 = new Vector(elementNode1.Coordinates, nodeToCheck.Coordinates);

            return v1.CrossProduct(v2) == Vector.NullVector;
            
        }
        public static bool CheckNodePosition(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            //check if nodeToCheck lies between elementNode1 and elementNode2
            bool result = false;
            double maxX = Math.Max(elementNode1.Coordinates.X, elementNode2.Coordinates.X);
            double minX = Math.Min(elementNode1.Coordinates.X, elementNode2.Coordinates.X);
            double maxY = Math.Max(elementNode1.Coordinates.Y, elementNode2.Coordinates.Y);
            double minY = Math.Min(elementNode1.Coordinates.Y, elementNode2.Coordinates.Y);
            double maxZ = Math.Max(elementNode1.Coordinates.Z, elementNode2.Coordinates.Z);
            double minZ = Math.Min(elementNode1.Coordinates.Z, elementNode2.Coordinates.Z);
            double X = nodeToCheck.Coordinates.X;
            double Y = nodeToCheck.Coordinates.Y;
            double Z = nodeToCheck.Coordinates.Z;

            if (minX <= X && X <= maxX && minY <= Y && Y <= maxY && minZ <= Z && Z <= maxZ)
            {
                result = true;
            }
            return result;
        }
        
    }

}
