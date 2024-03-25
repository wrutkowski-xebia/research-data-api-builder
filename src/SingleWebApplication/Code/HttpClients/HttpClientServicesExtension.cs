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

            return services;
        } 

        private static IHttpClientBuilder AddSettings(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder.ConfigureHttpClient(configureClient => {
                configureClient.Timeout = TimeSpan.FromSeconds(_timeout);
            });

            return httpClientBuilder;
        }
    }
}
