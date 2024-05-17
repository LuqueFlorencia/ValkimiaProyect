using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Azure;
using Newtonsoft.Json;


namespace Tennis.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, $"Algo salio mal: {exception.Message}");
            //// Configurar la respuesta HTTP
            
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // Aquí puedes manejar diferentes tipos de excepciones
            if (exception is BadRequestException) code = HttpStatusCode.BadRequest; // 400
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized; // 401
            else if (exception is EntityNotFoundException) code = HttpStatusCode.NotFound; // 404
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            var result = System.Text.Json.JsonSerializer.Serialize(new ErrorDetails()
            {
                StatusCode = (int)code,
                Message = $"ERROR: {exception.Message}",
                StackTrace = exception.StackTrace
            });

            return context.Response.WriteAsync(result);
        }
    }
}
