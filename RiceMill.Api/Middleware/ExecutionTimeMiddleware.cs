using RiceMill.Application.Common.Interfaces;
using Shared.ExtensionMethods;
using System.Diagnostics;

namespace RiceMill.Api.Middleware
{
    public sealed class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggingService _loggingService;

        public ExecutionTimeMiddleware(RequestDelegate next, ILoggingService loggingService)
        {
            _next = next;
            _loggingService = loggingService;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            using var responseBody = new MemoryStream();
            var originalBodyStream = context.Response.Body;
            context.Response.Body = responseBody;

            await _next(context);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var logMessage = $"Request took long.\n\n" +
                                 $"Request path: {context.Request.Path}\n\n" +
                                 $"Query string:{GetRequestQueryString(context)}\n\n" +
                                 $"Headers:{GetRequestHeaders(context)}\n" +
                                 $"Request body:{GetRequestBody(context)}\n\n" +
                                 $"Response body:{GetResponseBody(responseBody)}\n\n" +
                                 $"Response time: {elapsedMilliseconds} milliseconds.\n\n";

                _loggingService.Warning(logMessage);
            }
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }

        private static string GetRequestQueryString(HttpContext context)
        {
            var queryString = context.Request.QueryString.ToString();
            return queryString.IsNullOrEmpty() ? " Empty" : $"\n\t{queryString}";
        }

        private static string GetRequestHeaders(HttpContext context) => context.Request.Headers.IsCollectionNullOrEmpty()
            ? " Empty"
            : "\n" + string.Concat(context.Request.Headers.ToList().Select(header => $"\t{header.Key}: {header.Value}\n"));

        private static string GetRequestBody(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();
            request.Body.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = streamReader.ReadToEndAsync().Result;
            request.Body.Position = 0;
            return requestBody.IsNullOrEmpty() ? " Empty" : $"\n{requestBody}".Replace("\n", "\n\t");
        }

        private static string GetResponseBody(MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(responseBody).ReadToEndAsync().Result;
            return responseBodyText.IsNullOrEmpty() ? " Empty" : $"\n{responseBodyText.JsonPrettify()}";
        }
    }
}