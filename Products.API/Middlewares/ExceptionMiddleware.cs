using System;
using System.Net;
using Products.API.Exceptions.Products.Application.Exceptions;
using Products.API.Exceptions;
using Serilog;


namespace Products.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    InsufficientStockException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                httpContext.Response.ContentType = "application/json";

                string message  = ex switch
                {
                    NotFoundException => ex.Message,
                    InsufficientStockException => ex.Message,
                    _ => "An unexpected error occurred."
                };
                var errorResponse = new { Message = message };
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }

}
