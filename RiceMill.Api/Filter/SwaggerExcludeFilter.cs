﻿using Microsoft.OpenApi.Models;
using Shared.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RiceMill.Api.Filter
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
                return;

            var excludedProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null);
            foreach (var excludedProperty in excludedProperties)
            {
                var propertyToRemove = schema.Properties.Keys.SingleOrDefault(x => x.ToLower() == excludedProperty.Name.ToLower());
                if (propertyToRemove != null)
                    schema.Properties.Remove(propertyToRemove);
            }
        }
    }
}