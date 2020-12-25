using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MindboxTestApp.Core.DTO.Requests;
using MindboxTestApp.Core.Entities;
using MindboxTestApp.Core.Entities.Abstractions;
using MindboxTestApp.Core.Services;

namespace MindboxTestApp.Controllers
{
    /// <summary>
    ///     Figure controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FigureController : ControllerBase
    {
        private readonly IFigureService _figureService;
        private readonly IMemoryCache _memCache;
        private readonly TimeSpan CACHE_TIMESPAN = TimeSpan.FromMinutes(5);

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="figureService"></param>
        /// <param name="memCache"></param>
        public FigureController(IFigureService figureService, IMemoryCache memCache)
        {
            _figureService = figureService;
            _memCache = memCache;
        }

        /// <summary>
        ///     Returns the area of the figure
        /// </summary>
        /// <param name="id">Figure ID</param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public async Task<IActionResult> Get([FromRoute] int id, [FromQuery] GetFigureQueryParameters queryParams)
        {
            double? area = GetCachedAreaValue(id, queryParams.Type.Value);
            if (area != null)
                return Ok(area);
            GeometricFigure figure = await _figureService.Get(id, queryParams.Type.Value);
            if (figure == null)
                return NotFound();
            area = figure.GetArea();
            SetCachedAreaValue(id, queryParams.Type.Value, area.Value);
            return Ok(area);
        }

        private double? GetCachedAreaValue(int id, FigureType type)
        {
            if (!_memCache.TryGetValue(GetMemCacheKey(id, type), out double value))
                return null;
            return value;
        }

        private void SetCachedAreaValue(int id, FigureType type, double area)
        {
            _memCache.Set(GetMemCacheKey(id, type), area, CACHE_TIMESPAN);
        }

        private static string GetMemCacheKey(int id, FigureType type)
        {
            return $"{type}-{id}";
        }

        /// <summary>
        ///     Adds a new figure
        /// </summary>
        /// <remarks>
        ///     Sample requests:
        ///     POST /figure
        ///     {
        ///     "Type": "Circle",
        ///     "Radius": 5
        ///     }
        ///     POST /figure
        ///     {
        ///     "Type": "Triangle",
        ///     "ALength": 5,
        ///     "BLength": 5,
        ///     "ABAngle": 150
        ///     }
        /// </remarks>
        /// <param name="createDto">Creation DTO</param>
        /// <response code="400">If the figure has not passed validation.</response>
        /// <response code="200">A new figure has been added successfully.</response>
        /// <returns>Figure ID</returns>
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddFigureRequestBaseDto createDto)
        {
            var figure = createDto.Adapt<GeometricFigure>();
            figure = await _figureService.Add(figure);
            return Ok(figure.Id);
        }
    }
}