using RiceMill.Application.Common.Interfaces;
using Shared.ExtensionMethods;
using System.Diagnostics;

namespace RiceMill.Api.Middleware
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ExecutionTimeMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ILoggingService logging)
        {
            var stopwatch = Stopwatch.StartNew();
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (elapsedMilliseconds > 500)
            {
                var logMessage = $"Request took long.\n\n" +
                    $"Request Path: {context.Request.Path}\n\n" +
                    $"Query string:{GetRequestQueryString(context)}\n\n" +
                    $"Headers:{GetRequestHeaders(context)}\n" +
                    $"Request body:{GetRequestBody(context)}\n\n" +
                    $"Response body:{GetResponseBody(context, responseBody)}\n\n" +
                    $"Response time: {elapsedMilliseconds} milliseconds.\n\n";

                logging.Warning(logMessage);
            }
        }

        private static string GetRequestQueryString(HttpContext context)
        {
            var queryString = context.Request.QueryString.ToString();
            return queryString.IsNullOrEmpty() ? " Empty" : $"\n\t{queryString}";
        }

        private static string GetRequestHeaders(HttpContext context) => context.Request.Headers.IsCollectionNullOrEmpty()
                ? " Empty"
                : "\n" + string.Concat(context.Request.Headers.ToList().Select(header => $"\t{header.Key}: {header.Value}\n"));

        private string GetRequestBody(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();
            request.Body.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = streamReader.ReadToEndAsync().Result;
            request.Body.Position = 0;
            return requestBody.IsNullOrEmpty() ? " Empty" : $"\n{requestBody}".Replace("\n", "\n\t");
        }

        private string GetResponseBody(HttpContext context, MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = new StreamReader(context.Response.Body).ReadToEndAsync().Result;
            if (responseBodyText.IsNullOrEmpty())
                return " Empty";

            return $"\n{responseBodyText.ToString(Formatting.Indented)}".Replace("\n", "\n\t");
        }
    }
}