using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculateShapeArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateShapeArea.Tests
{
    [TestClass()]
    public class TriangleTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle1_ZeroLengthSidesAll()
        {
            var triangle = new Triangle(0.0, 0.0, 0.0);
            var area = triangle.Area;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle2_ZeroLengthSides_1()
        {
            var triangle = new Triangle(0.0, 1.0, 2.0);
            var area = triangle.Area;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle3_ZeroLengthSides_2()
        {
            var triangle = new Triangle(0.0, 1.0, 0.0);
            var area = triangle.Area;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle4_NegativeLengthSidesAll()
        {
            var triangle = new Triangle(-14.0, -9.0, -6.0);
            var area = triangle.Area;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle5_NegativeLengthSides_1()
        {
            var triangle = new Triangle(-14.0, 9.0, 6.0);
            var area = triangle.Area;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Triangle6_NegativeLengthSides_2()
        {
            var triangle = new Triangle(-14.0, 9.0, -6.0);
            var area = triangle.Area;
        }

        [TestMethod]
        public void Triangle7_NormalTriangle()
        {
            var triangle = new Triangle(14.0, 9.0, 6.0);
            var area = triangle.Area;
            double p = (14.0 + 9.0 + 6.0) / 2;
            var earea = Math.Sqrt(p * (p - 14.0) * (p - 9.0) * (p - 6.0));
            Assert.AreEqual(area, earea);
        }

        [TestMethod]
        public void Triangle8_MaxDoubleArea()
        {
            var triangle = new Triangle(double.MaxValue / 3, double.MaxValue / 3, double.MaxValue / 3);
            var area = triangle.Area;
            Assert.AreEqual(area, double.PositiveInfinity);
        }

        [TestMethod]
        public void Triangle9_RightTriangle()
        {
            var triangle = new Triangle(3.0, 4.0, 5.0);
            var right = triangle.IsRightTriangle();
            Assert.AreEqual(right, true);
        }

        [TestMethod]
        public void Triangle10_IsNotRightTriangle()
        {
            var triangle = new Triangle(3.0, 4.0, 6.0);
            var right = triangle.IsRightTriangle();
            Assert.AreEqual(right, false);
        }

        [TestMethod]
        public void Triangle11_FailedToDetermineRightTriangle()
        {
            var triangle = new Triangle(double.MaxValue / 3, double.MaxValue / 3, double.MaxValue / 3);
            var right = triangle.IsRightTriangle();
            Assert.AreEqual(right, null);
        }
    }
}