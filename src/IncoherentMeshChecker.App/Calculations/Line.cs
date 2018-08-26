namespace IncoherentMeshChecker.Model.Geometry
{
    public class Line //describes a line in 3d space
    {
        //equation of line
        //x = Point1.X + DirectionVector.X * t
        //y = Point1.Y + DirectionVector.Y * t
        //z = Point1.Z + DirectionVector.Z * t

        public PointD Point1 { get; set; } //point describint line
        public PointD Point2 { get; set; }
        public Vector DirectionVector { get; set; }

        public Line(PointD point1, PointD point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            this.DirectionVector = new Vector(point1, point2);
        }

        public Line()
        {
        }
    }
}