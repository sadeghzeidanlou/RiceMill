﻿using RiceMill.Application.Common.Models.Resource;

namespace RiceMill.Api.Configurations.Cors
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .WithHeaders(SharedResource.SecurityHeaderName));
            });
            return services;
        }
    }
}