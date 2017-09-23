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
    public class NodeTests
    {
        [TestMethod()]
        public void CompareCoordinates_ComparingTwoTheSameNodesUsingOperator_Passed()
        {
            Node node1 = new Node(1, new PointD(1.00014581, 4.00014581, 8.00014581));
            Node node2 = new Node(2, new PointD(1.00014581, 4.00014581, 8.00014581));

            Assert.IsTrue(node1.Coordinates == node2.Coordinates);
        }
        [TestMethod()]
        public void CompareCoordinates_ComparingTwoTheSameNodesUsingOperator_Failed()
        {
            Node node1 = new Node(1, new PointD(1.00014581, 4.00014581, 9.00014581));
            Node node2 = new Node(2, new PointD(1.00014581, 4.00014581, 8.00014581));

            Assert.IsFalse(node1.Coordinates == node2.Coordinates);
        }
    }
}