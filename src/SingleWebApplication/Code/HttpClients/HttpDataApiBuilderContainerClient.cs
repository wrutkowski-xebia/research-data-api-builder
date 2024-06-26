﻿namespace SingleWebApplication.Code.HttpClients
{
    public class HttpDataApiBuilderContainerClient(HttpClient httpClient)
    {
        public async Task<string> GetCustomersAsync()
        {
            return await httpClient.GetStringAsync("Customer");
        }
        public async Task<string> GetProductsAsync()
        {
            return await httpClient.GetStringAsync("Product");
        }
    }
}
