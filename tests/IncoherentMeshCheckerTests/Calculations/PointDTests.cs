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
    public class PointDTests
    {
        [TestMethod()]
        public void Equals_ComparingTwoTheSamePointsUsingEqualt_Passed()
        {
            PointD point1 = new PointD(1 * 0.1 / 0.1, 4.1, 5.1);
            PointD point2 = new PointD(1, 4.1, 5.1);

            Assert.IsTrue(point1.Equals(point2));
        }

        [TestMethod()]
        public void Equals_ComparingTwoTheSamePointsUsingOperator_Passed()
        {
            PointD point1 = new PointD(1.0001458 * 0.1 / 0.1, 4.0001458, 5.0001458);
            PointD point2 = new PointD(1.0001458, 4.0001458, 5.0001458);

            Assert.IsTrue(point1 == point2);
        }

        [TestMethod()]
        public void Equals_ComparingTwoDifferentPointsUsingOperator_Failed()
        {
            PointD point1 = new PointD(1.0001458 * 0.1 / 0.1, 4.0001458, 5);
            PointD point2 = new PointD(1.0001458, 4.0001458, 6);

            Assert.IsFalse(point1 == point2);
        }
    }
}