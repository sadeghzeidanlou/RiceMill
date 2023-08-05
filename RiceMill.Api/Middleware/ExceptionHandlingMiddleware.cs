using Azure.Core;
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
            HttpResponse response = context.Response;
            var originBody = response.Body;
            using var newBody = new MemoryStream();
            response.Body = newBody;
            try
            {
                context.Response.ContentType = ContentType.ApplicationJson.ToString();
                await _next(context);
            }
            catch (DbUpdateException dbEx)
            {
                logging.Error($"Database update error occurred during request processing. Path: {context.Request.Path}", dbEx);
                await ModifyResponseAsync(newBody, response, ResultStatusEnum.DatabaseError, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                logging.Error($"Unhandled exception occurred during request processing. Path: {context.Request.Path}", ex);
                await ModifyResponseAsync(newBody, response, ResultStatusEnum.UnHandleError, HttpStatusCode.InternalServerError);
            }
            finally
            {
                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originBody);
                response.Body = originBody;
            }
        }

        private static async Task ModifyResponseAsync(Stream stream, HttpResponse response, ResultStatusEnum resultStatus, HttpStatusCode statusCode)
        {
            response.StatusCode = (int)statusCode;
            stream.SetLength(0);
            var error = Result<bool>.Failure(new Error(resultStatus), statusCode);
            using var writer = new StreamWriter(stream, leaveOpen: true);
            await writer.WriteAsync(error.SerializeObject());
            await writer.FlushAsync();
        }
    }
}