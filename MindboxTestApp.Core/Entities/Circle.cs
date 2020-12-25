using System.Diagnostics.CodeAnalysis;

namespace MindboxTestApp.Core.Entities
{
    public sealed class Circle : Ellipse
    {
        public Circle(double radius)
            : base(radius, radius)
        {
        }

        /// <summary>
        ///     For ORM
        /// </summary>
        /// <param name="majorAxisLength"></param>
        /// <param name="minorAxisLength"></param>
        [ExcludeFromCodeCoverage]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private Circle(double majorAxisLength, double minorAxisLength)
            : base(majorAxisLength, minorAxisLength)
        {
        }

        public double Radius => MajorAxisLength;
    }
}