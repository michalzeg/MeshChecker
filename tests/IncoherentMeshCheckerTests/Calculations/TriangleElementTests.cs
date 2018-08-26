using IncoherentMeshChecker.Calculations.Elements;
using IncoherentMeshChecker.Calculations.Nodes;
using IncoherentMeshChecker.Shared.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncoherentMeshCheckerTests.Model
{
    [TestClass()]
    public class TriangleElementTests
    {
        [TestMethod()]
        public void Centre_CalculateCentreIn2D_Calculated()
        {
            IList<Node> nodes = new List<Node>
            {
                new Node(1, new PointD(1, 3, 0)),
                new Node(2, new PointD(2, 3, 0)),
                new Node(3, new PointD(3, 3, 0))
            };

            Element element = new TriangleElement(1, nodes);
            PointD expectedCentre = new PointD(2, 3, 0);
            PointD actualCentre = element.Centre;

            Assert.AreEqual(expectedCentre, actualCentre);
        }

        [TestMethod()]
        public void Centre_CalculateCentreIn3D_Calculated()
        {
            IList<Node> nodes = new List<Node>
            {
                new Node(1, new PointD(-1, 0, -8)),
                new Node(2, new PointD(-1, 1, -1)),
                new Node(3, new PointD(-1, 11, -6))
            };

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