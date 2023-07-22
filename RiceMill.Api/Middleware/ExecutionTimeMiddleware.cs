﻿using Serilog;
using System.Diagnostics;

namespace RiceMill.Api.Middleware
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ExecutionTimeMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (elapsedMilliseconds > 300)
            {
                Log.Warning($"Request to {context.Request.Path} took {elapsedMilliseconds} ms");
                //logger.LogInformation($"Request to {context.Request.Path} took {elapsedMilliseconds} ms");
            }
        }
    }
}