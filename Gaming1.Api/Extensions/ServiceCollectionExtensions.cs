using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Gaming1.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocument(document =>
            {
                document.Title = "Project API";
                document.Version = "v1";
                document.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("JWT Token", new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Description = "JWT Token",
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header
                    })
                );
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            });

            return services;
        }
    }
}