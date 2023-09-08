using Swashbuckle.AspNetCore.SwaggerUI;

namespace RiceMill.Api.Middleware
{
    public static class MiddlewareSetup
    {
        public static void AddMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => s.DocExpansion(DocExpansion.None));
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<RequestHeaderInspectorMiddleware>();
            app.UseMiddleware<ExecutionTimeMiddleware>();
            app.MapControllers();
        }
    }
}