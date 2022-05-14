using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ToDo.Api.Middlewares;

namespace ToDo.Api.Extensions;

public static class AppExtensions
{
    public static void UseSwaggerExtension(this IApplicationBuilder app)
    {
        app.UseSwagger();
        
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        
        app.UseSwaggerUI(options =>
        {
           
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json", 
                    description.GroupName.ToUpperInvariant());
            }
        });
        
        // app.UseSwagger();
        // app.UseSwaggerUI(c =>
        // {
        //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProgChallenge API");
        // });
    }
    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}