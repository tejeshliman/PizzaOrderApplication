using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PizzaOrderApplication.Core.Exception;
using PizzaOrderApplication.Core.Kernel;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PizzaOrderApplication.Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException cx)
            {
                await HandleExceptionAsync(httpContext, cx.InnerException, cx.ExMessage);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, "");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, string ExMessage)
        {
            _logger.LogError(ExMessage);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var msg = "Internal Server Error. Message:" + ExMessage;
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(
                new Error()
                {
                    ErrorCode = context.Response.StatusCode,
                    ErrorMessage = msg
                }));
        }
    }
}
