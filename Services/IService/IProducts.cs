using Ecommerce.Models;

namespace Ecommerce.Services.IServices
{
    public interface IProducts
    {
        // Add a product
        Task<SuccessMessage> CreateProductAsync(AddProducts product);
        // Update a product
        Task<SuccessMessage> UpdateProductAsync(Products product);
        // Delete a product
        Task<SuccessMessage> DeleteProductAsync(string id);
        // Get a product
        Task<Products> GetProductAsync(string id);
        // Get all products
        Task<List<Products>> GetAllProductsAsync();
    }
}