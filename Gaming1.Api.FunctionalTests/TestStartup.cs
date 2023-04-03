using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaming1.Api.FunctionalTests
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