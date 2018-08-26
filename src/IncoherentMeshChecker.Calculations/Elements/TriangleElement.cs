using IncoherentMeshChecker.Calculations.Nodes;
using IncoherentMeshChecker.Shared.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace IncoherentMeshChecker.Calculations.Elements
{
    public class TriangleElement : Element
    {
        public TriangleElement(int number, IList<Node> nodes)
        {
            base.numberOfNodes = 3;
            base.CheckNumberOfNodesInArray(nodes);
            base.CheckIfNodesAreNull(nodes);
            base.number = number;
            this.nodes = nodes;
            this.CalculateCentre();
        }

        protected override void CalculateCentre()
        {
            PointD centre = new PointD();
            centre.X = Nodes.Sum(m => m.Coordinates.X) / 3;
            centre.Y = Nodes.Sum(m => m.Coordinates.Y) / 3;
            centre.Z = Nodes.Sum(m => m.Coordinates.Z) / 3;
            this.centre = centre;
        }
    }
}