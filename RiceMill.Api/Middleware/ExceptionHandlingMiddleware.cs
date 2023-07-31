using Microsoft.EntityFrameworkCore;
using RiceMill.Application.Common.Interfaces;
using RiceMill.Application.Common.Models.Enums;
using RiceMill.Application.Common.Models.ResultObject;
using Shared.ExtensionMethods;
using System.Net;

namespace RiceMill.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ILoggingService logging)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException dbEx)
            {
                logging.Error($"Database update error occurred during request processing. Path: {context.Request.Path}", dbEx);
                var error = Result<bool>.Failure(new Error(ResultStatusEnum.DatabaseError), HttpStatusCode.InternalServerError);
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(error.SerializeObject());
            }
            catch (Exception ex)
            {
                logging.Error($"Unhandled exception occurred during request processing. Path: {context.Request.Path}", ex);
                var error = Result<bool>.Failure(new Error(ResultStatusEnum.UnHandleError), HttpStatusCode.InternalServerError);
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(error.SerializeObject());
            }
        }
    }
}