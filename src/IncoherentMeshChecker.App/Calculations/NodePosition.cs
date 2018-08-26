using System;
using IncoherentMeshChecker.Model.Elements;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Incoherentness
{
    public static class NodePosition
    {
        public static bool NodesLinearity(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            var v1 = new Vector(elementNode1.Coordinates, elementNode2.Coordinates);
            var v2 = new Vector(elementNode1.Coordinates, nodeToCheck.Coordinates);

            return v1.CrossProduct(v2) == Vector.NullVector;
        }

        public static bool CheckNodePosition(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            var result = false;
            var maxX = Math.Max(elementNode1.Coordinates.X, elementNode2.Coordinates.X);
            var minX = Math.Min(elementNode1.Coordinates.X, elementNode2.Coordinates.X);
            var maxY = Math.Max(elementNode1.Coordinates.Y, elementNode2.Coordinates.Y);
            var minY = Math.Min(elementNode1.Coordinates.Y, elementNode2.Coordinates.Y);
            var maxZ = Math.Max(elementNode1.Coordinates.Z, elementNode2.Coordinates.Z);
            var minZ = Math.Min(elementNode1.Coordinates.Z, elementNode2.Coordinates.Z);
            var x = nodeToCheck.Coordinates.X;
            var y = nodeToCheck.Coordinates.Y;
            var z = nodeToCheck.Coordinates.Z;

            if (minX <= x && x <= maxX && minY <= y && y <= maxY && minZ <= z && z <= maxZ)
            {
                result = true;
            }
            return result;
        }
    }
}