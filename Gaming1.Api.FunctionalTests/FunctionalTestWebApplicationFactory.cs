using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Gaming1.Api.FunctionalTests
{
    public class FunctionalTestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IDisposable
        where TStartup : class
    {
        protected HttpClient CreateClient(string apiKey)
        {
            CreateHostBuilder();
            var client = base.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("http://localhost/"),
            });
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder
                .UseContentRoot("")
                .UseEnvironment("Test")
                .UseStartup<TStartup>()
                .UseTestServer();
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

}