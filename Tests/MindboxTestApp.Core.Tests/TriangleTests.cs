using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindboxTestApp.Core.Entities;

namespace MindboxTestApp.Core.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TriangleTests : GeometricFigureTests
    {
        [TestMethod]
        public override void Area()
        {
            //arrange
            const double aLength = 5;
            const double bLength = 5;
            const double abAngle = 5;
            const double expectedResult = 1.089447;
            const double delta = 0.000001;
            var triangle = new Triangle(aLength, bLength, abAngle);

            //actions
            double area = triangle.GetArea();

            //asserts
            Assert.IsTrue(Math.Abs(expectedResult - area) < delta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ALength_Invalid()
        {
            //arrange
            const double aLength = -1;
            const double bLength = 5;
            const double abAngle = 5;

            //actions
            var triangle = new Triangle(aLength, bLength, abAngle);

            //asserts
            //exception expected
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BLength_Invalid()
        {
            //arrange
            const double aLength = 5;
            const double bLength = -1;
            const double abAngle = 5;

            //actions
            var triangle = new Triangle(aLength, bLength, abAngle);

            //asserts
            //exception expected
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ABAngle_Zero()
        {
            //arrange
            const double aLength = 5;
            const double bLength = 5;
            const double abAngle = 0;

            //actions
            var triangle = new Triangle(aLength, bLength, abAngle);

            //asserts
            //exception expected
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ABAngle_Greater180()
        {
            //arrange
            const double aLength = 5;
            const double bLength = 5;
            const double abAngle = 181;

            //actions
            var triangle = new Triangle(aLength, bLength, abAngle);

            //asserts
            //exception expected
        }
    }
}
