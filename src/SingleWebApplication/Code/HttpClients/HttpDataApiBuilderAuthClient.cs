namespace SingleWebApplication.Code.HttpClients
{
    public class HttpDataApiBuilderAuthClient(HttpClient httpClient)
    {
        public async Task<string> GetCustomersAsync()
        {
            return await httpClient.GetStringAsync("Customer");
        }
        public async Task<string> GetProductsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "Product");

            request.Headers.Add("X-MS-API-ROLE", "Sample.Role");
            //request.Headers.Add("X-MS-API-ROLE", "authenticated");


            var response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();


            //return await httpClient.GetStringAsync("Product");
        }
    }
}
