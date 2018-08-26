using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.Tests
{
    [TestClass()]
    public class ExtensionMethodsTests
    {
        [TestMethod()]
        public void IsApproxiamtelyEqual_CheckIfTwoNumbersAreEqual_Passed()
        {
            double maximumDifferenceAllowable = 0.01;
            double value1 = 1.00;
            double value2 = 1 * 0.1 / 0.1;

            Assert.IsTrue(value1.IsApproximatelyEqualTo(value2,maximumDifferenceAllowable));
        }
        [TestMethod()]
        public void IsApproxiamtelyEqual_CheckIfTwoNumbersAreEqual2_Passed()
        {
            double epsilon = DoubleExtension.MaximumDifferenceAllowed;

            double value1 = 1.3 * epsilon;
            double value2 = 1.25 * epsilon;

            Assert.IsTrue(value1.IsApproximatelyEqualTo(value2));
        }

    }
}