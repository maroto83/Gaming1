using Microsoft.AspNetCore.Builder;
using NSwag;

namespace Gaming1.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddSwagger(this IApplicationBuilder app)
        {
            OpenApiSchema[] schemes = {
                OpenApiSchema.Https
            };

            app.UseOpenApi(s =>
            {
                s.Path = "swagger/{documentName}/swagger.json";
                s.PostProcess = (document, request) => { document.Schemes = schemes; };
            });
            app.UseSwaggerUi3(s =>
            {
                s.DocumentPath = "swagger/{documentName}/swagger.json";
                s.Path = "/swagger";
            });
        }
    }
}