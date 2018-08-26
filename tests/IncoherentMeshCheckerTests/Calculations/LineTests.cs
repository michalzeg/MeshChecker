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
    public class LineTests
    {
        [TestMethod()]
        public void Create_CreateLineFromTwoPoints_CalculatedDirectionVector()
        {
            PointD startPoint = new PointD(1, 5, -9);
            PointD endPoint = new PointD(-8, -7, -6);

            Vector expectedDirectionVector = new Vector(-9, -12, 3);

            Line line = new Line(startPoint, endPoint);
            Vector actualDirectionVector = line.DirectionVector;

            Assert.AreEqual(expectedDirectionVector, actualDirectionVector);
        }
    }
}