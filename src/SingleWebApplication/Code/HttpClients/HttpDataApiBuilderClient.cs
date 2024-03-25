namespace SingleWebApplication.Code.HttpClients
{
    public class HttpDataApiBuilderClient(HttpClient httpClient)
    {
        public async Task<string> GetCustomersAsync()
        {
            return await httpClient.GetStringAsync("Customer");
        }
    }
}
