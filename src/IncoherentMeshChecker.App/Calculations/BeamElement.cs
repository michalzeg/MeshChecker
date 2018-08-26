using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncoherentMeshChecker.Model.Geometry;
using Extensions;

namespace IncoherentMeshChecker.Model.Elements
{
    public class BeamElement : Element
    {
        public BeamElement(int number, IList<Node> nodes)
        {
            base.numberOfNodes = 2;
            base.CheckNumberOfNodesInArray(nodes);
            base.CheckIfNodesAreNull(nodes);
            this.nodes = nodes;
            base.number = number;
            this.CalculateCentre();
        }

        protected override void CalculateCentre()
        {
            PointD centre = new PointD();
            centre.X = Nodes.Sum(m => m.Coordinates.X) / 2;
            centre.Y = Nodes.Sum(m => m.Coordinates.Y) / 2;
            centre.Z = Nodes.Sum(m => m.Coordinates.Z) / 2;
            this.centre = centre;
        }
    }
}