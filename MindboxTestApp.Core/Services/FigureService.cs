using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dawn;
using Microsoft.EntityFrameworkCore;
using MindboxTestApp.Core.Entities;
using MindboxTestApp.Core.Entities.Abstractions;

namespace MindboxTestApp.Core.Services
{
    public sealed class FigureService : IFigureService
    {
        private static readonly Dictionary<FigureType, Func<int, IApplicationDbContextFactory, Task<GeometricFigure>>>
            figureGetters =
                new Dictionary<FigureType, Func<int, IApplicationDbContextFactory, Task<GeometricFigure>>>();

        private readonly IApplicationDbContextFactory _dbContextFactory;

        public FigureService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = Guard.Argument(dbContextFactory, nameof(dbContextFactory)).NotNull().Value;
        }

        public Task<T> Get<T>(int id) where T : GeometricFigure
        {
            Guard.Argument(id, nameof(id)).Positive();
            return GetInternal<T>(id, _dbContextFactory);
        }

        public Task<T> Add<T>(T entity) where T : GeometricFigure
        {
            Guard.Argument(entity, nameof(entity)).NotNull();
            return AddInternal(entity, _dbContextFactory);
        }

        public void RegisterFigure<T>(FigureType figureType) where T : GeometricFigure
        {
            Guard.Argument(figureType, nameof(figureType)).Defined();
            figureGetters.Add(figureType, async (i, dbFactory) => await GetInternal<T>(i, dbFactory));
        }

        public Task<GeometricFigure> Get(int id, FigureType figureType)
        {
            Guard.Argument(id, nameof(id)).Positive();
            Guard.Argument(figureType, nameof(figureType)).Defined();
            return figureGetters[figureType](id, _dbContextFactory);
        }

        private static Task<T> GetInternal<T>(int id, IApplicationDbContextFactory dbContextFactory)
            where T : GeometricFigure
        {
            using ApplicationDbContext dbContext = dbContextFactory.Create();
            return dbContext.Set<T>()
                .FirstOrDefaultAsync(i => i.Id == id);
            ;
        }

        private static async Task<T> AddInternal<T>(T entity, IApplicationDbContextFactory dbContextFactory)
            where T : GeometricFigure
        {
            await using ApplicationDbContext dbContext = dbContextFactory.Create();
            await dbContext.Set<T>()
                .AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}