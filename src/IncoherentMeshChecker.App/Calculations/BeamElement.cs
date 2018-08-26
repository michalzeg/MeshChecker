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
            base.checkNumberOfNodesInArray(nodes);
            base.checkIfNodesAreNull(nodes);
            this.nodes = nodes;
            base.number = number;
            this.calculateCentre();
        }

        protected override void calculateCentre()
        {
            //centre is calculated as mean value of coordinates divided by 3
            PointD centre = new PointD();
            centre.X = Nodes.Sum(m => m.Coordinates.X) / 2;
            centre.Y = Nodes.Sum(m => m.Coordinates.Y) / 2;
            centre.Z = Nodes.Sum(m => m.Coordinates.Z) / 2;
            this.centre = centre;
        }
    }
}