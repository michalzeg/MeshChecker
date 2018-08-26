using System;
using IncoherentMeshChecker.Model.Elements;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Incoherentness
{
    public static class PositionOfNodes
    {
        public static bool NodesLinearity(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            //function checks if cross product of vectors elementNode1-elementNode2 and elementNode1-nodeToCheck are equal
            Vector v1 = new Vector(elementNode1.Coordinates, elementNode2.Coordinates);
            Vector v2 = new Vector(elementNode1.Coordinates, nodeToCheck.Coordinates);

            return v1.CrossProduct(v2) == Vector.NullVector;
        }

        public static bool CheckNodePosition(Node elementNode1, Node elementNode2, Node nodeToCheck)
        {
            //check if nodeToCheck lies between elementNode1 and elementNode2
            bool result = false;
            double maxX = Math.Max(elementNode1.Coordinates.X, elementNode2.Coordinates.X);
            double minX = Math.Min(elementNode1.Coordinates.X, elementNode2.Coordinates.X);
            double maxY = Math.Max(elementNode1.Coordinates.Y, elementNode2.Coordinates.Y);
            double minY = Math.Min(elementNode1.Coordinates.Y, elementNode2.Coordinates.Y);
            double maxZ = Math.Max(elementNode1.Coordinates.Z, elementNode2.Coordinates.Z);
            double minZ = Math.Min(elementNode1.Coordinates.Z, elementNode2.Coordinates.Z);
            double X = nodeToCheck.Coordinates.X;
            double Y = nodeToCheck.Coordinates.Y;
            double Z = nodeToCheck.Coordinates.Z;

            if (minX <= X && X <= maxX && minY <= Y && Y <= maxY && minZ <= Z && Z <= maxZ)
            {
                result = true;
            }
            return result;
        }
    }
}