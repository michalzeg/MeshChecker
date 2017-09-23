using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncoherentMeshChecker.Model.Geometry;
using Extensions;
namespace IncoherentMeshChecker.Model.Elements
{
    public class Node : IEquatable<Node>
    {
        public int Number { get; set; }
        public PointD Coordinates { get; set; }

        public Node() { }
        public Node(int number,PointD coordinates)
        {
            this.Number = number;
            this.Coordinates = coordinates;
        }

        public bool Equals(Node other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;
            //Check whether the products' properties are equal. 
            return this.Number == other.Number && this.Coordinates == other.Coordinates;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            if (node == null)
                return false;

            return this.Equals(node);
        }

        public override int GetHashCode()
        {
            var numberHash = this.Number.GetHashCode();
            var coordinatesHash = this.Coordinates.GetHashCode();

            return numberHash ^ coordinatesHash;
        }

    }

    public enum ElementType
    {
        Quad = 4,
        Triangle = 3,


    }
    public abstract class Element:IEquatable<Element> //describes finite element
    {
        public int NumberOfNodes { get { return this.numberOfNodes; } }
        public int Number { get { return this.number; } }
        public IList<Node> Nodes { get { return this.nodes; } }
        public PointD Centre { get { return this.centre; } }

        protected int numberOfNodes;
        protected int number;
        protected IList<Node> nodes;
        protected PointD centre;

        protected abstract void calculateCentre();

        protected void checkNumberOfNodesInArray(IList<Node> nodes)
        {
      
            if (nodes.Count != this.numberOfNodes)
            {
                string message = string.Format("{0} has to have {1} nodes", this.GetType().Name, numberOfNodes);
                throw new ArgumentException(message);
            }

        }
        protected void checkIfNodesAreNull(IList<Node> nodes)
        {
            if (nodes.Any(n => n == null))
                throw new ArgumentNullException("One of the nodes is null");
      
        }

        public bool Equals(Element other)
        {
            if (this.NumberOfNodes == other.NumberOfNodes
                && this.Number == other.Number
                && this.Nodes.ScrambledEquals(other.Nodes)
                && this.Centre == other.Centre)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            var element = obj as Element;
            if (element == null)
                return false;
            return this.Equals(element);
        }

        public override int GetHashCode()
        {
            var numberOfNodesHash = NumberOfNodes.GetHashCode();
            var numberHash = Number.GetHashCode();
            var nodesHash = Nodes.GetHashCode();
            var centreHash = Centre.GetHashCode();

            return numberOfNodesHash ^ numberHash ^ nodesHash ^ centreHash;
        }
    }

    public class QuadElement : Element
    {
        
        public QuadElement(int number, IList<Node> nodes)
        {
            base.numberOfNodes = 4;
            base.checkNumberOfNodesInArray( nodes);
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
