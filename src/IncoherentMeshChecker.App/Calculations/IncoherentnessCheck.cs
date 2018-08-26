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

        public IncoherentnessChecker(ICollection<Element> elements, ICollection<Node> nodes, double radious, IProgress<ProgressArgument> progressReport, CancellationToken cancellationToken)
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

                foreach (Node nodeToCheck in nodesInRadious)//this.nodes)
                {
                    //check distance between centre of the element and node
                    //double distance = new Vector(element.Centre, nodeToCheck.Coordinates).GetLength();
                    this.cancellationToken.ThrowIfCancellationRequested();
                    //if (distance<=this.radious)
                    //{
                    //node is in proximity of element
                    //check all nodes
                    for (int i = 0; i <= element.Nodes.Count - 2; i++)
                    {
                        Node elementNode1 = element.Nodes[i];
                        Node elementNode2 = element.Nodes[i + 1];

                        if (NodePositionCheck.CheckNodes(elementNode1, elementNode2, nodeToCheck))
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
}