using System;
using Dawn;
using MindboxTestApp.Core.Entities.Abstractions;

namespace MindboxTestApp.Core.Entities
{
    public class Ellipse : GeometricFigure
    {
        public Ellipse(double majorAxisLength, double minorAxisLength)
        {
            MajorAxisLength = Guard.Argument(majorAxisLength, nameof(majorAxisLength)).Positive().Value;
            MinorAxisLength = Guard.Argument(minorAxisLength, nameof(minorAxisLength)).Positive().Value;
        }

        public double MinorAxisLength { get; private set; }

        public double MajorAxisLength { get; private set; }

        public override double GetArea()
        {
            return MinorAxisLength * MajorAxisLength * Math.PI;
        }
    }
}