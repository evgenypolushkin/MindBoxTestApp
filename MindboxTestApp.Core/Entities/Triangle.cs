using System;
using Dawn;
using MindboxTestApp.Core.Entities.Abstractions;

namespace MindboxTestApp.Core.Entities
{
    public sealed class Triangle : GeometricFigure
    {
        public Triangle(double aLength, double bLength, double abAngle)
        {
            ALength = Guard.Argument(aLength, nameof(aLength)).Positive().Value;
            BLength = Guard.Argument(bLength, nameof(bLength)).Positive().Value;
            AbAngle = Guard.Argument(abAngle, nameof(abAngle)).Positive().LessThan(180).Value;
        }

        public double ALength { get; private set; }

        public double BLength { get; private set; }

        public double AbAngle { get; private set; }

        public override double GetArea()
        {
            return 0.5 * ALength * BLength * Math.Sin(AbAngle * Math.PI / 180);
        }
    }
}