using System.Collections.Generic;
using System.Linq;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Elements
{
    public class TriangleElement : Element
    {
        //public int Number { get; private set; }
        //public IList<Node> Nodes { get; private set; }
        //public PointD Centre { get; private set; }

        public TriangleElement(int number, IList<Node> nodes)
        {
            base.numberOfNodes = 3;
            base.checkNumberOfNodesInArray(nodes);
            base.checkIfNodesAreNull(nodes);
            base.number = number;
            this.nodes = nodes;
            this.calculateCentre();
        }

        protected override void calculateCentre()
        {
            //centre is calculated as mean value of coordinates divided by 3
            PointD centre = new PointD();
            centre.X = Nodes.Sum(m => m.Coordinates.X) / 3;
            centre.Y = Nodes.Sum(m => m.Coordinates.Y) / 3;
            centre.Z = Nodes.Sum(m => m.Coordinates.Z) / 3;
            this.centre = centre;
        }
    }
}