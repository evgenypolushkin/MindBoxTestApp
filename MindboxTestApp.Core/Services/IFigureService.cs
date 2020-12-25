using System.Threading.Tasks;
using MindboxTestApp.Core.Entities;
using MindboxTestApp.Core.Entities.Abstractions;

namespace MindboxTestApp.Core.Services
{
    public interface IFigureService
    {
        Task<T> Get<T>(int id) where T : GeometricFigure;
        Task<T> Add<T>(T entity) where T : GeometricFigure;

        Task<GeometricFigure> Get(int id, FigureType figureType);

        void RegisterFigure<T>(FigureType figureType) where T : GeometricFigure;
    }
}