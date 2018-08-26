using System.Collections.Generic;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Elements
{
    public class QuadElement : Element
    {
        public QuadElement(int number, IList<Node> nodes)
        {
            base.numberOfNodes = 4;
            base.checkNumberOfNodesInArray(nodes);
            base.checkIfNodesAreNull(nodes);
            base.number = number;
            this.nodes = nodes;
            this.calculateCentre();
        }

        protected override void calculateCentre()
        {
            //centre is calculates as the middle of the diagonal between
            //points 0 and 2
            Node node0 = this.Nodes[0];
            Node node2 = this.Nodes[2];

            //calculate vector between node 0 and node2
            Vector v = new Vector(node0.Coordinates, node2.Coordinates);
            //calculate vector v2 = 0.5*v
            Vector v2 = 0.5 * v;
            //translating point node0 by vector v2
            PointD centre = new PointD();
            centre.X = node0.Coordinates.X + v2.X;
            centre.Y = node0.Coordinates.Y + v2.Y;
            centre.Z = node0.Coordinates.Z + v2.Z;

            this.centre = centre;
        }
    }
}