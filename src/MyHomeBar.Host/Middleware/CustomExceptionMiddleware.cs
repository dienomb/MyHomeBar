namespace MyHomeBar.Host.Middleware
{
    using Microsoft.AspNetCore.Http;
    using MyHomeBar.Api.HttpErrors;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Serilog.Core;
    using Serilog;

    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                Serilog.Context.LogContext.PushProperty("CorrelationId", "XXXXXXXX");
                await next(httpContext);
            }
            catch (Exception exception)
            {
                Log.ForContext(Constants.SourceContextPropertyName, exception.Source).Error(exception, exception.Message);
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            CustomHttpError error = CustomHttpError.CreateCustomHttpError(exception);
            await WriteResponseAsync(
                context,
                JsonConvert.SerializeObject(error),
                "application/json",
                error.HttpStatusCode);
        }

        private Task WriteResponseAsync(
            HttpContext context,
            string content,
            string contentType,
            HttpStatusCode statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
            context.Response.Headers["Pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(content);
        }
    }
}
