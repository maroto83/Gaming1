using Autofac.Extensions.DependencyInjection;
using Gaming1.Api.Contracts.Game;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            return Host
                .CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }

        protected HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpContent;
        }

        protected async Task<TResult> GetResultDataFromUrl<TResult>(string url, object content = default)
        {
            var client = CreateClient();
            var response = await client.PostAsync(url, CreateHttpContent(content));
            var result = JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            return result;
        }

        protected async Task<StartResult> StartGame()
        {
            var game = await GetResultDataFromUrl<StartResult>(TestConstants.StartGameUrl);
            return game;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}