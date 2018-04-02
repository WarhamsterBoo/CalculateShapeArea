using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculateShapeArea;
using System.Collections.Generic;

namespace CalculateShapeAreaTests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Circle1_ZeroLengthRadius()
        {
            var circle = new Circle(0.0);
            var area = circle.Area;
        }

        [TestMethod]
        public void Circle2_NormalRadius()
        {
            var circle = new Circle(4.0);
            var area = circle.Area;
            Assert.AreEqual(area, 4 * 4 * Math.PI);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Circle3_NegativeRadius()
        {
            var circle = new Circle(-10.0);
            var area = circle.Area;
        }

        [TestMethod]
        public void Circle4_MaxDoubleArea()
        {
            var circle = new Circle(double.MaxValue / 2);
            var area = circle.Area;
            Assert.AreEqual(area, double.PositiveInfinity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Circle5_WrongNumberOfMeasurments()
        {
            var circle = new Circle(10.0);
            circle.Measurments = new List<double>() { 10.1, 5.0 }.AsReadOnly();
            var area = circle.Area;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Circle6_InfinityRadius()
        {
            var circle = new Circle(double.PositiveInfinity);
            var area = circle.Area;
        }
    }
}