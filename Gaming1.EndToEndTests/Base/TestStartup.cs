using Autofac;
using Gaming1.Api;
using Gaming1.Api.Extensions;
using Gaming1.Application.Service.DependencyInjection;
using Gaming1.Application.Service.Handlers;
using Gaming1.Infrastructure.Repositories.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming1.EndToEndTests.Base
{
    public class TestStartup
    {
        private IConfiguration _configuration;

        public TestStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                    .AddApplicationPart(typeof(Startup).Assembly)
                    .AddFormatterMappings()
                    .AddDataAnnotations();

            services
                .AddAutoMappers()
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetGameRequestHandler).Assembly));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInMemoryRepository();
            builder.RegisterHandlers();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}