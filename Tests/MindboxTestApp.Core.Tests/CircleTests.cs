using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindboxTestApp.Core.Entities;

namespace MindboxTestApp.Core.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CircleTests : GeometricFigureTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctor_InvalidRadius()
        {
            //arrange
            const double radius = -1;

            //actions
            var circle = new Circle(radius);

            //asserts
            //exception expected
        }

        [TestMethod]
        public void Ctor()
        {
            //arrange
            const double radius = 5;

            //actions
            var circle = new Circle(radius);

            //asserts
            Assert.AreEqual(radius, circle.Radius);
            Assert.AreEqual(radius, circle.MajorAxisLength);
            Assert.AreEqual(radius, circle.MinorAxisLength);
            Assert.AreEqual(0, circle.Id);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Ctor_ZeroArea()
        {
            //arrange
            const double radius = 0;

            //actions
            var circle = new Circle(radius);

            //asserts
            Assert.AreEqual(0, circle.GetArea());
        }

        [TestMethod]
        public override void Area()
        {
            //arrange
            const double radius = 5;
            const double expectedArea = 78.53981633974483;
            const double delta = 0.0000001;

            //actions
            var circle = new Circle(radius);
            double result = circle.GetArea();

            //asserts
            Assert.IsTrue(expectedArea - result < delta);
        }
    }
}
