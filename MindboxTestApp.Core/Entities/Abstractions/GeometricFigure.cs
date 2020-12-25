namespace MindboxTestApp.Core.Entities.Abstractions
{
    public abstract class GeometricFigure : StorableEntity, IGeometricFigure
    {
        public abstract double GetArea();
    }
}