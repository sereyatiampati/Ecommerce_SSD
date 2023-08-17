using Ecommerce.Models;
using Ecommerce.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.Services
{
    internal class ProductService : IProducts
    {
        private readonly HttpClient _httpClient;
        public readonly string _url = "http://localhost:3000/products";
        public ProductService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<SuccessMessage> CreateProductAsync(AddProducts product)
        {
            var content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_url, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Created Successfully " };
            }
            throw new Exception("Failed to create the product");

        }

        public async Task<SuccessMessage> DeleteProductAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(_url + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Deleted Successfully " };
            }
            throw new Exception("Failed to delete the specified product");
        }

        public async Task<List<Products>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            var products = JsonConvert.DeserializeObject<List<Products>>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                return products;
            }
            throw new Exception("Cannot get the products");
        }

        public async Task<Products> GetProductAsync(string id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            var product = JsonConvert.DeserializeObject<Products>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                return product;
            }
            throw new Exception("Cannot get the product with that Id");
        }

        public async Task<SuccessMessage> UpdateProductAsync(Products product)
        {
            var content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + "/" + product.Id, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "The Product was updated successfully " };
            }
            throw new Exception("Book updating failed!!");
        }
    }
}