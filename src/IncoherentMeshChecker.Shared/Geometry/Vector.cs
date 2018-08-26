using IncoherentMeshChecker.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncoherentMeshChecker.Shared.Geometry
{
    public class Vector
    {
        public static readonly Vector NullVector = new Vector(0, 0, 0);

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector()
        {
        }

        public Vector(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector(PointD startPoint, PointD endPoint)
        {
            this.X = endPoint.X - startPoint.X;
            this.Y = endPoint.Y - startPoint.Y;
            this.Z = endPoint.Z - startPoint.Z;
        }

        public override bool Equals(object obj)
        {
            Vector other = obj as Vector;
            if (other == null)
                return false;
            return (this.X.IsApproximatelyEqualTo(other.X) && this.Y.IsApproximatelyEqualTo(other.Y) && this.Z.IsApproximatelyEqualTo(other.Z));
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
        }

        public double GetLength()
        {
            double length = Math.Sqrt(X * X + Y * Y + Z * Z);
            return length;
        }

        public Vector CrossProduct(Vector vector)
        {
            double x, y, z;
            x = this.Y * vector.Z - vector.Y * this.Z;
            y = -(this.X * vector.Z - this.Z * vector.X);
            z = this.X * vector.Y - vector.X * this.Y;
            var result = new Vector(x, y, z);
            return result;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector sum = new Vector();
            sum.X = v1.X + v2.X;
            sum.Y = v1.Y + v2.Y;
            sum.Z = v1.Z + v2.Z;
            return sum;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector sum = new Vector();
            sum.X = v1.X - v2.X;
            sum.Y = v1.Y - v2.Y;
            sum.Z = v1.Z - v2.Z;
            return sum;
        }

        public static Vector operator *(double m, Vector v)
        {
            Vector sum = new Vector();
            sum.X = m * v.X;
            sum.Y = m * v.Y;
            sum.Z = m * v.Z;
            return sum;
        }

        public static Vector operator *(Vector v, double m)
        {
            Vector sum = new Vector();
            sum.X = m * v.X;
            sum.Y = m * v.Y;
            sum.Z = m * v.Z;
            return sum;
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            if (System.Object.ReferenceEquals(vector1, vector2))
                return true;
            if ((object)vector1 == null || (object)vector2 == null)
                return false;
            return (vector1.X.IsApproximatelyEqualTo(vector2.X) && vector1.Y.IsApproximatelyEqualTo(vector2.Y) && vector1.Z.IsApproximatelyEqualTo(vector2.Z));
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
        }
    }
}