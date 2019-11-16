using DataSample.BusinessLayer.Services;
using DataSample.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataSample.Client.Services
{
    public class RemoteProductsService : IProductsService
    {
        private const string BASE_URL = "https://localhost:44343/api/";
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public RemoteProductsService()
        {
            client = new HttpClient { BaseAddress = new Uri(BASE_URL) };

            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<IEnumerable<Product>> GetAsync(string searchTerm, int pageIndex, int itemsPerPage)
        {
            var resource = $"Products?searchTerm={searchTerm}&pageIndex={pageIndex}&itemsPerPage={itemsPerPage}";
            var response = await client.GetStringAsync(resource);

            return JsonSerializer.Deserialize<IEnumerable<Product>>(response, jsonSerializerOptions);
        }

        public async Task<Product> SaveAsync(Product product)
        {
            var json = JsonSerializer.Serialize(product, jsonSerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync("Products", content);

            return product;
        }
    }
}
