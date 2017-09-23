using Microsoft.VisualStudio.TestTools.UnitTesting;
using IncoherentMeshChecker.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Elements.Tests
{
    [TestClass()]
    public class TriangleElementTests
    {
        [TestMethod()]
        public void Centre_CalculateCentreIn2D_Calculated()
        {
            IList<Node> nodes = new List<Node>();
            nodes.Add(new Node(1, new PointD(1, 3, 0)));
            nodes.Add(new Node(2, new PointD(2, 3, 0)));
            nodes.Add(new Node(3, new PointD(3, 3, 0)));


            Element element = new TriangleElement(1, nodes);
            PointD expectedCentre = new PointD(2, 3, 0);
            PointD actualCentre = element.Centre;

            Assert.AreEqual(expectedCentre, actualCentre);

        }
        [TestMethod()]
        public void Centre_CalculateCentreIn3D_Calculated()
        {
            IList<Node> nodes = new List<Node>();
            nodes.Add(new Node(1, new PointD(-1, 0, -8)));
            nodes.Add(new Node(2, new PointD(-1, 1, -1)));
            nodes.Add(new Node(3, new PointD(-1, 11, -6)));


            Element element = new TriangleElement(1, nodes);
            PointD expectedCentre = new PointD(-1, 4, -5);
            PointD actualCentre = element.Centre;

            Assert.AreEqual(expectedCentre, actualCentre);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void Exception_CheckNumberOfNodes_ThrowException()
        {
            IList<Node> nodes = new Node[2];

            Element element = new TriangleElement(1, nodes);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Exception_CheckIfAnyNodeIsNull_ThrowException()
        {
            IList<Node> nodes = new Node[3];

            Element element = new TriangleElement(1, nodes);
        }
    }
}