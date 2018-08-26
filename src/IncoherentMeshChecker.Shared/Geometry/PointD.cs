using IncoherentMeshChecker.Shared.Extensions;
using System;

namespace IncoherentMeshChecker.Shared.Geometry
{
    public class PointD : IEquatable<PointD>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public PointD(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public PointD()
        {
        }

        public override bool Equals(object obj)
        {
            PointD other = obj as PointD;
            if (other == null)
                return false;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        public bool Equals(PointD other)
        {
            return (this.X.IsApproximatelyEqualTo(other.X)
                && this.Y.IsApproximatelyEqualTo(other.Y)
                && this.Z.IsApproximatelyEqualTo(other.Z));
        }

        public static bool operator ==(PointD point1, PointD point2)
        {
            if (System.Object.ReferenceEquals(point1, point2))
                return true;
            if ((object)point1 == null || (object)point2 == null)
                return false;
            return (point1.X.IsApproximatelyEqualTo(point2.X) && point1.Y.IsApproximatelyEqualTo(point2.Y) && point1.Z.IsApproximatelyEqualTo(point2.Z));
        }

        public static bool operator !=(PointD point1, PointD point2)
        {
            return !(point1 == point2);
        }
    }
}