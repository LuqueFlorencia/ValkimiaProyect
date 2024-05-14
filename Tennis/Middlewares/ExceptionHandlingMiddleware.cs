using System.Net;
using Newtonsoft.Json;

namespace Tennis.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        //private readonly RequestDelegate _next;

        //public ExceptionHandlingMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    try
        //    {
        //        await _next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción
        //        await HandleExceptionAsync(context, ex);
        //    }
        //}

        //private Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    // Configurar la respuesta HTTP
        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //    // Crear un objeto JSON con el mensaje de error
        //    var response = new
        //    {
        //        StatusCode = context.Response.StatusCode,
        //        Message = "Se ha producido un error en el servidor."
        //    };

        //    // Serializar el objeto JSON y enviarlo como respuesta
        //    var jsonResponse = JsonSerializer.Serialize(response);
        //    return context.Response.WriteAsync(jsonResponse);
        //}
    }
}
