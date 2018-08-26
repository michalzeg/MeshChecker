using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using IncoherentMeshChecker.Calculations.Elements;
using IncoherentMeshChecker.Shared.Helpers;
using IncoherentMeshChecker.Calculations.Nodes;
using IncoherentMeshChecker.Shared.Geometry;

namespace IncoherentMeshChecker.Calculations.Calculations
{
    public class IncoherentnessChecker
    {
        private readonly double radious;
        private readonly ICollection<Element> elements;
        private readonly ICollection<Node> nodes;
        private readonly IProgress<ProgressArgument> progressReport;
        private readonly CancellationToken cancellationToken;

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
            var incoherentElements = new List<string>();
            int numberOfElements = this.elements.Count;
            int currentElement = 1;
            foreach (Element element in this.elements)
            {
                var nodesInRadious = this.nodes.Where(e => new Vector(element.Centre, e.Coordinates).GetLength() < this.radious);

                foreach (Node nodeToCheck in nodesInRadious)
                {
                    CheckNode(incoherentElements, element, nodeToCheck);
                }

                ReportProgress(numberOfElements, currentElement, element);
                currentElement++;
            }
            return incoherentElements.Distinct().ToList();
        }

        private void CheckNode(List<string> incoherentElements, Element element, Node nodeToCheck)
        {
            this.cancellationToken.ThrowIfCancellationRequested();

            for (int i = 0; i <= element.Nodes.Count - 2; i++)
            {
                var elementNode1 = element.Nodes[i];
                var elementNode2 = element.Nodes[i + 1];

                if (NodePositionCheck.CheckNodes(elementNode1, elementNode2, nodeToCheck))
                {
                    incoherentElements.Add(element.Number.ToString());

                    var adjacentElements = this.elements
                        .Where(e => e.Nodes.Any(n => n == nodeToCheck))
                        .Select(e => e.Number.ToString()).ToList();

                    incoherentElements.AddRange(adjacentElements);
                }
            }
        }

        private void ReportProgress(int numberOfElements, int currentElement, Element element)
        {
            ProgressArgument progressArgument = new ProgressArgument
            {
                Progress = Convert.ToInt32(Convert.ToDouble(currentElement) / Convert.ToDouble(numberOfElements) * 100),
                Message = element.Number.ToString()
            };
            this.progressReport.Report(progressArgument);
        }
    }
}