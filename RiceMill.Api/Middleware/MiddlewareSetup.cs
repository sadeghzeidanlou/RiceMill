using Swashbuckle.AspNetCore.SwaggerUI;

namespace RiceMill.Api.Middleware
{
    public static class MiddlewareSetup
    {
        public static void AddMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s => s.DocExpansion(DocExpansion.None));
            }
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<HttpStatusMiddleware>();
            app.UseMiddleware<ExecutionTimeMiddleware>();
            app.MapControllers();
        }
    }
}