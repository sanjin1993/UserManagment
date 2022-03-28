using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagment.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserManagment.API.CustomExceptions;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //throw new NoUsersFoundException("Niti jedan user pronadjen");
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            context.Response.ContentType = "application/json";

            ApiException response;

            switch (exception)
            {
                case NoUsersFoundException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response =  new ApiException((int)HttpStatusCode.NotFound);
                    break;
                default:
                    response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, exception.Message,
                    exception.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);
                    break;
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);

        }
    }
}