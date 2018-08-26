using IncoherentMeshChecker.Shared.Geometry;
using System;

namespace IncoherentMeshChecker.Calculations.Nodes
{
    public class Node : IEquatable<Node>
    {
        public int Number { get; set; }
        public PointD Coordinates { get; set; }

        public Node()
        {
        }

        public Node(int number, PointD coordinates)
        {
            this.Number = number;
            this.Coordinates = coordinates;
        }

        public bool Equals(Node other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            if (Object.ReferenceEquals(this, other)) return true;

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
}