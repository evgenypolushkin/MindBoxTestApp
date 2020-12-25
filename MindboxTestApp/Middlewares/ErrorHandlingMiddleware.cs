using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MindboxTestApp.Middlewares
{
    /// <summary>
    ///     Unhandled errors handling middleware
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="logger"></param>
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Sorry :( Something went wrong. See logs for details.");
            }
        }
    }
}