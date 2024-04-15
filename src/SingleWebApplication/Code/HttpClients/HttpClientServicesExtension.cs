using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SingleWebApplication.Code.HttpClients
{
    public static class HttpClientServicesExtension
    {
        private static readonly int _timeout = 25;
        private static readonly string _missingUrlConfig = "missingUrlConfig";
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<HttpDataApiBuilderClient>(client =>
                client.BaseAddress = new Uri(configuration["HttpClients:HttpDataApiBuilderClient"] ?? _missingUrlConfig))
                .AddSettings();

            services.AddHttpClient<HttpDataApiBuilderContainerClient>(client =>
                client.BaseAddress = new Uri(configuration["HttpClients:HttpDataApiBuilderContainerClient"] ?? _missingUrlConfig))
                .AddSettings();

            services.AddHttpClient<HttpDataApiBuilderAuthClient>(client =>
                client.BaseAddress = new Uri(configuration["HttpClients:HttpDataApiBuilderClient"] ?? _missingUrlConfig))
                .AddSettings()
                .AddToken();

            services.AddHttpClient<HttpDataApiBuilderContainerAuthClient>(client =>
                client.BaseAddress = new Uri(configuration["HttpClients:HttpDataApiBuilderContainerClient"] ?? _missingUrlConfig))
                .AddSettings()
                .AddToken();

            return services;
        }

        private static IHttpClientBuilder AddSettings(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder.ConfigureHttpClient(configureClient =>
            {
                configureClient.Timeout = TimeSpan.FromSeconds(_timeout);
            });

            return httpClientBuilder;
        }

        private static IHttpClientBuilder AddToken(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder
             .AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
             .ConfigureHandler(
                         authorizedUrls: new[] { "https://research-data-api-builder-dab.lemonocean-d4632e7c.westeurope.azurecontainerapps.io", " http://localhost:5033", "https://lemon-bay-054e84803.5.azurestaticapps.net/data-api/" },
                         scopes: new[] { "api://de4b5da5-3135-4948-aa02-342ac1d8e8fc/Endpoint.Access" }));

            return httpClientBuilder;
        }
    }
}
