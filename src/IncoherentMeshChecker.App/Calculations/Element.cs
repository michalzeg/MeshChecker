using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Elements
{
    public abstract class Element : IEquatable<Element>
    {
        public int NumberOfNodes { get { return this.numberOfNodes; } }
        public int Number { get { return this.number; } }
        public IList<Node> Nodes { get { return this.nodes; } }
        public PointD Centre { get { return this.centre; } }

        protected int numberOfNodes;
        protected int number;
        protected IList<Node> nodes;
        protected PointD centre;

        protected abstract void CalculateCentre();

        protected void CheckNumberOfNodesInArray(IList<Node> nodes)
        {
            if (nodes.Count != this.numberOfNodes)
            {
                string message = string.Format("{0} has to have {1} nodes", this.GetType().Name, numberOfNodes);
                throw new ArgumentException(message);
            }
        }

        protected void CheckIfNodesAreNull(IList<Node> nodes)
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
}