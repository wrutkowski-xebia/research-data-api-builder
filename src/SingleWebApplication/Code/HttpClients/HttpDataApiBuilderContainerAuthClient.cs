using SingleWebApplication.Code.Authentication;

namespace SingleWebApplication.Code.HttpClients
{
    public class HttpDataApiBuilderContainerAuthClient
    {
        private readonly HttpClient _httpClient;
        public HttpDataApiBuilderContainerAuthClient(HttpClient httpClient, UserDabRoleHeader userDabRoleHeader)
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
