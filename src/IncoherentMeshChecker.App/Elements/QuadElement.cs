using System.Collections.Generic;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Elements
{
    public class QuadElement : Element
    {
        public QuadElement(int number, IList<Node> nodes)
        {
            base.numberOfNodes = 4;
            base.CheckNumberOfNodesInArray(nodes);
            base.CheckIfNodesAreNull(nodes);
            base.number = number;
            this.nodes = nodes;
            this.CalculateCentre();
        }

        protected override void CalculateCentre()
        {
            Node node0 = this.Nodes[0];
            Node node2 = this.Nodes[2];

            Vector vector = new Vector(node0.Coordinates, node2.Coordinates);

            Vector halfVector = 0.5 * vector;

            PointD centre = new PointD
            {
                X = node0.Coordinates.X + halfVector.X,
                Y = node0.Coordinates.Y + halfVector.Y,
                Z = node0.Coordinates.Z + halfVector.Z
            };

            this.centre = centre;
        }
    }
}