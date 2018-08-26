namespace IncoherentMeshChecker.Shared.Geometry
{
    public class Line
    {
        public PointD Point1 { get; private set; }
        public PointD Point2 { get; private set; }
        public Vector DirectionVector { get; private set; }

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