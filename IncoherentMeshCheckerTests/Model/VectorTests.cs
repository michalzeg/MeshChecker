using Microsoft.VisualStudio.TestTools.UnitTesting;
using IncoherentMeshChecker.Model.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncoherentMeshChecker.Model.Geometry.Tests
{
    [TestClass()]
    public class VectorTests
    {
        [TestMethod()]
        public void Create_CreateVectorUsingTwoPoints_Calcualted()
        {
            PointD startPoint = new PointD(1, 8, -5);
            PointD endPoint = new PointD(-8, -5, 4);

            Vector expectedValue = new Vector(-9, -13, 9);
            Vector actualValue = new Vector(startPoint, endPoint);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod()]
        public void Length_CalculateLength_Calculated()
        {
            Vector v1 = new Vector(1, 2, 2);
            double lengthExpected = 3;
            double lengthActual = v1.GetLength();
            Assert.AreEqual(lengthExpected, lengthActual);
        }

        [TestMethod()]
        public void Add_AddTwoVectors_Calculated()
        {
            Vector v1 = new Vector(2, 1, 5);
            Vector v2 = new Vector(8, 4, 3);

            Vector expectedVector = new Vector(10, 5, 8);
            Vector actualVector = v1 + v2;
            Assert.AreEqual(expectedVector, actualVector);

        }

        [TestMethod()]
        public void Subtract_SubtractsTwoVectors_Calcualted()
        {
            Vector v1 = new Vector(2, 1, 5);
            Vector v2 = new Vector(8, 4, 3);

            Vector expectedVector = new Vector(-6, -3, 2);
            Vector actualVector = v1 - v2;
            Assert.AreEqual(expectedVector, actualVector);
        }
        [TestMethod()]
        public void Multiply_MultiplyDoubleByVector_Calcualted()
        {
            Vector v = new Vector(2, 1, 5);

            Vector expectedVector = new Vector(1, 0.5, 2.5);
            Vector actualVector = 0.5 * v;
            Assert.AreEqual(expectedVector, actualVector);
        }
        [TestMethod()]
        public void Multiply_MultiplyVectorByDouble_Calcualted()
        {
            Vector v = new Vector(2, 1, 5);

            Vector expectedVector = new Vector(1, 0.5, 2.5);
            Vector actualVector = v * 0.5;
            Assert.AreEqual(expectedVector, actualVector);
        }
        [TestMethod()]
        public void CrossProduct_CalculateCrossProduct_Calculated()
        {
            var v1 = new Vector(1, -8, 6);
            var v2 = new Vector(8, -3, 5);

            var expectedVector = new Vector(-22, 43, 61);

            var actualVector = v1.CrossProduct(v2);
            Assert.AreEqual(expectedVector, actualVector);
        }

        [TestMethod()]
        public void CrossProduct_CalculateCrossProductOfParallelVectors_Calculated()
        {
            var v1 = new Vector(1, -8, 6);
            var v2 = new Vector(2, -16, 12);

            var expectedVector = new Vector(0, 0, 0);

            var actualVector = v1.CrossProduct(v2);
            Assert.AreEqual(expectedVector, actualVector);
        }

        [TestMethod()]
        public void Equals_ComparingTwoTheSameVectorsUsingEqualt_Passed()
        {
            var v1 = new Vector(1.0000000001, 4.0000000001, 5.0000000001);
            var v11 = v1 * 0.1 * 10;
            var v2 = new Vector(1.0000000001, 4.0000000001, 5.0000000001);

            Assert.IsTrue(v11.Equals(v2));
        }
        [TestMethod()]
        public void Equals_ComparingTwoTheSameVectorsUsingOperator_Passed()
        {
            var v1 = new Vector(1.0000000001, 4.0000000001, 5.0000000001);
            var v2 = new Vector(1.0000000001, 4.0000000001, 5.0000000001);

            Assert.IsTrue(v1 == v2);
        }
        [TestMethod()]
        public void Equals_ComparingTwoDifferentVectorsUsingOperator_Failed()
        {
            var v1 = new Vector(1, 4, 5);
            var v2 = new Vector(1, 4, 6);

            Assert.IsFalse(v1 == v2);
        }
        
        [TestMethod()]
        public void Equals_ComparingTheSameVector_Passed()
        {
            var v1 = new Vector(1.1, 0.1, -0.1);
            var v2 = v1 * 0.1 * 10;

            Assert.AreEqual(v1, v2); 
        }
    }
}