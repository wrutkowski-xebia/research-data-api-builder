using SingleWebApplication.Code.Authentication;

namespace SingleWebApplication.Code.HttpClients
{
    public class HttpDataApiBuilderAuthClient
    {
        private readonly HttpClient _httpClient;
        public HttpDataApiBuilderAuthClient(HttpClient httpClient, UserDabRoleHeader userDabRoleHeader)
        {
            _httpClient = httpClient;
            userDabRoleHeader.SetHeader(_httpClient);
        }

        public async Task<string> GetCustomersAsync()
        {
            return await _httpClient.GetStringAsync("Customer");
        }
        public async Task<string> GetProductsAsync()
        {
            return await _httpClient.GetStringAsync("Product");
        }

    }
}
