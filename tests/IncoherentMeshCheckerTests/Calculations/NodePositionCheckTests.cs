using NUnit.Framework;
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
    [TestFixture()]
    public class NodePositionCheckTests
    {
        [Test()]
        public void Check_NodeToCheckHasTheSameCoordinatesAsElementNode_Failed()
        {
            Node nodeToCheck = new Node() { Number = 1, Coordinates = new PointD(0.1, 0.1, 0.1) };
            Node elementNode1 = new Node() { Number = 2, Coordinates = new PointD(0.1, 0.1, 0.1) };
            Node elementNode2 = new Node() { Number = 3, Coordinates = new PointD(0.2, 0.1, 0.1) };

           
            Assert.IsFalse(NodePositionCheck.CheckNodes(elementNode1, elementNode2, nodeToCheck));
        }
        [Test()]
        public void Check_NodesAreNotLinear_Failed()
        {
            Node nodeToCheck = new Node() { Number = 1, Coordinates = new PointD(0.1, 0.1, 0.1) };
            Node elementNode1 = new Node() { Number = 2, Coordinates = new PointD(0.1, 0.3, 0.1) };
            Node elementNode2 = new Node() { Number = 3, Coordinates = new PointD(0.2, 0.1, 0.1) };

           
            Assert.IsFalse(NodePositionCheck.CheckNodes(elementNode1, elementNode2, nodeToCheck));
        }
        [Test()]
        public void Check_NodesAreLinearAndNodeIsOutsideElement_Failed()
        {
            Node nodeToCheck = new Node() { Number = 1, Coordinates = new PointD(0.1, 0.1, 0.1) };
            Node elementNode1 = new Node() { Number = 2, Coordinates = new PointD(0.4, 0.4, 0.4) };
            Node elementNode2 = new Node() { Number = 3, Coordinates = new PointD(0.8, 0.8, 0.8) };

            
            Assert.IsFalse(NodePositionCheck.CheckNodes(elementNode1, elementNode2, nodeToCheck));
        }
    }
}