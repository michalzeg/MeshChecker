using Microsoft.VisualStudio.TestTools.UnitTesting;
using IncoherentMeshChecker.Model.Incoherentness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncoherentMeshChecker.Model.Elements;
using IncoherentMeshChecker.Model.Geometry;

namespace IncoherentMeshChecker.Model.Incoherentness.Tests
{
    [TestClass()]
    public class NodePositionTests
    {
        [TestMethod()]
        public void Position_NodeToCheckBetweenElementNodes_Passed()
        {
            Node node1 = new Node(1, new PointD(1.000, 2.000, 5.000));
            Node node2 = new Node(2, new PointD(2.000, 2.000, 5.000));
            Node nodeToCheck = new Node(3, new PointD(1.500, 2.000, 5.000));
         

            Assert.IsTrue(NodePosition.CheckNodePosition(node1, node2, nodeToCheck));
        }

        [TestMethod()]
        public void Position_NodeToCheckOutsideElementNodes_Passed()
        {
            Node node1 = new Node(1, new PointD(1.0001458, 2.0001458, 5.0001458));
            Node node2 = new Node(2, new PointD(2.0001458, 2.0001458, 5.0001458));
            Node nodeToCheck = new Node(3, new PointD(-0.5001458, 2.0001458, 5.0001458));
          

            Assert.IsFalse(NodePosition.CheckNodePosition(node1, node2, nodeToCheck));
        }
    }
}