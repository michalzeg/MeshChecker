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
    public class NodesLinearityTests
    {
        [TestMethod()]
        public void CheckNodesLinearity_TwoParallelVectors_Passed()
        {
            Node node1 = new Node(1, new PointD(1.0001458, 2.0001458, 5.0001458));
            Node node2 = new Node(2, new PointD(2.0001458, 2.0001458, 5.0001458));
            Node nodeToCheck = new Node(3, new PointD(0.5001458, 2.0001458, 5.0001458));

            Assert.IsTrue(NodePosition.NodesLinearity(node1, node2, nodeToCheck));
        }

        [TestMethod()]
        public void CheckNodesLinearity_TwoNotParallelVectors_Failed()
        {
            Node node1 = new Node(1, new PointD(1.0001458, 8, 5));
            Node node2 = new Node(2, new PointD(2, 2, 5));
            Node nodeToCheck = new Node(3, new PointD(0.5, 2, 5));

            Assert.IsFalse(NodePosition.NodesLinearity(node1, node2, nodeToCheck));
        }
    }
}