using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Mapster;
using MindboxTestApp.Core.DTO.Requests;
using MindboxTestApp.Core.Entities;
using MindboxTestApp.Core.Entities.Abstractions;
using MindboxTestApp.Core.Services;

namespace MindboxTestApp.Core.Mappings
{
    [ExcludeFromCodeCoverage]
    public static class FigureMappings
    {
        private static readonly Dictionary<FigureType, Type> FigureAddBaseDtos = new Dictionary<FigureType, Type>();

        public static void ConfigureMapsterFigureMappings<TEntity, TEntityAddDto>(Func<TEntityAddDto, TEntity> functor,
            FigureType figureType, IFigureService figureService)
            where TEntity : GeometricFigure
            where TEntityAddDto : AddFigureRequestBaseDto, new()
        {
            TypeAdapterConfig<TEntityAddDto, TEntity>.NewConfig()
                .MapWith(i => functor(i));
            TypeAdapterConfig<TEntityAddDto, GeometricFigure>.NewConfig()
                .MapWith(i => i.Adapt<TEntity>());
            figureService.RegisterFigure<TEntity>(figureType);
            FigureAddBaseDtos.Add(figureType, typeof(TEntityAddDto));
        }

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        private static Circle Map(AddCircleRequestDto addCircleDto)
        {
            return new Circle(addCircleDto.Radius.Value);
        }

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        private static Triangle Map(AddTriangleRequestDto addTriangleDto)
        {
            return new Triangle(addTriangleDto.ALength.Value, addTriangleDto.BLength.Value,
                addTriangleDto.AbAngle.Value);
        }

        public static void ConfigureMapsterMappings(IFigureService figureService)
        {
            TypeAdapterConfig<FigureType, AddFigureRequestBaseDto>.NewConfig()
                .MapWith(i => CreateSpecificAddFigureRequestDto(i));
            ConfigureMapsterFigureMappings<Circle, AddCircleRequestDto>(Map, FigureType.Circle, figureService);
            ConfigureMapsterFigureMappings<Triangle, AddTriangleRequestDto>(Map, FigureType.Triangle, figureService);
        }

        private static AddFigureRequestBaseDto CreateSpecificAddFigureRequestDto(FigureType figureType)
        {
            return (AddFigureRequestBaseDto) Activator.CreateInstance(FigureAddBaseDtos[figureType], true);
        }
    }
}