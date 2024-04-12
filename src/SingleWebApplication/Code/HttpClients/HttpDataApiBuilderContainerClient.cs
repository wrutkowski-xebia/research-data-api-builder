namespace SingleWebApplication.Code.HttpClients
{
    public class HttpDataApiBuilderContainerClient(HttpClient httpClient)
    {
        public async Task<string> GetCustomersAsync()
        {
            return await httpClient.GetStringAsync("Customer");
        }
    }
}
